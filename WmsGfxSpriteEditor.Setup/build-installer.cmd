@echo off
rem Publishes the app (self-contained, x86) and compiles the Inno Setup installer.
setlocal
cd /d "%~dp0"

if exist obj\publish rmdir /s /q obj\publish
dotnet publish ..\WmsGfxSpriteEditor\WmsGfxSpriteEditor.csproj -c Release -r win-x86 --self-contained true -p:Platform=x86 -o obj\publish || exit /b 1

set ISCC=%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe
if not exist "%ISCC%" set ISCC=%ProgramFiles%\Inno Setup 6\ISCC.exe
if not exist "%ISCC%" set ISCC=%LocalAppData%\Programs\Inno Setup 6\ISCC.exe
if not exist "%ISCC%" (
  echo Inno Setup 6 not found. Install it from https://jrsoftware.org/isdl.php
  exit /b 1
)
"%ISCC%" WmsGfxSpriteEditor.iss
