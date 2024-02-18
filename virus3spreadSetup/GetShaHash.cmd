SET PowerShellDir=C:\Windows\System32\WindowsPowerShell\v1.0
CD /D "%PowerShellDir%"
Powershell -ExecutionPolicy Bypass -Command "& 'C:\Github\virus3spread\virus3spreadSetup\GetHash.ps1'"
pause