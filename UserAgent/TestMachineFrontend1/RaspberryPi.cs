using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestmachineFrontend1
{
   
    /// <summary>
    /// Allows to remotely call operations of the raspberry Pi.
    /// Runs a ReceiveLoop in an own Task for receiving TOs
    /// </summary>
    public sealed class RaspberryPi
    {
        private TcpClient _socket;
        private Dictionary<Type, Action<object>> _TOHandlerMap;
        private ConcurrentQueue<TaskCompletionSource<SuccessResult>> _answers;

        /// <summary>
        /// Creates and connects to the RaspberryPi
        /// </summary>
        /// <param name="endpoint">Contains the IP-Address and Port for connection</param>
        /// <returns>The created Raspberry Pi Class</returns>
        public static async Task<RaspberryPi> CreateAsync(IPEndPoint endpoint)
        {
            TcpClient socket = new TcpClient();
            await socket.ConnectAsync(endpoint.Address, endpoint.Port);
            return new RaspberryPi(socket);
        }

        /// <summary>
        /// Allows to register for received TOs.
        /// If a TO is received in the receive loop, the action will be called in an own thread with the TO as parameter
        /// </summary>
        /// <typeparam name="T">Type of the TO</typeparam>
        /// <param name="action">Registered Action</param>
        public void registerActionForTO<T>(Action<T> action)
        {
            if (_TOHandlerMap.ContainsKey(typeof(T)))
            {
                _TOHandlerMap[typeof(T)] += (to => action((T)to));
            }
            else
            {
                _TOHandlerMap.Add(typeof(T), to => action((T)to));
            }
        }

        private RaspberryPi(TcpClient socket)
        {
            _socket = socket;
            initToMap();
            _answers = new ConcurrentQueue<TaskCompletionSource<SuccessResult>>();
            Task.Run(() => ReceiveLoop());
        }

        private void initToMap()
        {
            _TOHandlerMap = new Dictionary<Type, Action<Object>>();
            registerActionForTO<SuccessResult>(onSuccessResult);
            registerActionForTO<ExceptionResult>(onExceptionResult);
        }

        private async Task<Object> sendRequest(Request request)
        {
            TaskCompletionSource<SuccessResult> answer = new TaskCompletionSource<SuccessResult>();
            _answers.Enqueue(answer);
            Transfer.sendObject(_socket.GetStream(), request);
            return (await answer.Task).result;
        }

        private async Task ReceiveLoop()
        {
            while (true)
            {
                Object transferObject = await Transfer.receiveObjectAsync(_socket.GetStream());
                _TOHandlerMap[transferObject.GetType()].Invoke(transferObject);
            }
        }

        private void onSuccessResult(SuccessResult successResult)
        {
            TaskCompletionSource<SuccessResult> answer;
            if (_answers.TryDequeue(out answer))
            {
                answer.SetResult(successResult);
            }
            else
            {
                throw new Exception("Result without Request");
            }
        }

        private void onExceptionResult(ExceptionResult excptionResult)
        {
            TaskCompletionSource<SuccessResult> answer;
            if (_answers.TryDequeue(out answer))
            {
                answer.SetException(new RequestExecutionException(excptionResult.exceptionMessage));
            }
            else
            {
                throw new Exception("Result without Request");
            }
        }


        public async Task<double> ChangePowerVoltage(double voltage)
        {
            return (double) await sendRequest(new Request("ChangePowerVoltage", voltage));
        }
        public async Task<string> GetRaspiConfig()
        {
            return (string) await sendRequest(new Request("GetRaspiConfig", 0));
        }

        public async Task<bool> CheckLEDStatus()
        {
            return (bool)await sendRequest(new Request("CheckLEDStatus", 0));
        }

        public async Task<string> LightLED(int value)
        {
            return (string)await sendRequest(new Request("LightLED", value));
        }

        public async Task<string> ConnectPins(int valueX, int valueY)
        {
            return (string)await sendRequest(new Request("ConnectPins", new object[] { valueX, valueY }));
        }

        public async Task<string> ResetMux(int v)
        {
            return (string)await sendRequest(new Request("ResetMux", v));
        }

        public async Task<string> SetARDVoltage(ContentControl content)
        {
            return (string)await sendRequest(new Request("SetARDVoltage", content.Content));
        }

        public async Task<string> SetHI(string family, string model)
        {
            return (string)await sendRequest(new Request("SetHI", new object[] { family, model }));  
        }

        public async Task<string> GetAvailableHI(int v)
        {
            return (string)await sendRequest(new Request("GetAvailableHI", v));
        }

        public async Task<string> PressPushButton(int duration)
        {
            return (string)await sendRequest(new Request("PressPushButton", duration));
        }

        public async Task<string> PressRockerSwitchDown(int duration)
        {
            return (string)await sendRequest(new Request("PressRockerSwitch", new int[] { 0, duration }));
        }

        public async Task<string> PressRockerSwitchUp(int duration)
        {
            return (string)await sendRequest(new Request("PressRockerSwitch", new int[] { 1, duration }));
        }

        public async Task<string> PressCombination(int[] param)
        {
            return (string)await sendRequest(new Request("PressCombination", param));
        }
        
        public async Task<string> DetectTeleCoil()
        {
            return (string)await sendRequest(new Request("EnableTeleCoil", 1));
        }

        public async Task<string> UndetectTeleCoil()
        {
            return (string)await sendRequest(new Request("EnableTeleCoil", 0));
        }

        public async Task<string> DetectAudioShoe()
        {
            return (string)await sendRequest(new Request("DetectAudioShoe", 1));    
        }

        public async Task<string> UndetectAudioShoe()
        {
            return (string)await sendRequest(new Request("DetectAudioShoe", 0));
        }

        public async Task<string> EndlessVCUp(/*int ticks*/)
        {
            return (string)await sendRequest(new Request("EndlessVCUp", new int[] { }));
        }

        public async Task<string> EndlessVCDown(/*int ticks*/)
        {
            return (string)await sendRequest(new Request("EndlessVCDown", new int[] { }));
        }

        public async Task<string> SetAnalogVolume(byte requestedVolumeLevel)
        {
            return (string)await sendRequest(new Request("SetAnalogVolume", requestedVolumeLevel));
        }

        public async Task<string> TurnHIOn(double voltage)
        {
            return (string)await sendRequest(new Request("TurnHIOn", voltage));
        }

        public async Task<string> SendToLCD(string text)
        {
            return (string)await sendRequest(new Request("SendToLCD", text));
        }

        public async Task<string> ToggleBacklight_LCD(int requestedParameter)
        {
            return (string)await sendRequest(new Request("ToggleBacklight_LCD", requestedParameter));
        }
    }
}
