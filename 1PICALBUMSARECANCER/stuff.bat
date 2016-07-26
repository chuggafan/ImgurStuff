
@echo off
./nuget.exe restore -SolutionDirectory ../1PICALBUMSARECANCER.sln

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBUILD.exe ..\1PICALBUMSARECANCER.sln
echo This has important things when you continue, be sure to input the imgur application ID next, and you'll be fine
pause
cls
.\bin\Debug\1PICALBUMSARECANCER.exe
::This stuff SHOULD work, PLEASE tell me if it doesn't, an EXE file will be in the /bin/DEBUG output::