using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        Dictionary<string, MethodInfo> commandMethodMap = new Dictionary<string, MethodInfo>
        {
            { "asd", typeof(MainWindow).GetMethod("updateTeleCoilUI") }
        };

        private void updateTeleCoilUI(int value)
        {
            Debug.WriteLine("asd");
        }

        private void updateAudioShoeUI(Object o)
        {
            return;
        }

        private void updateHIPowerUI(Object o)
        {
            return;
        }

        private void updateLEDUI(Object o)
        {
            return;
        }

        private void updateSoundUI(Object o)
        {
            return;
        }
    }

}
