@echo off

echo Installing npm packages...
pushd ..\Source\Frontend\raspi-robot-app
call npm install
popd
echo Done.

echo Restoring NuGet packages...
pushd ..\Source\Backend
dotnet restore
popd
echo Done.

PAUSE