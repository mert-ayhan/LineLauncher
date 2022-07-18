@echo off
pushd "%~dp0"
powershell Compress-7Zip "Release" -ArchiveFileName "LineLauncher.zip" -Format Zip
:exit
popd
@echo on