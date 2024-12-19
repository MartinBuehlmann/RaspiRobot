# Install RaspiRobot as Service on Raspberry PI

This document describes how to configure Raspberry PI to run RaspiRobot as a service.

## Raspberry Pi Preparation

To run RaspiRobot, both SPI and I2C must be enabled.

```
sudo raspi-config
```

Then navigate to _3 Interface Options_ and enable _I4 SPI_ and _I5 I2C_.

Reboot the device.

## Step-By-Step
The following section describes step-by-step how to setup RaspiRobot as a service:

1. Add RaspiRobot to systemd
To create a systemd service, create a RaspiRobot.service file in the /lib/systemd/system/ directory with the following content: 
```
[Unit]
Description=RaspiRobot
After=nginx.service

[Service]
Type=simple
User=pi
WorkingDirectory=/home/pi/RaspiRobot/bin
ExecStart=/home/pi/RaspiRobot/bin/RaspiRobot
Restart=always
RestartSec=5

[Install]
WantedBy=multi-user.target
```

To edit the file use the following command line:
```
sudo nano /lib/systemd/system/RaspiRobot.service
```

When the configuration of a service has changed, it might be required to reload the configuration:
```
sudo systemctl daemon-reload
```

2. Enable the service
To ensure that the service gets started after starting/rebooting the device, use the following command line:
```
sudo systemctl enable RaspiRobot
```

3. Start/stop the service
The service can be started or stopped by the following commands:
```
sudo systemctl start RaspiRobot
sudo systemctl stop RaspiRobot
```

4. Check state of a service
```
sudo systemctl status RaspiRobot
```

## Documentation on the Web
You can find more information here:

[Hosting an ASP.NET Core 2 application on a Raspberry Pi](https://thomaslevesque.com/2018/04/17/hosting-an-asp-net-core-2-application-on-a-raspberry-pi/)
[Raspberry Pi: Dienste starten, stoppen, neustarten, aktivieren und deaktivieren](https://www.elektronik-kompendium.de/sites/raspberry-pi/2002211.htm)
