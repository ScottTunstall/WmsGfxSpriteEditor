; Inno Setup script. Build with build-installer.cmd (needs Inno Setup 6: https://jrsoftware.org/isinfo.php)

#define AppName "Williams Graphics Sprite Editor"
#define AppVersion "1.0.0"
#define AppPublisher "Scott Tunstall"
#define AppExe "WmsGfxSpriteEditor.exe"

[Setup]
AppId={{51608157-0C9C-4DA8-8AC3-759483B0726E}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
UninstallDisplayIcon={app}\{#AppExe}
SetupIconFile=..\WmsGfxSpriteEditor\hulk-fancy.ico
OutputDir=bin
OutputBaseFilename=WmsGfxSpriteEditor.Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=dialog

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; Flags: unchecked

[Files]
Source: "obj\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#AppName}"; Filename: "{app}\{#AppExe}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "Launch {#AppName}"; Flags: nowait postinstall skipifsilent
