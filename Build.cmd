@echo off
pushd "%~dp0"
if exist bin rd /s /q bin
"%programfiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" /p:Configuration=Release /p:Platform="Any CPU"
:exit
popd
@echo on