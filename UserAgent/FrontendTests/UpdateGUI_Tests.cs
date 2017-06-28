using CommonFiles.TransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Shapes;
using TestmachineFrontend;

namespace TestmachineFrontendTests
{
    [TestClass]
    public class UpdateGUI_Tests
    {
        List<string> methodsOfMainWindow;
        List<string> existingMethods;
        Dictionary<string, FieldInfo> existingFieldsDic;

        Result falseResultGPIO;
        Result trueResultGPIO;



        MainWindow testMain = new MainWindow();

        [TestInitialize]
        public void initializeTest()
        {
            existingMethods = new List<string> {
            "updateGUI_EnableTeleCoil" ,
            "updateGUI_EnableAudioShoe",
            "updateGUI_TurnHIOn",
            "updateGUI_Sound",
            "updateGUI_PressPushButton",
            "updateGUI_PressRockerSwitch"
             };


            methodsOfMainWindow = testMain.mainMethods;

            existingFieldsDic = getFieldsOf(typeof(TestmachineFrontend.MainWindow));

            trueResultGPIO = new Result(true, "success", "High");
            falseResultGPIO = new Result(false, "fail", "Low");
        }


        [TestMethod]
        public void methodsInMainExist_test()
        {
            Assert.IsTrue(existingMethods.All(i => methodsOfMainWindow.Contains(i)));
        }

        [TestMethod]
        public void methodsInvokesTest()
        {
            FieldInfo TCoil_Eclipse;

            existingFieldsDic.TryGetValue("TCoil_Eclipse", out TCoil_Eclipse);

            if (TCoil_Eclipse != null)
            {
                Ellipse ellipse = (Ellipse)TCoil_Eclipse.GetValue(testMain);

                Brush valueBefore = ellipse.Fill;
                testMain.updateGUI_EnableTeleCoil(trueResultGPIO);
                Brush valueAfter = ellipse.Fill;

                Assert.AreNotEqual(valueBefore, valueAfter);
            }
        }

        /// <summary>
        /// This helper Method can get all declared fields of a Class
        /// </summary>
        /// <param name="type">The Type of a class. Use typeof(Class) to get the Type of a class</param>
        /// <returns>A Dictionary with fieldnames as keys and all fields within the defined type as values</returns>
        private static Dictionary<string, FieldInfo> getFieldsOf(Type type)
        {
            Dictionary<string, FieldInfo> fieldDict = new Dictionary<string, FieldInfo>();
            List<FieldInfo> fieldInfos = new List<FieldInfo>(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly));

            fieldInfos.ForEach(i => fieldDict.Add(i.Name, i));

            return fieldDict;
        }
    }
}
