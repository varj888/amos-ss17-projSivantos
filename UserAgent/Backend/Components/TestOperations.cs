using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RaspberryBackend
{
    class TestOperations : IRaspberryPiOperations
    {
        public string ConnectPins(int x, int y)
        {
            Debug.WriteLine("ConnectPins");
            Debug.WriteLine(x);
            Debug.WriteLine(y);
            return null;
        }

        public string EnableAudioShoe(int value)
        {
            throw new NotImplementedException();
        }

        public string EnableTeleCoil(int value)
        {
            throw new NotImplementedException();
        }

        public int EndlessVCUp(int ticks)
        {
            throw new NotImplementedException();
        }

        public string getAudioShoeStatus()
        {
            throw new NotImplementedException();
        }

        public string GetAvailableHI(int y)
        {
            throw new NotImplementedException();
        }

        public XDocument getStatusXML()
        {
            throw new NotImplementedException();
        }

        public string getTeleCoilStatus()
        {
            throw new NotImplementedException();
        }

        public string LightLED(int requestedParameter)
        {
            Debug.WriteLine("LightLED");
            Debug.WriteLine(requestedParameter);
            return requestedParameter.ToString();
            //throw new NotImplementedException();
        }

        public string PressCombination(int[] param)
        {
            throw new NotImplementedException();
        }

        public string PressPushButton(int duration)
        {
            throw new NotImplementedException();
        }

        public string PressRockerSwitch(int[] param)
        {
            throw new NotImplementedException();
        }

        public string ReadPin(ushort id)
        {
            throw new NotImplementedException();
        }

        public string ResetMux(int a)
        {
            throw new NotImplementedException();
        }

        public string ResetPin(ushort id)
        {
            throw new NotImplementedException();
        }

        public string SendToLCD(string text)
        {
            throw new NotImplementedException();
        }

        public string SetAnalogVolume(byte requestedVolumeLevel)
        {
            throw new NotImplementedException();
        }

        public string SetARDVoltage(string device)
        {
            throw new NotImplementedException();
        }

        public string SetHI(string family, string model)
        {
            throw new NotImplementedException();
        }

        public MultiplexerConfig setMultiplexerConfiguration(string family, string model_name)
        {
            throw new NotImplementedException();
        }

        public string ToggleBacklight_LCD(int requestedParameter)
        {
            throw new NotImplementedException();
        }

        public double TurnHIOn(double voltage)
        {
            throw new NotImplementedException();
        }

        public void updateLCD()
        {
            throw new NotImplementedException();
        }

        public string WritePin(ushort id)
        {
            throw new NotImplementedException();
        }
    }
}
