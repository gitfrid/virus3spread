Get-FileHash "C:\Github\virus3spread\virus3spreadSetup\Release\setup.exe" | Format-List | Out-File -FilePath "C:\Github\virus3spread\virus3spreadSetup\Virus3spreadHash.txt"
Get-FileHash "C:\Github\virus3spread\virus3spreadSetup\Release\virus3spreadSetup.msi" | Format-List | Out-File -Append -FilePath "C:\Github\virus3spread\virus3spreadSetup\Virus3spreadHash.txt"
pause
