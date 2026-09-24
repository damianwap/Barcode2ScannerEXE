# Build Script untuk ScanKilat Installer
# Menjalankan kompilasi Release .NET dan membungkusnya menjadi setup installer via Inno Setup

param (
    [switch]$SkipBuild = $false,
    [switch]$AutoInstallInno = $true
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $ScriptDir

Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "       BUILD & PACKAGING INSTALLER SCANKILAT          " -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Cyan

# 1. Cari MSBuild
if (-not $SkipBuild) {
    Write-Host "`n[1/3] Memeriksa compiler MSBuild..." -ForegroundColor Yellow
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    $msbuild = $null
    if (Test-Path $vswhere) {
        $msbuild = & $vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | Select-Object -First 1
    }
    if (-not $msbuild -or -not (Test-Path $msbuild)) {
        $msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
    }

    if (-not (Test-Path $msbuild)) {
        Write-Error "MSBuild tidak ditemukan! Pastikan Visual Studio / Build Tools terpasang."
    }

    Write-Host "Mengompilasi solusi dalam konfigurasi Release..." -ForegroundColor Green
    & $msbuild "$ScriptDir\ScanKilat.sln" /p:Configuration=Release /verbosity:minimal /nologo
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Kompilasi MSBuild gagal dengan exit code $LASTEXITCODE"
    }
    Write-Host "Kompilasi Release berhasil." -ForegroundColor Green
} else {
    Write-Host "`n[1/3] Melewati kompilasi binary (menggunakan bin\Release yang ada)..." -ForegroundColor Gray
}

# 2. Cari Inno Setup Compiler (iscc.exe)
Write-Host "`n[2/3] Memeriksa compiler Inno Setup (iscc.exe)..." -ForegroundColor Yellow
$isccPaths = @(
    "$env:LocalAppData\Programs\Inno Setup 6\ISCC.exe",
    "C:\Program Files (x86)\Inno Setup 6\iscc.exe",
    "C:\Program Files\Inno Setup 6\iscc.exe",
    "C:\Program Files (x86)\Inno Setup 5\iscc.exe",
    "C:\Program Files\Inno Setup 5\iscc.exe"
)

$iscc = $null
foreach ($p in $isccPaths) {
    if (Test-Path $p) {
        $iscc = $p
        break
    }
}

if (-not $iscc) {
    $cmd = Get-Command iscc.exe -ErrorAction SilentlyContinue
    if ($cmd) { $iscc = $cmd.Source }
}

if (-not $iscc) {
    if ($AutoInstallInno) {
        Write-Host "Inno Setup belum terpasang. Mengunduh dan memasang via winget..." -ForegroundColor Yellow
        winget install JRSoftware.InnoSetup -e --silent --accept-package-agreements --accept-source-agreements
        
        # Cek ulang setelah instalasi
        foreach ($p in $isccPaths) {
            if (Test-Path $p) {
                $iscc = $p
                break
            }
        }
    }
    
    if (-not $iscc) {
        Write-Error "Inno Setup (iscc.exe) tidak ditemukan. Silakan pasang via 'winget install JRSoftware.InnoSetup' atau unduh dari https://jrsoftware.org/isinfo.php"
    }
}

Write-Host "Inno Setup Compiler ditemukan: $iscc" -ForegroundColor Green

# 3. Kompilasi Installer
Write-Host "`n[3/3] Membungkus installer dengan Inno Setup..." -ForegroundColor Yellow
$distDir = Join-Path $ScriptDir "dist"
if (-not (Test-Path $distDir)) {
    New-Item -ItemType Directory -Path $distDir | Out-Null
}

& $iscc "$ScriptDir\installer.iss"
if ($LASTEXITCODE -ne 0) {
    Write-Error "Pembuatan installer gagal dengan exit code $LASTEXITCODE"
}

$installerExe = Join-Path $distDir "ScanKilat_Setup_v1.0.0.exe"
if (Test-Path $installerExe) {
    $fileItem = Get-Item $installerExe
    $sizeMb = [math]::Round($fileItem.Length / 1MB, 2)
    $hash = (Get-FileHash -Path $installerExe -Algorithm SHA256).Hash
    
    Write-Host "`n==========================================================" -ForegroundColor Green
    Write-Host "        INSTALLER BERHASIL DIBUAT DENGAN SUKSES!          " -ForegroundColor Green
    Write-Host "==========================================================" -ForegroundColor Green
    Write-Host "File Output : $installerExe" -ForegroundColor Cyan
    Write-Host "Ukuran File : $sizeMb MB" -ForegroundColor Cyan
    Write-Host "SHA-256     : $hash" -ForegroundColor Cyan
    Write-Host "==========================================================" -ForegroundColor Green
} else {
    Write-Error "File output $installerExe tidak ditemukan setelah proses build selesai."
}
