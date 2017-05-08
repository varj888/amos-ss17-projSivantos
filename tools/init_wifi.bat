:: Start an Ad-Hoc WiFi the raspi connects to if configured accordingly
:: Networks are stored with just their SSID and Key, MAC is ignored
:: After starting up the network, check arp -a for the raspi IPs
@ECHO OFF

netsh wlan set hostednetwork mode=allow ssid=raspi key=1234qwer || ECHO Couldn't create hostednetwork
netsh wlan start hostednetwork || ECHO Couldn't start hostednetwork

:: Flush ARP table for restarting WiFi (Note: Reboot Pis if restarting NIC)
arp -d || ECHO Couldn't flush ARP cache. Try running script as Administrator

pause