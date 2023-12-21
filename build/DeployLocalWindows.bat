@echo off
cd ..\Source
if exist ..\artifacts rd ..\artifacts /s /q

echo Build Angular frontend...
cd Frontend\raspi-robot-app
call ng build --output-path ../../../artifacts/frontend --delete-output-path
cd ..\..
echo Angular frontend built.

echo Build .NET backend...
cd Backend
dotnet publish RaspiRobot -c Release -r win-x64 --self-contained -o ../../artifacts/publish/windows /p:DebugType=None /p:DebugSymbols=false
cd ..
echo .NET backend built.

echo Copying frontend to backend for delivery...
copy ..\artifacts\frontend\browser\*.* ..\artifacts\publish\windows\wwwroot
echo Finished.

pause