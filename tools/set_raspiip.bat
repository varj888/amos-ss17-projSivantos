:: Set a static IP address on a host
:: Enter respective ip address in netsh command
:: Copy this file on raspi
:: Then execute the command on the raspi in cmd
@ECHO OFF

SET IP=10.1.57.227
SET SNM=255.255.255.0
SET GW=10.1.57.1
SET INTERFACE=ETHERNET

netsh interface ip set address "%INTERFACE%" static %IP% %SNM% %GW%

pause