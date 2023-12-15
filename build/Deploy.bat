SET address=192.168.1.25
SET password=pocketpc2002
SET product=RaspiRobot
CD ..
CD Source
RD ..\..\artifacts /s /q
dotnet publish %product% -c Release -r linux-arm --self-contained -o ../../artifacts/publish/raspberry /p:DebugType=None /p:DebugSymbols=false
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl stop %product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/%product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/%product%/bin"
pscp -pw %password% -r ../../artifacts/publish/raspberry/* pi@%address%:/home/pi/%product%/bin
plink -ssh pi@%address% -pw %password% -no-antispoof "chmod +x /home/pi/%product%/bin/%product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl start %product%"
PAUSE