using Fed.Modbus.Rtu.ModBusRTU;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fed.SerialPorts
{
    /// <summary>
    /// 串口客户端
    /// </summary>
    public class SerialPortClient
    {
        /// <summary>
        /// 串口对象
        /// </summary>
        private SerialPort _serialPort;

        public SerialPort SerialPort { get { return _serialPort; } set { _serialPort = value; } }

        public event Action<byte[]> OnReveivedData;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="portName">端口名，如COM1</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity">奇偶校验</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        /// <exception cref="Exception"></exception>
        public SerialPortClient(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            //获取当前计算机所有的串行端口名
            string[] sysSerialProtNames = SerialPort.GetPortNames();
            if (!sysSerialProtNames.Any(name => name == portName))
                throw new Exception($"“{portName}”串口不存在");
            //初始化串口对象
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            //串口接收数据事件
            //_serialPort.DataReceived += Receive;
        }

        /// <summary>
        /// 是否已经打开了端口
        /// </summary>
        public bool IsOpen { get { return _serialPort.IsOpen; } }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void TryOpen()
        {
            try
            {
                //打开串口
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("无法打开此串口，请检查是否被占用");
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void TryClose()
        {
            try
            {
                //关闭串口
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("无法关闭此串口，请检查是否被占用");
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        public void Send(byte[] data)
        {
            //获取串口状态，true为已打开，false为未打开
            bool isOpen = _serialPort.IsOpen;

            if (!isOpen)
            {
                TryOpen();
            }

            //发送字节数组
            //参数1：包含要写入端口的数据的字节数组。
            //参数2：参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
            //参数3：要写入的字节数。 
            _serialPort.Write(data, 0, data.Length);
        }


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        public List<T> Send<T>(byte[] data, ByteFormart formart = ByteFormart.BigEndian) where T : struct
        {
            //获取串口状态，true为已打开，false为未打开
            bool isOpen = _serialPort.IsOpen;

            if (!isOpen)
            {
                TryOpen();
            }

            //发送字节数组
            //参数1：包含要写入端口的数据的字节数组。
            //参数2：参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
            //参数3：要写入的字节数。 
            _serialPort.Write(data, 0, data.Length);
            //接收响应
            var response = Read();
            var responseData = MsgAnalysis.AnalysisMessage<T>(response, formart);
            responseData = responseData.Take(Convert.ToInt32(data[5])).ToList();
            return responseData;
            //根据解析响应

            //if (OnReveivedData != null)
            //    OnReveivedData(result);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        public void Send(string data)
        {
            //获取串口状态，true为已打开，false为未打开
            bool isOpen = _serialPort.IsOpen;

            if (!isOpen)
            {
                TryOpen();
            }
            //直接发送字符串
            _serialPort.Write(data);

        }

        private byte[] Read()
        {
            Thread.Sleep(100);
            int numBytesRead = 0;
            byte[] result = null;
            while (true)
            {
                var receiveBytes = _serialPort.BytesToRead;
                result = new byte[receiveBytes];
                if (receiveBytes == 0)
                {
                    continue;
                }
                numBytesRead += _serialPort.Read(result, numBytesRead, receiveBytes);
                if (numBytesRead == receiveBytes)
                    break;
            }
            return result;
        }

        /// <summary>
        /// 串口接收到数据触发此方法进行数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Receive(object sender, SerialDataReceivedEventArgs e)
        {
            //var receiveBytes = _serialPort.BytesToRead;
            //if (receiveBytes == 0)
            //{
            //    return;
            //}
            ////读取串口缓冲区的字节数据
            //byte[] result = new byte[receiveBytes];
            //_serialPort.Read(result, 0, receiveBytes);
            //if (OnReveivedData != null)
            //    OnReveivedData(result);
        }
    }
}
