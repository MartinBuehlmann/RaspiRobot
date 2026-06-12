SET address=raspirobot.%USERDNSDOMAIN%
SET password=pocketpc2002
SET product=RaspiRobot
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/%product%"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/%product%/bin"
scp -r %product%.service pi@%address%:/lib/systemd/system/
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemd daemon-reload"
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl enable %product%"
