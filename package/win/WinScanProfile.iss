#define AppName        GetStringFileInfo('..\..\bin\WinScanProfile.exe', 'ProductName')
#define AppVersion     GetStringFileInfo('..\..\bin\WinScanProfile.exe', 'ProductVersion')
#define AppFileVersion GetStringFileInfo('..\..\bin\WinScanProfile.exe', 'FileVersion')
#define AppCompany     GetStringFileInfo('..\..\bin\WinScanProfile.exe', 'CompanyName')
#define AppCopyright   GetStringFileInfo('..\..\bin\WinScanProfile.exe', 'LegalCopyright')
#define AppBase        LowerCase(StringChange(AppName, ' ', ''))
#define AppVersion3    Copy(AppVersion, 1, RPos('.', AppVersion) - 1)


[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppCompany}
AppPublisherURL=https://www.medo64.com/{#AppBase}/
AppCopyright={#AppCopyright}
VersionInfoProductVersion={#AppVersion}
VersionInfoProductTextVersion={#AppVersion3}
VersionInfoVersion={#AppFileVersion}
DefaultDirName={autopf}\{#AppCompany}\{#AppName}
OutputBaseFilename=setup
SourceDir=..\..\bin
OutputDir=..\dist
AppId=JosipMedved_WinScanProfile
CloseApplications="yes"
RestartApplications="no"
AppMutex=JosipMedved_WinScanProfile
UninstallDisplayIcon={app}\WinScanProfile.exe
AlwaysShowComponentsList=no
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
MergeDuplicateFiles=yes
MinVersion=0,6.0
PrivilegesRequired=lowest
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=no
DisableWelcomePage=yes
LicenseFile=../package/win/License.rtf


[Messages]
SetupAppTitle=Setup {#AppName} {#AppVersion3}
SetupWindowTitle=Setup {#AppName} {#AppVersion3}
BeveledLabel=medo64.com

[Dirs]
Name: "{userappdata}\Josip Medved\WinScan Profile";  Flags: uninsalwaysuninstall

[Files]
Source: "WinScanProfile.exe";                       DestDir: "{app}";  Flags: ignoreversion;
Source: "..\README.md";   DestName: "README.txt";   DestDir: "{app}";  Flags: overwritereadonly uninsremovereadonly;  Attribs: readonly;
Source: "..\LICENSE.md";  DestName: "LICENSE.txt";  DestDir: "{app}";  Flags: overwritereadonly uninsremovereadonly;  Attribs: readonly;

[Icons]
Name: "{userstartmenu}\WinScan Profile";  Filename: "{app}\WinScanProfile.exe"

[Run]
Description: "Launch application now";  Filename: "{app}\WinScanProfile.exe";  Flags: postinstall nowait skipifsilent runasoriginaluser shellexec
Description: "View ReadMe.txt";         Filename: "{app}\ReadMe.txt";          Flags: postinstall nowait skipifsilent runasoriginaluser shellexec unchecked


[Code]

procedure InitializeWizard;
begin
  WizardForm.LicenseAcceptedRadio.Checked := True;
end;


function PrepareToInstall(var NeedsRestart: Boolean): String;
var
    ResultCode: Integer;
begin
    Exec(ExpandConstant('{sys}\taskkill.exe'), '/f /im WinScanProfile.exe', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    Result := Result;
end;
