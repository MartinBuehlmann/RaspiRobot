@echo off

set raspi_name=%raspirobot_name%
set key_file="C:\temp\Raspberry\ssh_keys\id_rsa"

ssh -i "C:\temp\Raspberry\ssh_keys\id_rsa" pi@%raspirobot_name% "sudo shutdown -h now"