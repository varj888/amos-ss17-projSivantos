using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TestmachineFrontend1
{
<<<<<<< HEAD
=======

>>>>>>> master
    /// <summary>
    /// Allows to remotely call operations of the raspberry Pi.
    /// Runs a ReceiveLoop in an own Task for receiving TOs
    /// </summary>
    public sealed class RaspberryPi
    {
        private TcpClient _socket;
        private Dictionary<Type, Action<object>> _TOHandlerMap;
        private ConcurrentQueue<TaskCompletionSource<SuccessResult>> _answers;
        private Task _receiveTask;

        /// <summary>
        /// Thrown by the receive loop, if it was quit
        /// </summary>
        public event EventHandler<Exception> ConnectionClosed;

        /// <summary>
        /// Creates the Class, without connecting it
        /// </summary>
        /// <param name="socket"></param>
        public RaspberryPi()
        {
            initTOHandlerMap();
            _answers = new ConcurrentQueue<TaskCompletionSource<SuccessResult>>();
            _socket = new TcpClient();
        }

        /// <summary>
        /// Connects to the RaspberryPi
        /// </summary>
        /// <param name="endpoint">Contains the IP-Address and Port for connection</param>
        public async Task ConnectAsync(IPEndPoint endpoint)
        {
            await _socket.ConnectAsync(endpoint.Address, endpoint.Port);
            _receiveTask = Task.Run(() => ReceiveLoop());
        }

        /// <summary>
        /// Closes the connection. The receive loop quits the loop and throws a disconnected Event
        /// </summary>
        /// <returns></returns>
        public async Task Disconnect()
        {
            _socket.Close();
            clearAnswers();
            await _receiveTask;
            _socket = new TcpClient();
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

        private void clearAnswers()
        {
            TaskCompletionSource<SuccessResult> item;
            while (_answers.TryDequeue(out item))
            {
                // do nothing
            }
        }

        private void initTOHandlerMap()
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
            int timeout = 5000;
            if (await Task.WhenAny(answer.Task, Task.Run(() => Task.Delay(timeout))) != answer.Task)
            {
                throw new TimeoutException("The operation has timed out.");
            }
            return (await answer.Task).result;
        }

        private async Task ReceiveLoop()
        {
            try
            {
                while (true)
                {
                    Object transferObject = await Transfer.receiveObjectAsync(_socket.GetStream());
                    _TOHandlerMap[transferObject.GetType()].Invoke(transferObject);
                }
            }
            catch(Exception e)
            {
                _socket.Close();
                onConnectionClosed(e);
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

        private void onConnectionClosed(Exception e)
        {
            EventHandler<Exception> handler = ConnectionClosed;
            if (ConnectionClosed != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Async method that changes the active
        /// power supply voltage of the DAC to a specified value.
        /// </summary>
        /// <param name="voltage">Double: DAC accepts a value between 0.0 and 1.5</param>
        /// <returns>Returns the specified voltage.</returns>
        public async Task<double> ChangePowerVoltage(double voltage)
        {
            return (double)await sendRequest(new Request("ChangePowerVoltage", voltage));
        }

        /// <summary>
        /// Async method that gets the active
        /// RaspberryPi configuration.
        /// </summary>
        /// <returns>String: Returns the RaspberryPi Configuration .</returns>
        public async Task<string> GetRaspiConfig()
        {
            return (string)await sendRequest(new Request("GetRaspiConfig", 0));
        }

        public async Task<string> GetRaspiFamily()
        {
            return (string)await sendRequest(new Request("GetRaspiFamily", 0));
        }

        public async Task<string> GetRaspiModel()
        {
            return (string)await sendRequest(new Request("GetRaspiModel", 0));
        }

        /// <summary>
        /// Async method that checks the current status of a connected LED.
        /// </summary>
        /// <returns>Boolean: Returns true if the LED is On ( Voltage &gt 0.1 ) and false if the LED is OFF ( Voltage &lt 0.1 )</returns>
        public async Task<bool> CheckLEDStatus()
        {
            return (bool)await sendRequest(new Request("CheckLEDStatus", 0));
        }

        /// <summary>
        /// Async method that checks the current
        /// status of a connected LED.
        /// </summary>
        /// <returns>Boolean: Returns true if the LED is On ( Voltage &gt 0.1 ) and false if the LED is OFF ( Voltage &lt 0.1 ) </returns>
        public async Task<string> ConnectPins(int valueX, int valueY)
        {
            return (string)await sendRequest(new Request("ConnectPins", new object[] { valueX, valueY }));
        }

        public async Task<string> ReadPin(UInt16 pinID)
        {
            return (string)await sendRequest(new Request("ReadPin", pinID));
        }

        public async Task<string> WritePin(UInt16 pinID)
        {
            return (string)await sendRequest(new Request("WritePin", pinID));
        }

        public async Task<string> ResetPin(UInt16 pinID)
        {
            return (string)await sendRequest(new Request("ResetPin", pinID));
        }

        /// <summary>
        /// Async method that resets the multiplexer.
        /// After reset nothing is routed through the multiplexer.
        /// </summary>
        /// <returns>Bool: Retuns true if the operation succeeded, false if not.
        public async Task<string> ResetMux()
        {
            return (string)await sendRequest(new Request("ResetMux", 0));
        }

        /// <summary>
        /// Async method that sets the receiver voltage for a
        /// specific receiver.
        /// </summary>
        /// <param name="device">Specifies the receiver as string</param>
        /// <returns>Returns the specified receiver.</returns>
        public async Task<string> SetARDVoltage(string device)
        {
            return (string)await sendRequest(new Request("SetARDVoltage", device));
        }

        /// <summary>
        /// Async method that sets the multiplexer configuration for a
        /// specific HI model.
        /// </summary>
        /// <param name="family">Specifies the family</param>
        /// <param name="model">Specifies the model</param>
        /// <returns>String: Returns the specified HI model.</returns>
        public async Task<string> SetHI(string family, string model)
        {
            return (string)await sendRequest(new Request("SetHI", new object[] { family, model }));
        }

        /// <summary>
        /// Async method that gets a List of available HIs
        /// </summary>
        /// <returns>String: Returns a list of available HIs.</returns>
        public async Task<string> GetAvailableHI()
        {
            return (string)await sendRequest(new Request("GetAvailableHI", 0));
        }

        /// <summary>
        /// Async method that presses the pushbutton of the connected HI.
        /// </summary>
        /// <param name="durationCategorie">Specifies the duration the pushbutton should be pushed.</param>
        /// <returns>String: Returns the duration.</returns>
        public async Task<string> PressPushButton(string durationCategorie)
        {
            return (string)await sendRequest(new Request("PressPushButton", durationCategorie));
        }

        /// <summary>
        /// Async method that presses the rocker switch up button of the connected HI.
        /// </summary>
        /// <param name="durationCategorie">Specifies the duration the rocker switch up button should be pushed.</param>
        /// <returns>String: Returns the duration.</returns>
        public async Task<string> PressRockerSwitchDown(string durationCategorie)
        {
            return (string)await sendRequest(new Request("PressRockerSwitch", new string[] { "down", durationCategorie }));
        }

        /// <summary>
        /// Async method that presses the rocker switch down button of the connected HI.
        /// </summary>
        /// <param name="durationCategorie">Specifies the duration the rocker switch down button should be pushed.</param>
        /// <returns>String: Returns the duration.</returns>
        public async Task<string> PressRockerSwitchUp(string durationCategorie)
        {
            return (string)await sendRequest(new Request("PressRockerSwitch", new string[] { "up", durationCategorie }));
        }

        /// <summary>
        /// Async method that presses multiple user control buttons at once.
        /// </summary>
        /// <param name="param">Int 1 for press, duration any int: [rockerswitch_0, rockerswitch_1, pushbutton, duration]</param>
        /// <returns>String: Returns the duration.</returns>
        public async Task<string> PressCombination(string[] param)
        {
            return (string)await sendRequest(new Request("PressCombination", param));
        }

        /// <summary>
        /// Async method that sets the status of the tele coil to on.
        /// </summary>
        /// <returns>String: "High" </returns>
        public async Task<string> DetectTeleCoil()
        {
            return (string)await sendRequest(new Request("EnableTeleCoil", 1));
        }

        /// <summary>
        /// Async method that sets the status of the tele coil to off.
        /// </summary>
        /// <returns>String: "Low" </returns>
        public async Task<string> UndetectTeleCoil()
        {
            return (string)await sendRequest(new Request("EnableTeleCoil", 0));
        }

        /// <summary>
        /// Async method that sets the status of the audio shoe to on.
        /// </summary>
        /// <returns>String: "High" </returns>
        public async Task<string> DetectAudioShoe()
        {
            return (string)await sendRequest(new Request("EnableAudioShoe", 1));
        }

        /// <summary>
        /// Async method that sets the status of the audio shoe to off.
        /// </summary>
        /// <returns>String: "High" </returns>
        public async Task<string> UndetectAudioShoe()
        {
            return (string)await sendRequest(new Request("EnableAudioShoe", 0));
        }

        /// <summary>
        /// Async method that presses the rocker switch up button multiple times.
        /// </summary>
        /// <param name="ticks">Int: Specifies the number of presses</param>
        /// <returns>Int: The number of presses </returns>
        public async Task<int> EndlessVCUp(int ticks)
        {
            return (int)await sendRequest(new Request("EndlessVCUp", ticks));
        }

        /// <summary>
        /// Async method that presses the rocker switch down button multiple times.
        /// </summary>
        /// <param name="ticks">Int: Specifies the number of presses</param>
        /// <returns>Int: The number of presses </returns>
        public async Task<int> EndlessVCDown(int ticks)
        {
            return (int)await sendRequest(new Request("EndlessVCDown", ticks));
        }

        /// <summary>
        /// Async method that sets the analog volume (potentiometer) the rocker switch up button multiple times.
        /// to a specifc value.
        /// </summary>
        /// <param name="requestedVolumeLevel">Byte: Between 0 and 127</param>
        /// <returns>String: requestedVolumeLevel as String </returns>
        public async Task<string> SetAnalogVolume(byte requestedVolumeLevel)
        {
            return (string)await sendRequest(new Request("SetAnalogVolume", requestedVolumeLevel));
        }

        /// <summary>
        /// Async method that displays a text on the connected LCD.
        /// </summary>
        /// <param name="text">String: Arbitrary string. Max 32 characters</param>
        /// <returns>String: Returns the sent string </returns>
        public async Task<string> SendToLCD(string text)
        {
            return (string)await sendRequest(new Request("SendToLCD", text));
        }

        /// <summary>
        /// Async method that toggles the backlight of the connected LCD.
        /// </summary>
        /// <param name="requestedParameter">Int: 0 for off, 1 for on</param>
        /// <returns>String: Returns the requestedParamter as string </returns>
        public async Task<string> ToggleBacklight_LCD(int requestedParameter)
        {
            return (string)await sendRequest(new Request("ToggleBacklight_LCD", requestedParameter));
        }
    }
}
