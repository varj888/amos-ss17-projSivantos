using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public partial class Operation
    {
        private CancellationTokenSource _cts;
        private Task _writingOnLcd;

        /// <summary>
        /// Method to wrap updating the LCD with fixed information.
        /// </summary>
        public void updateLCD()
        {
            if (RasPi.isTestMode()) return;
            LCD.initiateLCD();

            cancelWritingWait();
            CheckTurnOnBacklight();

            _writingOnLcd = Task.Run(async () => await printOnLcd(), _cts.Token);
        }

        #region Helpers

        private List<string> prepairLine1()
        {
            string ip = GetIpAddressAsync();
            string hi = StorageCfgs.Hi.Model;
            string currentReceiver = StorageCfgs.Hi.CurrentReceiver;

            return new List<string> { ip, hi, currentReceiver };
        }

        private Task printOnLcd()
        {
            while (!_cts.IsCancellationRequested)
            {
                List<string> line1 = prepairLine1();

                foreach (var content in line1)
                {
                    SymbolConfig.initilizeSymbols();

                    LCD.prints(content);
                    LCD.gotoSecondLine();

                    printLine2();

                    try
                    {
                        Task.Delay(3500).Wait(_cts.Token);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("cancel LCD-Writing Task");
                        return Task.CompletedTask;
                    }

                    LCD.clrscr();
                }
            }

            return Task.CompletedTask;
        }

        private void printLine2()
        {
            LCD.prints(" ");
            this.LCD.printSymbol(SymbolConfig.busySymbolAddress);
            LCD.prints("   ");
            this.LCD.printSymbol(SymbolConfig.batterySymbolAddress);
            LCD.prints("   ");
            this.LCD.printSymbol(SymbolConfig.initSymbolAddress);
            LCD.prints("   ");
            this.LCD.printSymbol(SymbolConfig.volumeSymbolAddress);
            LCD.prints(" ");

        }

        private void cancelWritingWait()
        {
            _cts?.Cancel();

            Debug.Write("\n**** Wait for LCD-Writing Task ****\n");
            while (_writingOnLcd != null && !_writingOnLcd.IsCanceled && !_writingOnLcd.IsCompleted)
            {

            }
            Debug.Write("**** LCD-Writing Task finished ****\n");

            _cts = new CancellationTokenSource();
        }

        private string GetIpAddressAsync()
        {
            var ipAsString = "Not Found";
            var hosts = Windows.Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
            var hostNames = new List<string>();

            //NetworkInterfaceType
            foreach (var h in hosts)
            {
                hostNames.Add(h.DisplayName);
                if (h.Type == Windows.Networking.HostNameType.Ipv4)
                {
                    var networkAdapter = h.IPInformation.NetworkAdapter;
                    if (networkAdapter.IanaInterfaceType == (uint)NetworkInterfaceType.Ethernet || networkAdapter.IanaInterfaceType == (uint)NetworkInterfaceType.Wireless80211)
                    {
                        IPAddress ip;
                        if (!IPAddress.TryParse(h.DisplayName, out ip)) continue;
                        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) return ip.ToString();
                    }
                }
            }
            return ipAsString;
        }

        private void CheckTurnOnBacklight()
        {
            if (LCD.backLight != 0x01)
            {
                this.LCD.switchBacklightTo(0x01);
            }
        }
        #endregion

    }
}
