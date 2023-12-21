@echo off
cd ..
cd Source

echo Installing npm packages...
cd Frontend/raspi-robot-app
call npm install
cd ../..
echo Done.

echo Restoring NuGet packages...
cd Backend/
dotnet restore
cd ..
echo Done.

PAUSE