using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Based upon information in https://www.hackster.io/porrey/discover-i2c-devices-on-the-raspberry-pi-84bc8b
        /// New I2C device are only going to be connected once. The I2C device will be initialized in DefaultBusSpeedMode and ExclusiveConnectionMode.
        /// </summary>
        /// <param name="deviceAdress">The Adress of the specific I2C device (consult Data-Sheed if neccessary)</param>
        /// <returns></returns>
        public static async Task connectDeviceAsync(byte deviceAdress)
        {
            if (!connectedDevices.ContainsKey(deviceAdress))
            {
                await connectDeviceAsync(deviceAdress, false, false);
            }
        }

        /// <summary>Asynchronous method to connect devices.</summary>
        /// <param name="fastModeBusSpeed">True for FastMode or False for DefaulMode.</param>
        /// <param name="sharedConnectionMode">True for SharedMode or False for ExclusiveMode.</param>
        /// <returns></returns>
        /// <see cref="connectDeviceAsync(byte)"/>
        public static async Task connectDeviceAsync(byte deviceAdress, bool fastModeBusSpeed, bool sharedConnectionMode)
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

                setUpDevice(device, fastModeBusSpeed, sharedConnectionMode);
            }
        }

        /// <summary>
        /// Wrapper for setting up an I2C device. Sets respective settings such as BusSpeed or SharingMode.
        /// </summary>
        /// <param name="device">The I2C device to configure.</param>
        /// <param name="fastModeBusSpeed">Boolean flag to choose between FastMode and StandardMode.</param>
        /// <param name="sharedConnectionMode">Boolean flag to choose between shared connection or exclusive connection.</param>
        private static void setUpDevice(I2cDevice device, bool fastModeBusSpeed, bool sharedConnectionMode)
        {
            I2cConnectionSettings settings = device.ConnectionSettings;
            settings.BusSpeed = fastModeBusSpeed ? I2cBusSpeed.FastMode : I2cBusSpeed.StandardMode;
            settings.SharingMode = sharedConnectionMode ? I2cSharingMode.Shared : I2cSharingMode.Exclusive;

            Debug.WriteLine("Setup Connection of I2C Device \n"
                            + "BusSpeed : "+settings.BusSpeed +"\n"
                            + "Mode : " + settings.SharingMode + "\n"
            );
        }
    }
}
