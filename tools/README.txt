!! get_pi_ip.bat and init_wifi.bat are only necessary if you use wifi !!
!! If you use ethernet, you should know the raspis IP, if not you may !!
!! want to try get_pi_ip.bat                                          !!

The two batch-scripts are designed for development automation. For now, testing with raspis goes as follows:

* Configure raspi to automatically connect to an ad-hoc wifi (the one from sivantos is already configured this way)
* Turn on raspi
* Execute init_wifi.bat with admin rights (necessary to flush ARP cache, which you wanna do if you already had the wifi up and connected)
* Wait for some time, the raspi takes some time to boot
* Execute get_pi_ip.bat and look at the output
  * If you didn't flush ARP cache, there may be redundant addresses in there
  * If you did or you just booted up, the IP addresses showing are the connected raspis