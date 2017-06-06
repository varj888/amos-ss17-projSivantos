using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace RaspberryBackend.Data
{
    /// <summary>
    /// represents the common interface for
    /// I2C-Communication with devices
    /// </summary>
    public static class I2C
    {
        /// <summary>
        /// one Raspberry Pi allows to connect up to 128 devices
        /// </summary>
		const int MIN_ADDRESS = 0;
        const int MAX_ADDRESS = 127;

        /// <summary>
        /// Discovers the I2C-compatible devices
        /// </summary>
        /// <returns>Addresses of connected devices</returns>
		public static async Task<IEnumerable<byte>> DiscoverAllDevicesAsync()
        {
            IList<byte> addressList = new List<byte>();
            string deviceSelector = I2cDevice.GetDeviceSelector();
            var devices = await DeviceInformation.FindAllAsync(deviceSelector).AsTask();
            I2cDevice i2cDevice;

            if (devices.Count > 0)
            {
                for (byte address = MIN_ADDRESS; address <= MAX_ADDRESS; address++)
                {
                    var i2cSettings = new I2cConnectionSettings(address);
                    i2cSettings.BusSpeed = I2cBusSpeed.FastMode;
                    i2cSettings.SharingMode = I2cSharingMode.Shared;

                    i2cDevice = await I2cDevice.FromIdAsync(devices[0].Id, i2cSettings);
                    if (i2cDevice != null)
                    {
                        try
                        {
                            byte[] writeBuffer = new byte[1] { 0 };
                            //devices.Write(writeBuffer);
                            addressList.Add(address);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Exception: {0}", e.Message);
                            //return;
                        }
                    }
                }
            }
            return addressList;
        }
    }
}
