using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace RaspberryBackend
{
    public class I2C
    {
        public static Dictionary<byte, I2cDevice> connectedDevices = new Dictionary<byte, I2cDevice>();

        public const string I2C_CONTROLLER_NAME = "I2C1";

        /// <summary>
        /// based on information in https://www.hackster.io/porrey/discover-i2c-devices-on-the-raspberry-pi-84bc8b
        /// New I2C device are only going to be connected once. The I2C device will be initialized in DefaultBusSpeedMode and ExclusiveConnectionMode.
        /// </summary>
        /// <param name="deviceAdress">The Adress of the specific I2C device (consult Data-Sheed if neccessary)</param>
        /// <returns></returns>
        public static async Task connectDeviceAsync(byte deviceAdress)
        {
            if (!connectedDevices.ContainsKey(deviceAdress))
            {
                // *** Get a selector string that will return all I2C controllers on the system
                string availableDeviceSelector = I2cDevice.GetDeviceSelector();

                // *** Find the I2C bus controller device with our selector string
                DeviceInformationCollection availableDeviceSelectors = await DeviceInformation.FindAllAsync(availableDeviceSelector).AsTask();
                DeviceInformation deviceSelector = availableDeviceSelectors[0];

                // *** Create the settings and specify the device address.
                I2cConnectionSettings settings = new I2cConnectionSettings(deviceAdress);

                // *** Create an I2cDevice with our selected bus controller and I2C settings.
                I2cDevice device = await I2cDevice.FromIdAsync(deviceSelector.Id, settings);

                connectedDevices.Add(deviceAdress, device);

                setUpDevice(device, false, false);
            }
        }

        /// <summary></summary
        /// <param name="FastModeBusSpeed">True for FastMode or False for DefaulMode</param>
        /// <param name="SharedConnectionMode">True for SharedMode or False for ExclusiveMode</param>
        /// <returns></returns>
        /// <see cref="connectDeviceAsync(byte)"/>
        public static async Task connectDeviceAsync(byte deviceAdress, bool FastModeBusSpeed, bool SharedConnectionMode)
        {
            if (!connectedDevices.ContainsKey(deviceAdress))
            {
                // *** Get a selector string that will return all I2C controllers on the system
                string availableDeviceSelector = I2cDevice.GetDeviceSelector();

                // *** Find the I2C bus controller device with our selector string
                DeviceInformationCollection availableDeviceSelectors = await DeviceInformation.FindAllAsync(availableDeviceSelector).AsTask();
                DeviceInformation deviceSelector = availableDeviceSelectors[0];

                // *** Create the settings and specify the device address.
                I2cConnectionSettings settings = new I2cConnectionSettings(deviceAdress);

                // *** Create an I2cDevice with our selected bus controller and I2C settings.
                I2cDevice device = await I2cDevice.FromIdAsync(deviceSelector.Id, settings);

                connectedDevices.Add(deviceAdress, device);

                setUpDevice(device, FastModeBusSpeed, SharedConnectionMode);
            }
        }

        private static void setUpDevice(I2cDevice device, bool FastModeBusSpeed, bool SharedConnectionMode)
        {
            I2cConnectionSettings settings = device.ConnectionSettings;
            settings.BusSpeed = FastModeBusSpeed ? I2cBusSpeed.FastMode : I2cBusSpeed.StandardMode;
            settings.SharingMode = SharedConnectionMode ? I2cSharingMode.Shared : I2cSharingMode.Exclusive;
        }
    }
}
