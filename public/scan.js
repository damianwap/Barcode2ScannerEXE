const $ = id => document.getElementById(id);
let ws = null, session = null, connected = false;
let scanner = null, camOn = false, torchOn = false, facing = 'environment', lastSent = '', lastAt = 0;
let sentCount = 0;
let audioCtx = null;
let detectedDevice = 'Smartphone HP';

let pairScanner = null;
let pendingQueue = [];

// OFFLINE QUEUE: scan tetap bisa dilakukan tanpa koneksi (gudang/basement tanpa
// sinyal). Disimpan di localStorage (max 500), dikirim otomatis saat reconnect
// atau via tombol "Sync" manual. Queue hanya dibaca/ditulis origin ini.
const OFFLINE_KEY = 'b2s_offline_queue';
function loadOfflineQueue() {
  try {
    const raw = localStorage.getItem(OFFLINE_KEY);
    if (!raw) return [];
    const arr = JSON.parse(raw);
    return Array.isArray(arr) ? arr.filter(i => i && i.text) : [];
  } catch (e) { return []; }
}
function saveOfflineQueue(q) {
  try {
    localStorage.setItem(OFFLINE_KEY, JSON.stringify(q.slice(0, 500)));
  } catch (e) {}
}
let offlineQueue = loadOfflineQueue();

function enqueueOffline(text, format) {
  offlineQueue.push({ text: String(text), format: format || 'unknown', at: Date.now() });
  if (offlineQueue.length > 500) offlineQueue = offlineQueue.slice(-500);
  saveOfflineQueue(offlineQueue);
  updateOfflineBadge();
}

function updateOfflineBadge() {
  const el = $('offlineBadge');
  const btn = $('btnSync');
  if (!el) return;
  if (offlineQueue.length > 0) {
    el.style.display = 'inline-flex';
    el.querySelector('span').textContent = offlineQueue.length + ' offline';
    if (btn) btn.disabled = !connected;
  } else {
    el.style.display = 'none';
    if (btn) btn.disabled = true;
  }
}

const params = new URLSearchParams(location.search);

// Persistensi refresh: sesi + key di sessionStorage (selamat dari refresh, hilang
// saat tab ditutup), history + counter di localStorage (selamat dari refresh).
const SESS_KEY = 'b2s_session';
const JOIN_KEY = 'b2s_join_key';
const HIST_KEY = 'b2s_history_v1';
const COUNT_KEY = 'b2s_sentcount_v1';
const MAX_HIST = 200;

function loadHistStore() {
  try {
    const raw = localStorage.getItem(HIST_KEY);
    if (!raw) return [];
    const arr = JSON.parse(raw);
    if (!Array.isArray(arr)) return [];
    return arr.filter(i => i && i.text).slice(0, MAX_HIST);
  } catch (e) { return []; }
}
function saveHistStore() {
  try {
    localStorage.setItem(HIST_KEY, JSON.stringify(histStore.slice(0, MAX_HIST)));
    localStorage.setItem(COUNT_KEY, String(sentCount));
  } catch (e) {}
}
let histStore = loadHistStore();
try {
  const c = parseInt(localStorage.getItem(COUNT_KEY) || '0', 10);
  if (!isNaN(c) && c > 0) {
    sentCount = c;
    $('cnt').textContent = sentCount;
  }
} catch (e) {}

function persistSession(s, k) {
  try {
    if (s) sessionStorage.setItem(SESS_KEY, s);
    if (k) sessionStorage.setItem(JOIN_KEY, k);
  } catch (e) {}
}
function clearPersistedSession() {
  try {
    sessionStorage.removeItem(SESS_KEY);
    sessionStorage.removeItem(JOIN_KEY);
  } catch (e) {}
}

let joinKey = (params.get('key') || '').trim();
let initialSession = (params.get('session') || '').trim();
try {
  if (!initialSession) initialSession = (sessionStorage.getItem(SESS_KEY) || '').trim();
  if (!joinKey) joinKey = (sessionStorage.getItem(JOIN_KEY) || '').trim();
} catch (e) {}
// Simpan secret dari URL agar selamat dari refresh (URL dibersihkan di bawah)
persistSession((params.get('session') || '').trim(), (params.get('key') || '').trim());

if (initialSession) {
  $('code').value = initialSession;
}

if (joinKey) {
  $('hintAuth').innerHTML = '🛡️ <strong>Kunci QR Terverifikasi:</strong> HP Anda akan terhubung secara otomatis ke laptop.';
} else {
  $('hintAuth').innerHTML = '💡 Tekan tombol <b>"Scan QR Code Layar Laptop"</b> di atas agar terhubung otomatis dan aman.';
}

// Algoritma Deteksi Tipe & Model HP Otomatis
async function detectPhoneType() {
  let model = '';
  let brand = '';

  if (navigator.userAgentData && navigator.userAgentData.getHighEntropyValues) {
    try {
      const data = await navigator.userAgentData.getHighEntropyValues(['model', 'platform']);
      if (data.model) model = data.model;
    } catch (e) {}
  }

  const ua = navigator.userAgent;
  if (!model) {
    const m = ua.match(/;\s*([^;]+?)\s*Build/i);
    if (m) model = m[1].trim();
  }

  // Identifikasi Merk HP
  if (/SM-[A-Za-z0-9]+/i.test(model) || /Samsung/i.test(ua)) brand = 'Samsung';
  else if (/Xiaomi|Redmi|POCO/i.test(ua) || /220|210|M20/i.test(model)) brand = 'Xiaomi';
  else if (/OPPO|CPH/i.test(ua) || /^CPH/i.test(model)) brand = 'OPPO';
  else if (/vivo/i.test(ua) || /^V2/i.test(model)) brand = 'Vivo';
  else if (/Realme|RMX/i.test(ua) || /^RMX/i.test(model)) brand = 'Realme';
  else if (/Infinix|TECNO|itel/i.test(ua) || /^X[0-9]{3}/i.test(model)) brand = 'Infinix';
  else if (/iPhone/i.test(ua)) brand = 'Apple iPhone';
  else if (/iPad/i.test(ua)) brand = 'Apple iPad';
  else if (/Pixel/i.test(ua)) brand = 'Google Pixel';
  else if (/Huawei|HONOR/i.test(ua)) brand = 'Huawei';
  else if (/Android/i.test(ua)) brand = 'HP Android';
  else brand = 'Smartphone HP';

  if (model && !brand.toLowerCase().includes(model.toLowerCase())) {
    return `${brand} ${model}`;
  }
  return brand;
}

// Jalankan deteksi otomatis dan kunci tampilan
detectPhoneType().then(name => {
  detectedDevice = name;
  $('deviceDisplayName').textContent = name;
});

function setStatus(mode, txt) {
  const pill = $('statusPill');
  const s = $('status');
  s.textContent = txt;
  pill.classList.remove('on', 'waiting');
  if (mode === 'on') pill.classList.add('on');
  if (mode === 'waiting') pill.classList.add('waiting');
}

function updateSessionUI(isConnected, name) {
  if (isConnected) {
    $('sessionCard').style.display = 'none';
    $('sessionActiveBar').classList.add('active');
    $('activeCodeDisplay').textContent = session;
    $('activeDeviceDisplay').textContent = name || detectedDevice;
  } else {
    $('sessionCard').style.display = 'block';
    $('sessionActiveBar').classList.remove('active');
  }
}

$('btnChangeSession').onclick = () => {
  if (camOn) stopCam();
  stopPairScanner();
  try { if (ws) { clearTimeout(ws._rc); ws.close(); } } catch (e) {}
  connected = false;
  session = null;
  joinKey = '';
  clearPersistedSession();
  updateSessionUI(false);
  $('code').value = '';
  $('code').focus();
};

function ok(msg) {
  const el = $('ok');
  el.innerHTML = `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#15803d" stroke-width="2.5" aria-hidden="true" focusable="false"><polyline points="20 6 9 17 4 12"/></svg> <span></span>`;
  el.querySelector('span').textContent = msg;
  clearTimeout(el._t);
  el._t = setTimeout(() => el.innerHTML = '', 3500);
}

function buzz() {
  try { navigator.vibrate && navigator.vibrate(30); } catch (e) {}
}

function beep() {
  try {
    const Ctx = window.AudioContext || window.webkitAudioContext;
    if (!Ctx) return;
    if (!audioCtx) {
      audioCtx = new Ctx();
      if (audioCtx.sampleRate > 44100) {
        try { audioCtx.close(); } catch (e) {}
        audioCtx = new Ctx({ sampleRate: 44100 });
      }
    }
    if (audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
    const now = audioCtx.currentTime;
    const o = audioCtx.createOscillator();
    const g = audioCtx.createGain();
    o.type = 'sine';
    o.frequency.setValueAtTime(1046.5, now);
    g.gain.setValueAtTime(0.12, now);
    g.gain.exponentialRampToValueAtTime(0.0001, now + 0.1);
    o.connect(g);
    g.connect(audioCtx.destination);
    o.start(now);
    o.stop(now + 0.1);
  } catch (e) {}
}

let wakeLockObj = null;
async function holdWakeLock() {
  try {
    if ('wakeLock' in navigator) {
      wakeLockObj = await navigator.wakeLock.request('screen');
    }
  } catch (e) {}
}
document.addEventListener('visibilitychange', () => {
  if (document.visibilityState === 'visible' && wakeLockObj !== null) {
    holdWakeLock();
  }
});

// Parser QR Code untuk mengenali format URL, JSON, atau teks sesi dari aplikasi laptop
function parseQrCodeData(data) {
  if (!data) return null;
  data = data.trim();

  // 1. Coba parse sebagai URL
  try {
    let urlObj = null;
    if (data.startsWith('http://') || data.startsWith('https://')) {
      urlObj = new URL(data);
    } else if (data.includes('session=') || data.includes('?')) {
      urlObj = new URL('https://dummy/' + data.replace(/^.*?\?/, '?'));
    }

    if (urlObj) {
      const s = urlObj.searchParams.get('session');
      const k = urlObj.searchParams.get('key');
      if (s) {
        return {
          session: s.trim(),
          key: (k || '').trim(),
          host: urlObj.host && urlObj.host !== 'dummy' ? urlObj.host : null,
          fullUrl: data.startsWith('http') ? data : null
        };
      }
    }
  } catch (e) {}

  // 2. Coba parse JSON {"session":"...","key":"..."}
  try {
    const obj = JSON.parse(data);
    if (obj.session) {
      return {
        session: String(obj.session).trim(),
        key: String(obj.key || '').trim(),
        host: null,
        fullUrl: null
      };
    }
  } catch (e) {}

  // 3. Regex session dan key
  const sMatch = data.match(/session[=:]([0-9]{6})/i);
  const kMatch = data.match(/key[=:]([a-zA-Z0-9_-]+)/i);
  if (sMatch) {
    return {
      session: sMatch[1],
      key: kMatch ? kMatch[1] : '',
      host: null,
      fullUrl: null
    };
  }

  // 4. Plain 6 digit
  if (/^[0-9]{6}$/.test(data)) {
    return {
      session: data,
      key: '',
      host: null,
      fullUrl: null
    };
  }

  return null;
}

// KEAMANAN: validasi IP literal private — IPv4 penuh (4 oktet) atau loopback.
// Hostname/DNS TIDAK pernah auto-redirect (selalu confirm), mencegah "10.evil.com".
function isPrivateLiteralIp(h) {
  const parts = h.split('.');
  if (parts.length !== 4 || !parts.every(p => /^\d{1,3}$/.test(p) && Number(p) <= 255)) return false;
  const a = Number(parts[0]), b = Number(parts[1]);
  return a === 10 || (a === 172 && b >= 16 && b <= 31) || (a === 192 && b === 168) || a === 127;
}

// Scanner Kamera untuk Membaca QR Code pada Aplikasi EXE Laptop
async function startPairScanner() {
  hideErr();
  if (typeof Html5Qrcode === 'undefined') {
    showErr('❌ Library kamera belum siap. Silakan muat ulang halaman ini.');
    return;
  }
  if (!window.isSecureContext) {
    showErr('❌ Kamera diblokir karena halaman tidak dibuka via HTTPS.<br>Buka alamat <b>https://...</b> dari layar laptop.');
    return;
  }

  // Matikan kamera barcode utama jika sedang aktif
  if (camOn) await stopCam();

  const wrap = $('pairViewfinder');
  const btn = $('btnScanQrExe');
  const subhint = $('qrSubhint');
  if (wrap) wrap.style.display = 'block';
  if (btn) btn.style.display = 'none';
  if (subhint) subhint.style.display = 'none';

  try {
    if (pairScanner) {
      try { await pairScanner.stop(); await pairScanner.clear(); } catch (e) {}
    }

    pairScanner = new Html5Qrcode('pairReader');
    const qrBoxConfig = (w, h) => {
      const edge = Math.floor(Math.min(w, h) * 0.85);
      return { width: edge, height: edge };
    };

    const onQrScanSuccess = async (decodedText) => {
      const parsed = parseQrCodeData(decodedText);
      if (parsed && parsed.session) {
        beep();
        buzz();

        // Tutup kamera scanner pairing
        await stopPairScanner();

        // Jika QR menunjuk host/IP yang berbeda, alihkan halaman ke sana
        if (parsed.host && parsed.host !== location.host && parsed.fullUrl) {
          // KEAMANAN: Hanya redirect otomatis ke IP literal jaringan lokal/private.
          // Regex lama (/^(192\.168\.|10\.|...)/) lolos untuk "192.168.evil.com" karena
          // tidak di-anchor ke akhir string — QR palsu bisa redirect ke phishing.
          const h = parsed.host.split(':')[0].toLowerCase();
          if (isPrivateLiteralIp(h) || h === 'localhost') {
            ok('Mengalihkan ke server lokal: ' + parsed.host);
            setTimeout(() => { location.href = parsed.fullUrl; }, 400);
          } else if (confirm('QR mengarah ke server: ' + parsed.host + '\n\nApakah Anda yakin ingin membuka alamat ini?')) {
            location.href = parsed.fullUrl;
          }
          return;
        }

        // Simpan sesi dan kunci koneksi
        session = parsed.session;
        joinKey = parsed.key;
        $('code').value = session;
        persistSession(session, joinKey);

        ok(`✓ QR Terbaca! Menghubungkan ke laptop...`);
        
        // Langsung otomatis terhubung
        connectToServer(session, joinKey);
      } else {
        showErr('⚠️ QR Code tidak sesuai format Barcode2Scanner.<br>Pastikan mengarahkan kamera ke QR Code di layar laptop!');
      }
    };

    try {
      await pairScanner.start(
        { facingMode: 'environment' },
        { fps: 5, qrbox: qrBoxConfig },
        onQrScanSuccess,
        () => {}
      );
    } catch (envErr) {
      // Fallback ke kamera depan/default jika environment tidak tersedia
      await pairScanner.start(
        { facingMode: 'user' },
        { fps: 5, qrbox: qrBoxConfig },
        onQrScanSuccess,
        () => {}
      );
    }
  } catch (err) {
    if (wrap) wrap.style.display = 'none';
    if (btn) btn.style.display = 'flex';
    if (subhint) subhint.style.display = 'block';
    const msg = String((err && err.message) || err);
    showErr('❌ Gagal mengaktifkan kamera scan QR:<br>' + escapeHtml(msg));
  }
}

async function stopPairScanner() {
  try {
    if (pairScanner) {
      await pairScanner.stop();
      await pairScanner.clear();
    }
  } catch (e) {}
  pairScanner = null;
  const wrap = $('pairViewfinder');
  const btn = $('btnScanQrExe');
  const subhint = $('qrSubhint');
  if (wrap) wrap.style.display = 'none';
  if (btn) btn.style.display = 'flex';
  if (subhint) subhint.style.display = 'block';
}

if ($('btnScanQrExe')) {
  $('btnScanQrExe').onclick = () => startPairScanner();
}
if ($('btnClosePair')) {
  $('btnClosePair').onclick = () => stopPairScanner();
}

function connectToServer(targetSession, targetKey) {
  const code = (targetSession || $('code').value).trim();
  const key = (targetKey !== undefined ? targetKey : joinKey).trim();

  if (!/^\d{6}$/.test(code)) {
    showErr('Masukkan kode sesi 6 digit yang valid dari layar laptop.');
    $('code').focus();
    return;
  }
  if (!key) {
    showErr('❌ <strong>Kunci koneksi belum ada.</strong><br>Tekan tombol <b>"Scan QR Code Layar Laptop"</b> di atas agar terhubung secara otomatis dan aman.');
    return;
  }

  session = code;
  joinKey = key;
  $('code').value = code;
  persistSession(session, joinKey);

  // Server hanya melayani TLS: tolak koneksi polos agar session/key tidak bocor cleartext.
  if (location.protocol !== 'https:' && location.hostname !== 'localhost' && location.hostname !== '127.0.0.1') {
    showErr('Koneksi diblokir: buka halaman ini via <b>https://</b> dari layar laptop agar session dan kunci tidak terkirim polos.');
    return;
  }
  if (ws) {
    try { ws.close(); } catch (e) {}
  }
  setStatus('waiting', 'Menghubungkan...');
  hideErr();
  ws = new WebSocket('wss://' + location.host);

  ws.onopen = () => {
    ws.send(JSON.stringify({ 
      type: 'register', 
      role: 'phone', 
      session,
      key: joinKey,
      name: detectedDevice
    }));
  };

  ws.onclose = () => {
    connected = false;
    setStatus('off', 'Terputus — menyambung ulang...');
    $('btnCam').disabled = true;
    $('btnSend').disabled = true;
    updateSessionUI(false);
    // Reconnect otomatis: sesi tersimpan di sessionStorage, jadi bisa register ulang.
    if (session && joinKey) {
      clearTimeout(ws._rc);
      ws._rc = setTimeout(() => {
        if (!connected && session && joinKey) connectToServer(session, joinKey);
      }, 2500);
    }
  };

  ws.onmessage = (ev) => {
    let m; try { m = JSON.parse(ev.data); } catch { return; }
    
    if (m.type === 'registered') {
      if (m.approved) {
        connected = true;
        setStatus('on', 'Tersambung & Terverifikasi');
        $('btnCam').disabled = false;
        $('btnSend').disabled = false;
        updateSessionUI(true, m.deviceName);
        hideErr();
        ok(`✓ Tersambung [${m.deviceName || detectedDevice}]! Mengaktifkan kamera barcode...`);
        updateOfflineBadge();
        // Flush offline queue yang menumpuk saat terputus
        if (offlineQueue.length > 0) {
          setTimeout(flushOfflineQueue, 500);
        }
        // Otomatis nyalakan kamera pemindai barcode setelah terhubung
        if (!camOn) {
          setTimeout(() => {
            if (connected && !camOn) {
              $('btnCam').click();
            }
          }, 350);
        }

        // Kirim antrean scan yang tersimpan saat proses menghubungkan
        if (pendingQueue.length > 0) {
          setTimeout(() => {
            while (pendingQueue.length > 0) {
              const item = pendingQueue.shift();
              send(item.text, item.format);
            }
          }, 150);
        }
      } else if (m.status === 'waiting_approval') {
        connected = false;
        setStatus('waiting', '⏳ Menunggu Izin Laptop');
        $('btnCam').disabled = true;
        $('btnSend').disabled = true;
        showErr('⏳ <strong>Menunggu Persetujuan Laptop:</strong><br>Silakan lihat layar laptop.');
      }
    }

    if (m.type === 'approved') {
      connected = true;
      setStatus('on', 'Tersambung & Terverifikasi');
      $('btnCam').disabled = false;
      $('btnSend').disabled = false;
      updateSessionUI(true, m.deviceName);
      hideErr();
      ok(`✓ Disetujui laptop! Silakan nyalakan kamera.`);
      beep();
      buzz();

      if (pendingQueue.length > 0) {
        setTimeout(() => {
          while (pendingQueue.length > 0) {
            const item = pendingQueue.shift();
            send(item.text, item.format);
          }
        }, 150);
      }
    }

    if (m.type === 'rejected') {
      connected = false;
      setStatus('off', '❌ Izin Ditolak');
      updateSessionUI(false);
      showErr('❌ Permintaan koneksi ditolak oleh pemilik laptop.');
    }

    if (m.type === 'error') {
      setStatus('off', '❌ Gagal Terhubung');
      showErr('❌ ' + escapeHtml(m.message || ''));
    }
  };
}

$('btnConn').onclick = () => connectToServer();

function send(text, format) {
  text = String(text || '').trim();
  if (!text) return;

  const now = Date.now();
  if (text === lastSent && now - lastAt < 1500) return;
  lastSent = text;
  lastAt = now;

  // Belum terhubung / socket mati → simpan ke offline queue, tetap catat di history lokal.
  if (!connected || !ws || ws.readyState !== WebSocket.OPEN) {
    if (ws && ws.readyState === WebSocket.CONNECTING) {
      pendingQueue.push({ text, format: format || 'unknown' });
      ok('⏳ Menghubungkan... Barcode akan dikirim otomatis saat tersambung.');
      return;
    }
    enqueueOffline(text, format);
    recordLocal(text, format, true);
    ok('📴 Offline — tersimpan (' + offlineQueue.length + '). Akan dikirim saat Sync.');
    if (!$('optCont').checked && scanner) stopCam();
    return;
  }

  transmit(text, format);
}

function transmit(text, format) {
  ws.send(JSON.stringify({ type: 'scan', session, text, format: format || 'unknown' }));
  recordLocal(text, format, false);
}

function recordLocal(text, format, isOffline) {
  sentCount++;
  $('cnt').textContent = sentCount;
  histStore.unshift({ text: String(text), format: format || 'unknown', offline: !!isOffline, at: Date.now() });
  if (histStore.length > MAX_HIST) histStore = histStore.slice(0, MAX_HIST);
  saveHistStore();

  // History entry
  const empty = $('histEmpty');
  if (empty) empty.style.display = 'none';

  const d = document.createElement('div');
  const preview = text.length > 32 ? text.slice(0, 32) + '…' : text;
  const tag = isOffline
    ? '<small style="color:#b45309">📴 offline</small>'
    : '<small><svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="#15803d" stroke-width="2.5" aria-hidden="true" focusable="false"><polyline points="20 6 9 17 4 12"/></svg> OK</small>';
  d.innerHTML = `
    <span class="code-txt">${escapeHtml(preview)}</span>
    ${tag}
  `;
  $('hist').prepend(d);

  buzz();
  beep();
  if (!isOffline) ok('Tersinkron: ' + preview);

  if (!$('optCont').checked && scanner) {
    stopCam();
  }
}

// Kirim seluruh offline queue ke laptop (dipanggil otomatis saat reconnect + tombol Sync).
function flushOfflineQueue() {
  if (!connected || !ws || ws.readyState !== WebSocket.OPEN) return;
  if (offlineQueue.length === 0) return;
  const batch = offlineQueue.splice(0, offlineQueue.length);
  saveOfflineQueue(offlineQueue);
  updateOfflineBadge();
  let n = 0;
  for (const item of batch) {
    try {
      ws.send(JSON.stringify({ type: 'scan', session, text: item.text, format: item.format || 'unknown', offlineAt: item.at }));
      n++;
    } catch (e) {
      offlineQueue.unshift(item);
    }
  }
  saveOfflineQueue(offlineQueue);
  updateOfflineBadge();
  if (n > 0) ok('✅ ' + n + ' scan offline tersinkron ke laptop!');
}

if ($('btnSync')) {
  $('btnSync').onclick = () => {
    if (!connected) {
      showErr('⚠️ Hubungkan ke laptop dulu, baru tekan Sync.');
      return;
    }
    flushOfflineQueue();
    if (offlineQueue.length === 0) hideErr();
  };
}

if ($('btnClearHist')) {
  $('btnClearHist').onclick = () => {
    if (histStore.length === 0 && offlineQueue.length === 0) {
      ok('Riwayat sudah kosong.');
      return;
    }
    const pending = offlineQueue.length;
    const msg = pending > 0
      ? `Hapus ${histStore.length} riwayat dan ${pending} scan offline yang belum di-sync? Yang sudah terkirim ke laptop tetap ada di sana.`
      : `Hapus ${histStore.length} riwayat di HP ini? Yang sudah terkirim ke laptop tetap ada di sana.`;
    if (!confirm(msg)) return;
    histStore = [];
    sentCount = 0;
    offlineQueue = [];
    try {
      localStorage.removeItem(HIST_KEY);
      localStorage.removeItem(COUNT_KEY);
    } catch (e) {}
    saveOfflineQueue(offlineQueue);
    saveHistStore();
    $('cnt').textContent = '0';
    renderHistStore();
    updateOfflineBadge();
    ok('Riwayat HP dihapus.');
  };
}

function escapeHtml(s) {
  return String(s).replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
}

function showErr(html) {
  const e = $('errbox');
  e.style.display = 'block';
  e.innerHTML = html;
}

// Gambar ulang daftar history dari store (dipanggil saat load agar refresh tidak hilang).
function renderHistStore() {
  const box = $('hist');
  if (!box) return;
  box.innerHTML = '';
  if (histStore.length === 0) {
    box.innerHTML = '<div class="hist-empty" id="histEmpty">Belum ada barcode yang dikirim</div>';
    return;
  }
  for (const item of histStore) {
    const d = document.createElement('div');
    const preview = item.text.length > 32 ? item.text.slice(0, 32) + '…' : item.text;
    const tag = item.offline
      ? '<small style="color:#b45309">📴 offline</small>'
      : '<small><svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="#15803d" stroke-width="2.5" aria-hidden="true" focusable="false"><polyline points="20 6 9 17 4 12"/></svg> OK</small>';
    d.innerHTML = `
      <span class="code-txt">${escapeHtml(preview)}</span>
      ${tag}
    `;
    box.appendChild(d);
  }
}
function hideErr() { 
  $('errbox').style.display = 'none'; 
}

function setCamState(state) {
  const hint = $('camStateHint');
  const btnTxt = $('camBtnText');
  if (state === 'starting') {
    if (btnTxt) btnTxt.textContent = 'Menyalakan...';
    if (hint) hint.textContent = 'Membuka kamera...';
  } else if (state === 'on') {
    if (btnTxt) btnTxt.textContent = '⏹ Matikan Kamera';
    if (hint) hint.textContent = 'Kamera aktif. Arahkan ke barcode.';
  } else {
    if (btnTxt) btnTxt.textContent = 'Nyalakan Kamera';
    if (hint) hint.textContent = 'Kamera mati. Nyalakan untuk mulai memindai.';
  }
}

$('btnCam').onclick = async () => {
  if (camOn) {
    stopCam();
    return;
  }
  hideErr();
  setCamState('starting');
  
  if (typeof Html5Qrcode === 'undefined') {
    showErr('❌ Library kamera gagal dimuat.<br>Refresh halaman ini dan pastikan HP tersambung ke internet.');
    setCamState('off');
    return;
  }
  if (!window.isSecureContext) {
    showErr('❌ Kamera diblokir karena halaman tidak HTTPS.<br>Buka persis alamat <b>https://...</b> dari layar laptop, lalu pilih <b>Advanced / Lanjutkan</b>.');
    setCamState('off');
    return;
  }

  try {
    scanner = new Html5Qrcode('reader');
    $('vfPlaceholder').style.display = 'none';
    holdWakeLock();

    await scanner.start(
      { facingMode: facing },
      {
        fps: 8,
        qrbox: (w, h) => {
          const edge = Math.floor(Math.min(w, h) * 0.75);
          return { width: edge, height: edge };
        },
        videoConstraints: { facingMode: { ideal: facing }, width: { ideal: 640 }, height: { ideal: 480 } }
      },
      (text, res) => {
        const fmt = res && res.result && res.result.format ? res.result.format.formatName : 'camera';
        send(text, fmt);
      },
      () => {}
    );

    camOn = true;
    setCamState('on');
    $('btnCam').classList.add('stop');
    $('laserLine').classList.add('active');
    $('btnTorch').disabled = false;
    $('btnSwitch').disabled = false;
  } catch (e) {
    setCamState('off');
    $('vfPlaceholder').style.display = 'flex';
    const msg = String((e && e.message) || e);
    let saran = '1) Izinkan akses kamera di browser. 2) Pastikan kamera tidak dipakai aplikasi lain.';
    if (/permission|denied|not allowed/i.test(msg)) {
      saran = 'Izin kamera DITOLAK. Buka pengaturan browser / ikon 🔒 di address bar → izinkan Kamera → muat ulang halaman.';
    } else if (/not found|no camera|devices/i.test(msg)) {
      saran = 'Kamera tidak ditemukan. Tutup aplikasi kamera lain atau coba tombol Ganti Kamera.';
    }
    showErr('❌ Gagal mengaktifkan kamera:<br>' + escapeHtml(msg) + '<br><br>👉 Solusi: ' + saran);
  }
};

async function stopCam() {
  try {
    if (scanner) {
      await scanner.stop();
      await scanner.clear();
    }
  } catch (e) {}
  try {
    if (wakeLockObj) { await wakeLockObj.release(); wakeLockObj = null; }
  } catch (e) {}
  camOn = false;
  setCamState('off');
  $('btnCam').classList.remove('stop');
  $('laserLine').classList.remove('active');
  $('vfPlaceholder').style.display = 'flex';
  $('btnTorch').disabled = true;
  $('btnSwitch').disabled = true;
  $('btnTorch').classList.remove('on');
  torchOn = false;
  $('torchTxt').textContent = 'Senter / Flash';
}

$('btnTorch').onclick = async () => {
  if (!scanner) return;
  try {
    torchOn = !torchOn;
    await scanner.applyVideoConstraints({ advanced: [{ torch: torchOn }] });
    if (torchOn) {
      $('btnTorch').classList.add('on');
      $('torchTxt').textContent = 'Flash ON';
    } else {
      $('btnTorch').classList.remove('on');
      $('torchTxt').textContent = 'Senter / Flash';
    }
  } catch (e) {
    showErr('Senter tidak didukung di perangkat ini. Nyalakan lampu ruangan atau dekatkan barcode ke kamera.');
  }
};

$('btnSwitch').onclick = async () => {
  facing = facing === 'environment' ? 'user' : 'environment';
  const wasOn = camOn;
  if (wasOn) await stopCam();
  if (wasOn) $('btnCam').click();
};

$('btnSend').onclick = () => {
  const v = $('manual').value.trim();
  if (!v) return;
  send(v, 'MANUAL');
  $('manual').value = '';
  $('manual').focus();
};

$('manual').addEventListener('keydown', (e) => {
  if (e.key === 'Enter') {
    $('btnSend').click();
  }
});

// Auto-connect if session parameter exists
if (initialSession && joinKey) {
  session = initialSession;
  setTimeout(() => connectToServer(session, joinKey), 500);
}
updateOfflineBadge();
renderHistStore();

// KEAMANAN: hapus secret (session/key) dari URL setelah dibaca agar tidak tersimpan
// di browser history, dan tidak bocor via share-URL / screen-share / shoulder-surfing.
try {
  if (params.get('session') || params.get('key')) {
    history.replaceState(null, '', location.pathname);
  }
} catch (e) {}

// Peringatan saat tab ditutup: hanya jika ada scan offline yang belum tersinkron.
// Riwayat tersimpan di localStorage dan pulih saat refresh; sesi perlu pairing ulang.
window.addEventListener('beforeunload', (e) => {
  if (offlineQueue.length > 0) {
    e.preventDefault();
    e.returnValue = '';
  }
});
