# Panduan Google Sheets Real-Time

Setiap scan masuk otomatis dikirim ke Google Sheet via Apps Script webhook.

## 1. Buat Sheet
1. Buka https://sheets.google.com → spreadsheet baru.
2. Baris pertama (header): `Waktu | Perangkat | Barcode | Format`

## 2. Pasang Script
1. Di spreadsheet: **Extensions → Apps Script**.
2. Hapus isi editor, tempel kode ini:

```js
const SHEET_NAME = 'Sheet1';
const SHARED_KEY = ''; // opsional: isi string rahasia, misal 'abc123'

function doPost(e) {
  const data = JSON.parse(e.postData.contents);
  if (SHARED_KEY && data.key !== SHARED_KEY) {
    return ContentService.createTextOutput('forbidden').setMimeType(ContentService.MimeType.TEXT);
  }
  const sh = SpreadsheetApp.getActiveSpreadsheet().getSheetByName(SHEET_NAME);
  sh.appendRow([data.waktu || new Date(), data.perangkat || '', data.barcode || '', data.format || '']);
  return ContentService.createTextOutput('ok').setMimeType(ContentService.MimeType.TEXT);
}
```

3. **Deploy → New deployment → Web app**:
   - Execute as: **Me**
   - Who has access: **Anyone**
   - Deploy → salin URL `https://script.google.com/.../exec`.

## 3. Sambungkan Aplikasi
1. Di aplikasi klik **📊 Sheets Setup**, tempel URL `/exec` tadi.
2. Jika pakai `SHARED_KEY` di script, ganti logika `key` di `Form1.vb`
   (`PostToGoogleSheets`) agar mengirim key yang sama dengan field `key`.
   Default saat ini mengirim `CurrentJoinKey` — samakan `SHARED_KEY`-nya
   atau kosongkan agar tanpa verifikasi (hanya untuk jaringan terpercaya).

## Catatan
- Pengiriman dilakukan di background thread (timeout 8 detik), gagal kirim
  tidak memblokir scan — data tetap ada di tabel + real-time CSV/XLSX.
- Jangan commit URL webhook ke git — anggap seperti password.
