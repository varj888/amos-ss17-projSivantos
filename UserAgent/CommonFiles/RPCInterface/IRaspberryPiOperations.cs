using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonFiles.RPCInterface
{
    interface IRaspberryPiOperations
    {
        string ConnectPins(int x, int y);
        string EnableAudioShoe(int value);
        string EnableTeleCoil(int value);
        int EndlessVCUp(int ticks);
        string getAudioShoeStatus();
        string GetAvailableHI(int y);
        XDocument getStatusXML();
        string getTeleCoilStatus();
        string LightLED(Int32 requestedParameter);
        string ToggleLED(int[] param);
        string PressCombination(int[] param);
        string PressPushButton(int duration);
        string PressRockerSwitch(int[] param);
        string ReadPin(UInt16 id);
        string ResetMux(int a);
        string ResetPin(UInt16 id);
        string SendToLCD(string text);
        string SetAnalogVolume(byte requestedVolumeLevel);
        string SetARDVoltage(string device);
        string SetHI(string family, string model);
        string ToggleBacklight_LCD(int requestedParameter);
        double ChangePowerVoltage(double voltage);
        void updateLCD();
        string WritePin(UInt16 id);

    }
}