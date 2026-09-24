# Panduan & Spesifikasi Pembuatan Installer & Penanganan Windows SmartScreen (ScanKilat)

Dokumen ini berisi panduan teknis lengkap serta instruksi otomasi (dapat langsung dieksekusi oleh developer atau AI Agent) untuk mengemas **ScanKilat** menjadi file installer profesional (`.exe`) tunggal dan menangani filter keamanan **Windows Defender SmartScreen**.

---

## Bagian 1: Penjelasan Mendalam Mengenai Installer & Windows SmartScreen

### 1.1 Mengapa File Installer (.exe Setup) Wajib untuk Produk Komersial?
Mendistribusikan aplikasi Windows dalam bentuk folder `.zip` mentah memiliki banyak kelemahan di pasar komersial (terutama pengguna toko/kasir/UMKM):
1. **Kegagalan Ekstraksi Parsial:** Pengguna sering hanya menarik `ScanKilat.exe` ke Desktop tanpa menyertakan folder `public/` (web server) atau `QRCoder.dll`, yang mengakibatkan aplikasi langsung *crash* saat dibuka.
2. **Ketiadaan Shortcut Standar:** Tidak adanya icon otomatis di Desktop dan Start Menu membuat pengguna kesulitan menemukan aplikasi setelah komputer dinyalakan ulang.
3. **Konfigurasi Firewall Manual yang Rumit:** Pengguna awam sering kali tidak paham cara membuka port Windows Defender Firewall. Dengan installer, perintah `netsh advfirewall` dijalankan otomatis di balik layar (*silent*) saat instalasi berjalan, dan otomatis dibersihkan saat di-*uninstall*.
4. **Kesan Profesional & Uninstaller Bersih:** Aplikasi komersial wajib terdaftar di *Windows Settings > Apps & Features (Add or Remove Programs)* sehingga dapat dihapus dengan bersih.

### 1.2 Memahami Windows Defender SmartScreen
Saat pengguna mengunduh dan menjalankan file `.exe` baru di Windows 10/11, sering muncul jendela biru bertuliskan:
> **"Windows protected your PC / Microsoft Defender SmartScreen prevented an unrecognized app from starting"**

#### Mengapa Peringatan Ini Muncul?
* Windows SmartScreen bekerja berdasarkan **Sistem Reputasi (Trust & Reputation)**.
* Setiap file biner baru yang baru diunduh dari internet dan belum pernah dilihat oleh jutaan komputer lain akan dianggap *"unrecognized"* (belum dikenal), **meskipun file tersebut 100% bebas dari virus/malware**.

#### 2 Strategi Menangani Windows SmartScreen:

| Pendekatan | Biaya | Cara Kerja & Langkah | Cocok Untuk |
| :--- | :--- | :--- | :--- |
| **A. Edukasi Pengguna (Organic Reputation)** | **Gratis (Rp 0)** | 1. Berikan panduan visual 1-langkah di web/brosur: *"Klik **More info** (Info selengkapnya) -> Klik **Run anyway** (Tetap jalankan)"* (hanya perlu dilakukan 1x).<br>2. Seiring bertambahnya jumlah instalasi bersih (biasanya ~100–300 unduhan tanpa komplain malware), SmartScreen akan otomatis menganggap file tersebut bereputasi baik dan peringatan hilang dengan sendirinya. | Tahap Awal, Peluncuran MVP, UMKM, Penjualan Komunitas |
| **B. Sertifikat Code Signing Resmi (Instant Trust)** | Berbayar (~$150 – $350 / tahun) | 1. Membeli sertifikat **Code Signing** (OV atau EV) dari Otoritas Sertifikat (DigiCert, Sectigo, SSL.com).<br>2. Menandatangani file installer via `signtool.exe`. Sertifikat jenis **EV (Extended Validation)** langsung menaikkan reputasi sejak hari pertama tanpa pernah memunculkan layar biru SmartScreen. | Skala Komersial Penuh / Enterprise |

---

## Bagian 2: Panduan Eksekusi untuk AI Agent / Developer

Bagian ini dirancang agar dapat dieksekusi secara otonom oleh AI Agent atau developer baris demi baris.

### 2.1 File yang Telah Disediakan dalam Repositori
* **[`installer.iss`](installer.iss):** Skrip konfigurasi Inno Setup yang mengemas seluruh file `bin/Release`, folder `public`, icon aplikasi, pembuatan shortcut Desktop/Start Menu/Startup, dan registrasi port firewall 3443 otomatis.
* **[`build-installer.ps1`](build-installer.ps1):** Skrip PowerShell otomatis untuk mengompilasi Release .NET, mendeteksi/mengunduh Inno Setup, dan memproduksi file `dist/ScanKilat_Setup_v1.0.0.exe`.

---

### 2.2 Prasyarat Lingkungan (Prerequisites)
1. **Sistem Operasi:** Windows 10 atau Windows 11.
2. **MSBuild Engine:** Tersedia via Visual Studio 2019 / 2022 atau Visual Studio Build Tools.
3. **Inno Setup 6:** Kompiler installer (dapat dipasang otomatis melalui `winget`).

---

### 2.3 Langkah Eksekusi Otomatis (Single Command)

Buka terminal PowerShell pada direktori proyek dan jalankan:

```powershell
powershell -ExecutionPolicy Bypass -File .\build-installer.ps1
```

Skrip ini akan secara otomatis:
1. Menemukan MSBuild dan mengompilasi proyek dalam konfigurasi `Release`.
2. Memeriksa keberadaan Inno Setup compiler (`iscc.exe`). Jika belum terpasang, skrip akan otomatis memasangnya via `winget install JRSoftware.InnoSetup`.
3. Membangun installer ke dalam direktori `dist/`.
4. Menampilkan ukuran file dan checksum SHA-256 installer.

---

### 2.4 Langkah Eksekusi Manual (Step-by-Step)

Jika ingin menjalankan setiap tahapan secara terpisah:

#### Langkah 1: Kompilasi Binary Release
```powershell
& "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" ScanKilat.sln /p:Configuration=Release /verbosity:minimal
```

#### Langkah 2: Instalasi Inno Setup (Jika Belum Ada)
```powershell
winget install JRSoftware.InnoSetup -e --silent --accept-package-agreements --accept-source-agreements
```

#### Langkah 3: Kompilasi File Installer
```powershell
& "C:\Program Files (x86)\Inno Setup 6\iscc.exe" .\installer.iss
```
*Hasil installer akan berada di: `dist\ScanKilat_Setup_v1.0.0.exe`.*

---

### 2.5 (Opsional) Penandatanganan Digital (Code Signing)
Jika Anda telah memiliki sertifikat Code Signing (file `.pfx`):

```powershell
# Menggunakan signtool dari Windows SDK
$signtool = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.22621.0\x64\signtool.exe"
& $signtool sign /f "C:\path\to\certificate.pfx" /p "PasswordCert" /tr "http://timestamp.digicert.com" /td sha256 /fd sha256 "dist\ScanKilat_Setup_v1.0.0.exe"
```

---

## Bagian 3: Checklist Pengujian & Verifikasi (Quality Assurance)

Sebelum mendistribusikan installer ke pelanggan, AI Agent atau QA Tester wajib memverifikasi item berikut:

- [ ] **Instalasi Bersih (Clean Install):** Jalankan `ScanKilat_Setup_v1.0.0.exe`. Pastikan wizard berjalan lancar, meminta izin administrator, dan meletakkan file di `C:\Program Files (x86)\ScanKilat`.
- [ ] **Shortcut Desktop & Start Menu:** Pastikan icon `app.ico` muncul dengan benar di Desktop dan menu Start.
- [ ] **Integritas Aset Web:** Buka folder `C:\Program Files (x86)\ScanKilat\public` dan pastikan `scan.html`, `scan.js`, dan folder `vendor/` lengkap.
- [ ] **Verifikasi Aturan Firewall:** Buka PowerShell admin dan jalankan:
  ```powershell
  netsh advfirewall firewall show rule name="ScanKilat Port 3443"
  ```
  Pastikan aturan port 3443 TCP aktif berstatus `ALLOW`.
- [ ] **Uji Coba Auto-Type:** Buka aplikasi, hubungkan kamera HP via scan QR, arahkan kursor ke Notepad, dan lakukan scan. Pastikan teks muncul utuh beserta akhiran Enter/Tab.
- [ ] **Verifikasi Uninstaller:** Lakukan uninstall melalui *Control Panel > Programs and Features*. Pastikan seluruh file di Program Files terhapus dan aturan firewall dibersihkan.
