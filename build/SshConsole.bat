@echo off

set raspi_name=%raspitobot_name%
set key_file="C:\temp\Raspberry\ssh_keys\id_rsa"

ssh -i "C:\temp\Raspberry\ssh_keys\id_rsa" pi@%raspirobot_name%