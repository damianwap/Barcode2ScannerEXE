const $ = id => document.getElementById(id);
let ws = null, session = null, hostKey = null, joinKey = null, rows = [], lastText = '';
let audioCtx = null;
let searchQuery = '';
let activeReqId = null;
let connectedPhones = [];
let reconnectTimer = null;

// Sound synthesis
function beep(type = 'scan') {
  if (!$('optBeep').checked) return;
  try {
    audioCtx = audioCtx || new (window.AudioContext || window.webkitAudioContext)();
    if (audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
    const now = audioCtx.currentTime;

    if (type === 'alert') {
      const o = audioCtx.createOscillator();
      const g = audioCtx.createGain();
      o.type = 'triangle';
      o.frequency.setValueAtTime(587.33, now);
      o.frequency.setValueAtTime(880.00, now + 0.1);
      g.gain.setValueAtTime(0.2, now);
      g.gain.exponentialRampToValueAtTime(0.001, now + 0.35);
      o.connect(g);
      g.connect(audioCtx.destination);
      o.start(now);
      o.stop(now + 0.35);
      return;
    }

    // Normal scan chime
    const o1 = audioCtx.createOscillator();
    const g1 = audioCtx.createGain();
    o1.type = 'sine';
    o1.frequency.setValueAtTime(1046.5, now);
    g1.gain.setValueAtTime(0.12, now);
    g1.gain.exponentialRampToValueAtTime(0.001, now + 0.12);
    o1.connect(g1);
    g1.connect(audioCtx.destination);
    o1.start(now);
    o1.stop(now + 0.12);

    const o2 = audioCtx.createOscillator();
    const g2 = audioCtx.createGain();
    o2.type = 'sine';
    o2.frequency.setValueAtTime(1567.98, now + 0.05);
    g2.gain.setValueAtTime(0.09, now + 0.05);
    g2.gain.exponentialRampToValueAtTime(0.0001, now + 0.18);
    o2.connect(g2);
    g2.connect(audioCtx.destination);
    o2.start(now + 0.05);
    o2.stop(now + 0.18);
  } catch (e) {}
}

let toastTimer = null;
function toast(msg) {
  const t = $('toast');
  $('toastMsg').textContent = msg;
  t.classList.add('show');
  clearTimeout(toastTimer);
  toastTimer = setTimeout(() => t.classList.remove('show'), 2200);
}

function setStatus(on, txt) {
  $('dot').className = 'dot' + (on ? ' on' : '');
  $('statusTxt').textContent = txt;
}

function updatePhonesHeader(phonesList) {
  if (Array.isArray(phonesList)) {
    connectedPhones = [...new Set(phonesList.filter(Boolean))];
  }
  const p = $('phoneStatus');
  const txt = $('phoneTxt');
  if (!p || !txt) return;

  if (connectedPhones.length > 0) {
    p.classList.add('online');
    p.style.background = 'rgba(16, 185, 129, 0.16)';
    p.style.borderColor = 'rgba(16, 185, 129, 0.45)';
    p.style.color = '#34d399';
    if (connectedPhones.length === 1) {
      txt.textContent = `📱 ${connectedPhones[0]} (Terhubung)`;
    } else {
      txt.textContent = `📱 ${connectedPhones.length} HP: ${connectedPhones.join(', ')}`;
    }
  } else {
    p.classList.remove('online');
    p.style.background = 'rgba(99, 102, 241, 0.12)';
    p.style.borderColor = 'rgba(99, 102, 241, 0.28)';
    p.style.color = '#a5b4fc';
    txt.textContent = 'HP belum tersambung';
  }
}

function connect(onOpen) {
  if (ws && (ws.readyState === 0 || ws.readyState === 1)) {
    onOpen && onOpen();
    return;
  }
  clearTimeout(reconnectTimer);
  const proto = location.protocol === 'https:' ? 'wss' : 'ws';
  ws = new WebSocket(proto + '://' + location.host);
  
  ws.onopen = () => { 
    setStatus(true, 'Server Terhubung'); 
    const savedS = sessionStorage.getItem('scanner_session');
    const savedH = sessionStorage.getItem('scanner_hostKey');
    const savedJ = sessionStorage.getItem('scanner_joinKey');
    if (savedS && savedH) {
      session = savedS;
      hostKey = savedH;
      joinKey = savedJ || '';
      ws.send(JSON.stringify({ 
        type: 'register', 
        role: 'laptop', 
        session: savedS, 
        hostKey: savedH 
      }));
    }
    onOpen && onOpen(); 
  };

  ws.onclose = () => {
    setStatus(false, 'Terputus — Menghubungkan...');
    connectedPhones = [];
    updatePhonesHeader([]);
    reconnectTimer = setTimeout(() => connect(), 2500);
  };

  ws.onmessage = (ev) => {
    let m; try { m = JSON.parse(ev.data); } catch { return; }
    
    if (m.type === 'created') { 
      session = m.session;
      hostKey = m.hostKey;
      joinKey = m.joinKey;
      sessionStorage.setItem('scanner_session', session);
      sessionStorage.setItem('scanner_hostKey', hostKey);
      sessionStorage.setItem('scanner_joinKey', joinKey);
      connectedPhones = [];
      updatePhonesHeader([]);
      afterSession(); 
    }

    if (m.type === 'registered' && m.role === 'laptop') {
      afterSession();
      toast('Sesi terhubung: ' + m.session);
    }

    if (m.type === 'phone-joined') {
      const devName = m.name || 'HP';
      if (!connectedPhones.includes(devName)) {
        connectedPhones.push(devName);
      }
      updatePhonesHeader(connectedPhones);
      toast(`📱 ${devName} terhubung!`);
      beep('scan');
    }

    if (m.type === 'phone-left') {
      const devName = m.name || 'HP';
      connectedPhones = connectedPhones.filter(n => n !== devName);
      updatePhonesHeader(connectedPhones);
      toast(`📴 ${devName} terputus`);
    }

    if (m.type === 'phones-update') {
      updatePhonesHeader(m.phones || []);
    }

    // Modal Permintaan Izin Perangkat
    if (m.type === 'phone-request') {
      activeReqId = m.reqId;
      $('reqDeviceName').textContent = m.name || 'Smartphone Baru';
      $('approvalModal').classList.add('show');
      beep('alert');
      toast('⚠️ Permintaan izin dari: ' + (m.name || 'Perangkat HP'));
    }

    if (m.type === 'scan') {
      const devName = m.from || 'HP';
      if (devName && !connectedPhones.includes(devName)) {
        connectedPhones.push(devName);
        updatePhonesHeader(connectedPhones);
      }
      addRow(m);
    }

    if (m.type === 'error') {
      if (m.message && (m.message.includes('tidak ditemukan') || m.message.includes('kedaluwarsa'))) {
        sessionStorage.removeItem('scanner_session');
        sessionStorage.removeItem('scanner_hostKey');
        sessionStorage.removeItem('scanner_joinKey');
        session = null;
        hostKey = null;
        joinKey = null;
        $('code').textContent = '------';
        $('linkWrap').style.display = 'none';
        $('qrcode').innerHTML = '<div class="qr-placeholder"><svg viewBox="0 0 24 24"><rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/><rect x="3" y="14" width="7" height="7"/></svg><span>Klik "Buat Kode Sesi Baru" untuk memulai</span></div>';
        connectedPhones = [];
        updatePhonesHeader([]);
      }
      toast('❌ ' + m.message);
    }
  };
}

function afterSession() {
  $('code').textContent = session;
  const url = location.origin + '/scan.html?session=' + session + '&key=' + joinKey;
  $('linkWrap').style.display = 'flex';
  $('linkTxt').textContent = url;
  $('qrcode').innerHTML = '';
  
  if (typeof QRCode === 'undefined') {
    $('qrcode').innerHTML = '<div class="qr-placeholder" style="color:#ef4444"><span>QR Generator gagal dimuat. Buka link manual di HP.</span></div>';
    return;
  }
  new QRCode($('qrcode'), { 
    text: url, 
    width: 210, 
    height: 210,
    colorDark: '#0f172a',
    colorLight: '#ffffff',
    correctLevel: QRCode.CorrectLevel.H
  });
  toast('Sesi aman aktif: ' + session);
  syncAutoTypeConfig();
}

function syncAutoTypeConfig() {
  const elOpt = $('optAutoType');
  const elSuffix = $('selSuffix');
  const badge = $('autoTypeBadge');
  if (!elOpt || !elSuffix) return;
  const enabled = elOpt.checked;
  const suffix = elSuffix.value;

  if (badge) {
    if (enabled) {
      badge.className = 'badge-status-auto';
      badge.textContent = '🟢 AKTIF';
    } else {
      badge.className = 'badge-status-auto off';
      badge.textContent = '⚪ NONAKTIF';
    }
  }

  if (ws && ws.readyState === 1) {
    ws.send(JSON.stringify({
      type: 'config-autotype',
      session: session,
      enabled: enabled,
      suffix: suffix
    }));
  }
}

if ($('optAutoType')) {
  $('optAutoType').addEventListener('change', () => {
    syncAutoTypeConfig();
    toast($('optAutoType').checked ? '⚡ Ketik otomatis: DIAKTIFKAN' : 'Ketik otomatis: DINONAKTIFKAN');
  });
}

if ($('selSuffix')) {
  $('selSuffix').addEventListener('change', () => {
    syncAutoTypeConfig();
    toast('Tombol akhir scan: ' + $('selSuffix').options[$('selSuffix').selectedIndex].text);
  });
}

// Modal Handlers
$('btnModalApprove').onclick = () => {
  if (!activeReqId || !ws || ws.readyState !== 1) return;
  const devName = $('reqDeviceName').textContent || 'HP';
  if (!connectedPhones.includes(devName)) {
    connectedPhones.push(devName);
    updatePhonesHeader(connectedPhones);
  }
  ws.send(JSON.stringify({ type: 'approve-phone', reqId: activeReqId }));
  $('approvalModal').classList.remove('show');
  toast(`📱 Perangkat ${devName} telah diizinkan!`);
  activeReqId = null;
};

$('btnModalReject').onclick = () => {
  if (!activeReqId || !ws || ws.readyState !== 1) return;
  ws.send(JSON.stringify({ type: 'reject-phone', reqId: activeReqId }));
  $('approvalModal').classList.remove('show');
  toast('Perangkat ditolak.');
  activeReqId = null;
};

$('btnCreate').onclick = () => {
  sessionStorage.removeItem('scanner_session');
  sessionStorage.removeItem('scanner_hostKey');
  sessionStorage.removeItem('scanner_joinKey');
  if (!ws || ws.readyState !== 1) { 
    connect(() => ws.send(JSON.stringify({ type: 'create' }))); 
  } else {
    ws.send(JSON.stringify({ type: 'create' }));
  }
};

$('btnCopyCode').onclick = () => {
  if (!session) return toast('Buat sesi terlebih dahulu!');
  navigator.clipboard.writeText(session).then(() => toast('Kode ' + session + ' disalin!'));
};

$('btnOpenTab').onclick = () => {
  if (!session) return;
  const url = location.origin + '/scan.html?session=' + session + (joinKey ? '&key=' + joinKey : '');
  window.open(url, '_blank');
};

$('btnCopyLatest').onclick = () => {
  if (!lastText) return;
  navigator.clipboard.writeText(lastText).then(() => toast('Hasil scan terbaru disalin!'));
};

function addRow(m) {
  if ($('optDedup').checked && m.text === lastText) return;
  lastText = m.text;
  const timeStr = new Date(m.at).toLocaleTimeString('id-ID');
  const fmtStr = m.format || '-';
  const fromStr = m.from || 'HP';
  
  rows.unshift({ 
    at: timeStr, 
    timestamp: m.at,
    text: m.text, 
    format: fmtStr,
    from: fromStr
  });

  render(); 
  beep('scan');

  // Update Stats
  $('statCount').textContent = rows.length;
  $('statTime').textContent = timeStr;
  $('statFormat').textContent = fmtStr;

  // Flash Latest Box
  const lv = $('latest'); 
  lv.classList.remove('show'); 
  void lv.offsetWidth; 
  lv.classList.add('show');
  $('latestVal').textContent = m.text;
  $('latestMeta').textContent = `${new Date(m.at).toLocaleString('id-ID')} • ${fmtStr} • 📱 ${fromStr}`;

  // Focus Input handling
  const fi = $('focusInput'); 
  fi.value = m.text;
  fi.focus(); 
  fi.select();
  try { fi.setRangeText(m.text); } catch (e) {}

  // Clipboard copy
  if ($('optCopy').checked && navigator.clipboard) {
    navigator.clipboard.writeText(m.text).catch(() => {});
  }
}

function render() {
  $('count').textContent = rows.length + ' hasil tersimpan';
  $('statCount').textContent = rows.length;
  
  const filtered = searchQuery ? rows.filter(r => 
    r.text.toLowerCase().includes(searchQuery) || 
    r.format.toLowerCase().includes(searchQuery) ||
    r.at.toLowerCase().includes(searchQuery) ||
    (r.from && r.from.toLowerCase().includes(searchQuery))
  ) : rows;

  if (filtered.length === 0) {
    $('tbody').innerHTML = `
      <tr>
        <td colspan="6">
          <div class="empty-state">
            <svg viewBox="0 0 24 24"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>
            <p>${rows.length === 0 ? 'Belum ada hasil scan masuk.' : 'Tidak ada hasil yang sesuai dengan filter pencarian.'}</p>
          </div>
        </td>
      </tr>
    `;
    return;
  }

  $('tbody').innerHTML = filtered.map((r, i) => {
    const originalIndex = rows.indexOf(r);
    const num = rows.length - originalIndex;
    return `
      <tr>
        <td style="color: var(--text-muted); font-size: 12px; font-variant-numeric: tabular-nums;">${num}</td>
        <td style="white-space: nowrap; color: var(--text-secondary); font-size: 12px;">${r.at}</td>
        <td>
          <span class="badge-phone" title="${escapeHtml(r.from || 'HP')}">
            <svg viewBox="0 0 24 24"><rect x="5" y="2" width="14" height="20" rx="2" ry="2"/><line x1="12" y1="18" x2="12.01" y2="18"/></svg>
            ${escapeHtml(r.from || 'HP')}
          </span>
        </td>
        <td class="td-code">${escapeHtml(r.text)}</td>
        <td><span class="badge-fmt">${escapeHtml(r.format)}</span></td>
        <td style="text-align: right;">
          <button class="btn-mini" data-copy="${originalIndex}" title="Salin kode ini">
            <svg viewBox="0 0 24 24"><rect x="9" y="9" width="13" height="13" rx="2" ry="2"/><path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1"/></svg>
            Salin
          </button>
        </td>
      </tr>
    `;
  }).join('');
}

function escapeHtml(s) { 
  return String(s).replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c])); 
}

function copyOne(i) {
  if (!rows[i]) return;
  navigator.clipboard.writeText(rows[i].text).then(() => toast('Disalin: ' + rows[i].text));
}

// Delegasi klik untuk tombol "Salin" (pengganti inline onclick — wajib untuk CSP tanpa 'unsafe-inline')
$('tbody').addEventListener('click', (e) => {
  const btn = e.target.closest('[data-copy]');
  if (btn) copyOne(Number(btn.getAttribute('data-copy')));
});

$('tableFilter').addEventListener('input', (e) => {
  searchQuery = e.target.value.trim().toLowerCase();
  render();
});

// Cegah CSV formula injection: nilai yang diawali =, +, -, @, Tab, CR di-prefix '
function csvField(v) {
  let s = String(v == null ? '' : v).replace(/"/g, '""');
  if (/^[=+\-@\t\r]/.test(s)) s = "'" + s;
  return '"' + s + '"';
}

$('btnCSV').onclick = () => {
  if (!rows.length) return toast('Belum ada data untuk diexport');
  const csv = 'No,Waktu,Perangkat,Hasil,Format\n' + [...rows].reverse().map((r, i) => 
    [i + 1, r.at, r.from || 'HP', r.text, r.format].map(csvField).join(',')
  ).join('\n');
  const a = document.createElement('a');
  a.href = URL.createObjectURL(new Blob([csv], { type: 'text/csv;charset=utf-8;' }));
  a.download = 'hasil-scan-' + Date.now() + '.csv'; 
  a.click();
  toast('File CSV berhasil didownload!');
};

$('btnCopyAll').onclick = () => {
  if (!rows.length) return toast('Belum ada data untuk disalin');
  const allText = [...rows].reverse().map(r => `${r.text}	${r.from || 'HP'}`).join('\n');
  navigator.clipboard.writeText(allText).then(() => toast(rows.length + ' baris disalin ke clipboard!'));
};

$('btnClear').onclick = () => { 
  if (!rows.length) return;
  if (!confirm('Apakah Anda yakin ingin menghapus semua riwayat scan?')) return;
  rows = []; 
  lastText = ''; 
  render(); 
  $('latest').classList.remove('show');
  $('statCount').textContent = '0';
  $('statTime').textContent = '-';
  $('statFormat').textContent = '-';
  toast('Riwayat telah dibersihkan');
};

connect();
