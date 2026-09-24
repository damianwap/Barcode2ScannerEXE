#define MyAppName "ScanKilat"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "ViciLaptop"
#define MyAppURL "https://github.com/damianwap/Barcode2ScannerEXE"
#define MyAppExeName "ScanKilat.exe"
#define MyAppId "{{A8D4E15C-35D0-49BA-8D47-815C33E3E4BD}"

[Setup]
AppId={#MyAppId}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=dist
OutputBaseFilename=ScanKilat_Setup_v1.0.0
SetupIconFile=app.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
UsedUserAreasWarning=no
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "startupicon"; Description: "Jalankan otomatis saat Windows dinyalakan (Run at Startup)"; GroupDescription: "Pengaturan Tambahan:"; Flags: unchecked

[Files]
; Exe tunggal: semua dependensi (QRCoder.dll, app.ico, public/) sudah di-embed di dalam exe
Source: "bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\ScanKilat.exe.config"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{commonstartup}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: startupicon

[Run]
; Otomatis daftarkan aturan Windows Defender Firewall untuk Port 3443 saat install
Filename: "netsh.exe"; Parameters: "advfirewall firewall add rule name=""ScanKilat Port 3443"" dir=in action=allow protocol=TCP localport=3443 profile=private,domain"; Flags: runhidden
; Opsi jalankan aplikasi setelah instalasi selesai
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallRun]
; Bersihkan aturan firewall saat aplikasi di-uninstall
Filename: "netsh.exe"; Parameters: "advfirewall firewall delete rule name=""ScanKilat Port 3443"""; RunOnceId: "DelScanKilatFirewallRule"; Flags: runhidden
