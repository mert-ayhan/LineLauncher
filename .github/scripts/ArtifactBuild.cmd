@echo off
cd ../..
pushd "%~dp0"
cd bin/Release
del /f *.xml *.config *.pdb *.exe
move .\Obfuscator_Output\line-launcher.exe
rd /s /q Obfuscator_Output
cd ../..
powershell Compress-7Zip "bin/Release" -ArchiveFileName "LineLauncher.zip" -Format Zip
:exit
popd
@echo on