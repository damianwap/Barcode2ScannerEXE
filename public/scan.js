const $ = id => document.getElementById(id);
let ws = null, session = null, connected = false;
let scanner = null, camOn = false, torchOn = false, facing = 'environment', lastSent = '', lastAt = 0;
let sentCount = 0;
let audioCtx = null;
let detectedDevice = 'Smartphone HP';

let pairScanner = null;
let pendingQueue = [];

const params = new URLSearchParams(location.search);
let joinKey = (params.get('key') || '').trim();

if (params.get('session')) {
  $('code').value = params.get('session').trim();
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
  updateSessionUI(false);
  $('code').focus();
};

function ok(msg) {
  const el = $('ok');
  el.innerHTML = `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2.5"><polyline points="20 6 9 17 4 12"/></svg> <span></span>`;
  el.querySelector('span').textContent = msg;
  clearTimeout(el._t);
  el._t = setTimeout(() => el.innerHTML = '', 3500);
}

function flash() {
  const f = $('flash');
  f.classList.remove('go');
  void f.offsetWidth;
  f.classList.add('go');
}

function buzz() {
  try { navigator.vibrate && navigator.vibrate([60, 40, 60]); } catch (e) {}
}

function beep() {
  try {
    audioCtx = audioCtx || new (window.AudioContext || window.webkitAudioContext)();
    if (audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
    const now = audioCtx.currentTime;
    const o = audioCtx.createOscillator();
    const g = audioCtx.createGain();
    o.type = 'sine';
    o.frequency.setValueAtTime(1046.5, now);
    g.gain.setValueAtTime(0.15, now);
    g.gain.exponentialRampToValueAtTime(0.0001, now + 0.12);
    o.connect(g);
    g.connect(audioCtx.destination);
    o.start(now);
    o.stop(now + 0.12);
  } catch (e) {}
}

try {
  navigator.wakeLock && navigator.wakeLock.request('screen').catch(() => {});
} catch (e) {}

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
        // Efek notifikasi
        beep();
        buzz();
        flash();

        // Tutup kamera scanner pairing
        await stopPairScanner();

        // Jika QR menunjuk host/IP yang berbeda, alihkan halaman ke sana
        if (parsed.host && parsed.host !== location.host && parsed.fullUrl) {
          ok('Mengalihkan ke server: ' + parsed.host);
          setTimeout(() => { location.href = parsed.fullUrl; }, 400);
          return;
        }

        // Simpan sesi dan kunci koneksi
        session = parsed.session;
        joinKey = parsed.key;
        $('code').value = session;

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
        { fps: 15, qrbox: qrBoxConfig },
        onQrScanSuccess,
        () => {}
      );
    } catch (envErr) {
      // Fallback ke kamera depan/default jika environment tidak tersedia
      await pairScanner.start(
        { facingMode: 'user' },
        { fps: 15, qrbox: qrBoxConfig },
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
    return alert('Masukkan kode sesi 6 digit yang valid dari layar laptop!');
  }
  if (!key) {
    showErr('❌ <strong>Kunci koneksi belum ada.</strong><br>Tekan tombol <b>"Scan QR Code Layar Laptop"</b> di atas agar terhubung secara otomatis dan aman.');
    return;
  }

  session = code;
  joinKey = key;
  $('code').value = code;

  const proto = location.protocol === 'https:' ? 'wss' : 'ws';
  if (ws) {
    try { ws.close(); } catch (e) {}
  }
  setStatus('waiting', 'Menghubungkan...');
  hideErr();
  ws = new WebSocket(proto + '://' + location.host);

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
    setStatus('off', 'Terputus');
    $('btnCam').disabled = true;
    $('btnSend').disabled = true;
    updateSessionUI(false);
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

  if (!connected) {
    if (ws && ws.readyState === WebSocket.CONNECTING) {
      pendingQueue.push({ text, format: format || 'unknown' });
      ok('⏳ Menghubungkan... Barcode akan dikirim otomatis saat tersambung.');
      return;
    }
    showErr('⚠️ HP belum terhubung ke laptop. Pastikan status "Tersambung" sebelum memindai.');
    return;
  }
  
  const now = Date.now();
  if (text === lastSent && now - lastAt < 1500) return;
  lastSent = text;
  lastAt = now;

  ws.send(JSON.stringify({ type: 'scan', session, text, format: format || 'unknown' }));
  sentCount++;
  $('cnt').textContent = sentCount;

  // History entry
  const empty = $('histEmpty');
  if (empty) empty.style.display = 'none';

  const d = document.createElement('div');
  const preview = text.length > 32 ? text.slice(0, 32) + '…' : text;
  d.innerHTML = `
    <span class="code-txt">${escapeHtml(preview)}</span>
    <small><svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2.5"><polyline points="20 6 9 17 4 12"/></svg> OK</small>
  `;
  $('hist').prepend(d);

  flash();
  buzz();
  beep();
  ok('Tersinkron: ' + preview);

  if (!$('optCont').checked && scanner) {
    stopCam();
  }
}

function escapeHtml(s) {
  return String(s).replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
}

function showErr(html) {
  const e = $('errbox');
  e.style.display = 'block';
  e.innerHTML = html;
}
function hideErr() { 
  $('errbox').style.display = 'none'; 
}

$('btnCam').onclick = async () => {
  if (camOn) {
    stopCam();
    return;
  }
  hideErr();
  
  if (typeof Html5Qrcode === 'undefined') {
    showErr('❌ Library kamera gagal dimuat.<br>Refresh halaman ini dan pastikan HP tersambung ke internet.');
    return;
  }
  if (!window.isSecureContext) {
    showErr('❌ Kamera diblokir karena halaman tidak HTTPS.<br>Buka persis alamat <b>https://...</b> dari layar laptop, lalu pilih <b>Advanced / Lanjutkan</b>.');
    return;
  }

  try {
    scanner = new Html5Qrcode('reader');
    $('vfPlaceholder').style.display = 'none';
    
    await scanner.start(
      { facingMode: facing },
      { 
        fps: 15, 
        qrbox: (w, h) => {
          const edge = Math.floor(Math.min(w, h) * 0.75);
          return { width: edge, height: edge };
        } 
      },
      (text, res) => {
        const fmt = res && res.result && res.result.format ? res.result.format.formatName : 'camera';
        send(text, fmt);
      },
      () => {}
    );

    camOn = true;
    $('camBtnText').textContent = '⏹ Matikan Kamera';
    $('btnCam').classList.add('stop');
    $('laserLine').classList.add('active');
    $('btnTorch').disabled = false;
    $('btnSwitch').disabled = false;
    ok('Kamera aktif — arahkan ke barcode');
  } catch (e) {
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
  camOn = false;
  $('camBtnText').textContent = 'Nyalakan Kamera';
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
    alert('Flash / senter tidak didukung di perangkat ini.');
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
if (params.get('session') && joinKey) {
  setTimeout(() => $('btnConn').click(), 500);
}
