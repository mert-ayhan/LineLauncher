@echo off
pushd "%~dp0"
cd bin/Release
del /f *.xml
del /f *.config
del /f *.pdb
del /f *.exe
move .\Obfuscator_Output\line-launcher.exe
rmdir Obfuscator_Output\
cd ../..
powershell Compress-7Zip "bin/Release" -ArchiveFileName "LineLauncher.zip" -Format Zip
:exit
popd
@echo on