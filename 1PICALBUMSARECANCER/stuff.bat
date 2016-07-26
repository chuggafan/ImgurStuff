@echo off
start ./nuget.exe restore -SolutionDirectory ..\1PICALBUMSARECANCER.sln

start C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBUILD.exe ..\1PICALBUMSARECANCER.sln
echo goto this directory's bin/DEBUG folder for important stuffs!

::This stuff SHOULD work, PLEASE tell me if it doesn't, an EXE file will be in the /bin/DEBUG output::