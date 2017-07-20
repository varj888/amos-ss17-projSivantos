using RaspberryBackend.Config;
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

            cancelWriteOnLcd();

            CheckTurnOnBacklight();

            List<string> line1 = prepairLine1();
            prepairSymbols();

            _writingOnLcd = Task.Run(() => printOnLcd(line1), _cts.Token);
        }

        #region Helpers

        private List<string> prepairLine1()
        {
            string ip = GetIpAddressAsync();
            string hi = StorageCfgs.Hi.Model;
            string currentReceiver = StorageCfgs.Hi.CurrentReceiver;

            return new List<string> { ip, hi, currentReceiver };
        }

        private void printOnLcd(List<string> line1)
        {
            while (!_cts.IsCancellationRequested)
            {
                foreach (var content in line1)
                {
                    LCD.prints(content);
                    LCD.gotoSecondLine();
                    printLine2();

                    try
                    {
                        Task.Delay(3500).Wait(_cts.Token);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }

                    LCD.clrscr();
                }
            }
        }

        private void printLine2()
        {
            LCD.prints("<Akt>");
            LCD.prints(" ");
            this.LCD.printSymbol(SymbolConfig.batterySymbolAddress);
            LCD.prints(" ");
            this.LCD.printSymbol(SymbolConfig.initSymbolAddress);
            LCD.prints(" ");
            LCD.prints("<Vol>");
        }
        private void cancelWriteOnLcd()
        {
            _cts?.Cancel();
            waitForCancelation();
            _cts = new CancellationTokenSource();
        }
        private void waitForCancelation()
        {
            Debug.Write("**** Cancel Task");
            while (_writingOnLcd != null && !_writingOnLcd.IsCanceled && !_writingOnLcd.IsCompleted)
            {
                Debug.Write(".");
            }
            Debug.WriteLine(" ****");
        }
        /// <summary>
        /// Set state for background in LCD. Will want to switch to toggle
        /// </summary>
        /// <param name="targetState"></param>
        private void setLCDBackgroundState(byte targetState)
        {
            LCD.backLight = targetState;
            LCD.write(targetState, 0);
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


        private void prepairSymbols()
        {
            this.LCD.createSymbol(this.getBatterySymbol(), SymbolConfig.batterySymbolAddress);
            this.LCD.createSymbol(this.getInitSymbol(RasPi.isInitialized()), SymbolConfig.initSymbolAddress);
        }

        private byte[] getBatterySymbol()
        {
            double batstatus = this.ADConverter.CurrentDACVoltage1 / this.ADConverter.getMaxVoltage();
            byte[] data = (byte[])SymbolConfig.batterySymbol.Clone();

            for (int i = 1; i <= 6; i++)
            {
                int counter = 6;
                double frac = (double)i / 6.0;
                if (batstatus < frac)
                {
                    data[counter - i] = 0b00010001;
                }
                counter--;
            }

            return data;
        }

        private byte[] getInitSymbol(bool isInit)
        {
            return (isInit) ? SymbolConfig.isInitSymbol : SymbolConfig.notInitSymbol;
        }

        private void CheckTurnOnBacklight()
        {
            if (LCD.backLight != 0x01)
            {
                this.setLCDBackgroundState(0x01);
            }
        }
        #endregion

    }
}
