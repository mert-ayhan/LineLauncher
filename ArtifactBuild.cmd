@echo off
pushd "%~dp0"
powershell Compress-7Zip "Release" -ArchiveFileName "LineLauncherX86.zip" -Format Zip
powershell Compress-7Zip "x64\Release" -ArchiveFileName "LineLauncherX64.zip" -Format Zip
:exit
popd
@echo on