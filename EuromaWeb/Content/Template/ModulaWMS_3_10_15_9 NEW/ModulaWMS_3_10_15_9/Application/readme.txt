Elenco file di installazione:


---------------------
- Microsoft Framework 1.1
File: dotnetfx.exe
---------------------
- Microsoft Framework SP1
File: NDP1.1sp1-KB867460-X86.exe
---------------------
- Microsoft .NET Framework versione 2.0 Redistributable Package (x86) Inglese
File:    NetFrameWork20x86.exe (Original name dotnetfx.exe)
Comando: NetFrameWork20x86.exe /q:a /c:"install.exe /q"
---------------------
- Microsoft .NET Framework versione 2.0 Redistributable Package (x64) Inglese
File:    NetFrameWork20x64.exe (Original name NetFx64.exe)
Comando: NetFrameWork20x64.exe /q:a /c:"install.exe /q"
---------------------
- Microsoft .NET Framework versione 2.0 Redistributable Package (IA64) Inglese
File:    NetFrameWork20IA64.exe (Original name NetFx64.exe)
Comando: NetFrameWork20IA64.exe /q:a /c:"install.exe /q"
---------------------
- Microsoft .NET Framework 3.5 Service Pack 1
File:    dotnetfx35sp1.exe
Comando: dotnetfx35sp1.exe
---------------------
- Microsoft Core XML Services (MSXML) 6.0
File:    msxml6.msi
Comando: msiexec /i "\<Percorso>\msxml6.msi" /passive
---------------------
- Windows Installer 3.1
File:    WindowsInstaller-KB893803-v2-x86.exe
Comando: WindowsInstaller-KB893803-v2-x86.exe /passive /norestart
- Windows Installer 4.5
For Windows Vista, Windows Vista Service Pack 1 and Windows Server 2008:
x86  Platform: Windows6.0-KB942288-v2-x86.msu
x64  Platform: Windows6.0-KB942288-v2-x64.msu
IA64 Platform: Windows6.0-KB942288-v2-ia64.msu
For Windows XP Service Pack 2 and Windows XP Service Pack 3 (32-bit platforms):
x86  Platform: WindowsXP-KB942288-v3-x86.exe
For Windows Server 2003 Service Pack 1, Windows Server 2003 Service Pack 2 and Windows XP 64-bit Editions:
x86  Platform: WindowsServer2003-KB942288-v4-x86.exe
x64  Platform: WindowsServer2003-KB942288-v4-x64.exe
IA64 Platform: WindowsServer2003-KB942288-v4-ia64.exe
Comando: FILE.EXE /passive /norestart
Comando: wusa.exe FILE.MSU  /quiet /norestart
---------------------
-- Windows PowerShell 1.0
-- http://www.microsoft.com/windowsserver2003/technologies/management/powershell/download.mspx
Windows PowerShell 1.0 English-Language Installation Package for Windows XP (KB926139) 
  WindowsXP-KB926139-v2-x86-ENU.exe
Windows PowerShell 1.0 English-Language Installation Package for Windows XP x64 Edition (KB926139) 
  WindowsServer2003.WindowsXP-KB926139-v2-x64-ENU.exe

Windows PowerShell 1.0 English-Language Installation Package for Windows Server 2003 (KB926139) 
  WindowsServer2003-KB926139-v2-x86-ENU.exe
Windows PowerShell 1.0 English-Language Installation Package for Windows Server 2003 x64 Edition (KB926139) 
  WindowsServer2003.WindowsXP-KB926139-v2-x64-ENU.exe
Windows PowerShell 1.0 English-Language Installation Package for Windows Server 2003 for Itanium-based Systems (KB926139) 
  WindowsServer2003-KB926139-v2-ia64-ENU.exe

For Windows Server 2008 where it is available as an optional component on the installation disk or via Server Manager
---------------------
- Microsoft SQL Server 2008 R2 Express Edition (Database with Management Tools) 
Versione 10.50.1600.1
32-bit 
File:    SQLEXPRWT_x86_ENU.exe
Date Published: 
64-bit 
File:    SQLEXPRWT_x64_ENU.exe
Date Published: 
Language:       English
Comando:        SQLEXPRWT_x86_ENU.exe /QS /INDICATEPROGRESS /HIDECONSOLE /IACCEPTSQLSERVERLICENSETERMS /ACTION=INSTALL /FEATURES=SQLEngine,SSMS /INSTANCENAME=SQLEXPRESS /TCPENABLED=1 /ERRORREPORTING=0 /SQMREPORTING=0 /SQLSVCSTARTUPTYPE=Automatic /SQLCOLLATION=Latin1_General_100_CI_AS /SECURITYMODE=SQL /SAPWD="Sys73#m0d" /SQLSVCACCOUNT="NT AUTHORITY\SYSTEM" 
---------------------
- Microsoft SQL Server Management Studio Express Service Pack 3
File:           SQLServer2005_SSMSEE.msi
Version:        9.00.4035
Date Published: 12/15/2008
Language:       English
Comando: msiexec /i "\<Percorso>\SQLServer2005_SSMSEE.msi" /passive
---------------------
- Visual FoxPro ODBC Driver
File:    VFPODBC.msi
Comando: msiexec /i "\<Percorso>\VFPODBC.msi" /passive
---------------------
- Programma per condividere le cartelle
File:    RMTSHARE.EXE
Comando: RMTSHARE.EXE \\<Nome PC>\\InstallSystore="C:\SystemLogistics\InstallSystore" /grant Everyone:r /REMARK:"Shared directory for SyStore installation."