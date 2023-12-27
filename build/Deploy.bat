@echo off
set address=192.168.1.25
set password=pocketpc2002
set product=RaspiRobot

cd ..\Source
if exist ..\artifacts rd ..\artifacts /s /q

echo Build Angular frontend...
cd Frontend\raspi-robot-app
call ng build --output-path ../../../artifacts/frontend --delete-output-path
cd ..\..
echo Angular frontend built.

echo Build .NET backend...
cd Backend
dotnet publish %product% -c Release -r linux-arm --self-contained -o ../../artifacts/publish/raspberry /p:DebugType=None /p:DebugSymbols=false
cd ..
echo .NET backend built.

echo Copying frontend to backend for delivery...
copy ..\artifacts\frontend\*.* ..\artifacts\publish\raspberry\wwwroot
echo Frontend copied.

echo Deploying to Raspberry PI at '%address%'...
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl stop %product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/%product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/%product%/bin"
pscp -pw %password% -r ../artifacts/publish/raspberry/* pi@%address%:/home/pi/%product%/bin/
plink -ssh pi@%address% -pw %password% -no-antispoof "chmod +x /home/pi/%product%/bin/%product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl start %product%"
echo Finished.

pause
