﻿@echo off
echo *** Building Microsoft.Bot.Builder.Extensions.Azure.Telemetry.QueueStorageWriter
setlocal
setlocal enabledelayedexpansion
setlocal enableextensions
set errorlevel=0
mkdir ..\nuget
erase /s ..\nuget\Microsoft.Bot.Builder.Extensions.Azure.Telemetry.QueueStorageWriter*nupkg
msbuild /property:Configuration=release Microsoft.Bot.Builder.Extensions.Azure.Telemetry.QueueStorageWriter.csproj 
for /f %%v in ('powershell -noprofile "(Get-Command .\bin\release\Microsoft.Bot.Builder.Extensions.Telemetry.dll).FileVersionInfo.FileVersion"') do set telemetry=%%v
for /f %%v in ('powershell -noprofile "(Get-Command .\bin\release\Microsoft.Bot.Builder.Extensions.Azure.Telemetry.QueueStorageWriter.dll).FileVersionInfo.FileVersion"') do set version=%%v
..\..\packages\NuGet.CommandLine.3.4.3\tools\NuGet.exe pack Microsoft.Bot.Builder.Extensions.Azure.Telemetry.QueueStorageWriter.nuspec -symbols -properties version=%version%;telemetry=%telemetry% -OutputDirectory ..\nuget
echo *** Finished building Microsoft.Bot.Builder.Extensions.Azure.Telemetry.QueueStorageWriter

