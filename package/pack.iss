; 汉化：MonKeyDu QQ:565887
; 由 Inno Setup 脚本向导 v6.3.1 生成的脚本.
; 有关创建 INNO SETUP 脚本文件的详细信息，请参阅帮助文件！!

#define MyAppName "ElectCell"
#define MyAppVersion "1.8"
#define MyAppExeName "ElectCell-HMI.exe"

[Setup]
; 注意：AppId 的值唯一标识此应用程序.不要在其他应用程序的安装程序中使用相同的 AppId 值.
; (若要生成新的 GUID，请单击"工具" - "生成 GUID").
AppId={{24A8E85B-5EB6-410D-91AB-920C7906E8C7}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
DefaultDirName=C:\{#MyAppName}
DisableProgramGroupPage=yes
; 取消下列注释行，在非管理员安装模式下运行(仅为当前用户安装.)
;PrivilegesRequired=lowest
OutputDir=..\package
OutputBaseFilename=ElectCell
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "chs"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\case1\*"; DestDir: "{app}\case1\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\res\*"; DestDir: "{app}\res\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\aeSLN.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\aeTool.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\case_path.csv"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\ElectCell-HMI.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\ElectCell-HMI.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\ElectCell-HMI.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\ElectCell-HMI.sln"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\libifcoremd.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\libifcoremdd.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\libifportmd.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\libmmd.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\libmmdd.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\mailz\Desktop\ElectCell-HMI\svml_dispmd.dll"; DestDir: "{app}"; Flags: ignoreversion
; 注意:  在任何共享系统文件上不要使用 "Flags: ignoreversion"

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

