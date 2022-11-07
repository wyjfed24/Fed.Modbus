using Fed.Modbus.Rtu;
using Fed.Modbus.Rtu.ModBusRTU;
using Fed.SerialPorts;

using Modbus.Device;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        SerialPortClient _serialPortClient;
        IModbusMaster master;
        public bool IsConnected { get { return _serialPortClient != null && _serialPortClient.IsOpen; } }
        /// <summary>
        /// 是否为写入模式
        /// </summary>
        private bool isWrite = false;

        /// <summary>
        /// 读写模式
        /// </summary>
        private object readWriteMode = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //获取串口列表
            cbPortName.DataSource = SerialPort.GetPortNames();
            //设置可选波特率
            cbBaudRate.DataSource = new object[] { 9600, 19200 };
            //设置可选奇偶校验
            cbParity.DataSource = new object[] { "None", "Odd", "Even", "Mark", "Space" };
            //设置可选数据位
            cbDataBits.DataSource = new object[] { 5, 6, 7, 8 };
            //设置可选停止位
            cbStopBits.DataSource = new object[] { 1, 1.5, 2 };
            //设置发送模式
            cbMethod.Items.AddRange(new object[] {
                "读取输出线圈",
                "读取离散输入",
                "读取保持型寄存器",
                "读取输入寄存器",
                "写入单个线圈",
                "写入多个线圈",
                "写入单个寄存器",
                "写入多个寄存器"
            });
            cbDataType.DataSource = new object[] { "bool", "ushort", "short", "int", "float", "double" };
            cbByteOrder.DataSource = new object[] { "AB CD/AB CD EF GH", "CD AB/GH EF CD AB", "BA DC/BA DC FE HG", "DC BA/HG FE DC BA" };
            //设置默认选中项
            cbPortName.SelectedIndex = 0;
            cbBaudRate.SelectedIndex = 0;
            cbParity.SelectedIndex = 0;
            cbDataBits.SelectedIndex = 3;
            cbStopBits.SelectedIndex = 0;
            cbMethod.SelectedIndex = 0;
            cbDataType.SelectedIndex = 0;
            cbByteOrder.SelectedIndex = 0;

            btnSend.Enabled = false;
        }

        /// <summary>
        /// 获取字节顺序
        /// </summary>
        /// <returns></returns>
        private ByteFormart GetByteFormart()
        {
            return cbByteOrder.SelectedItem.ToString() switch
            {
                "AB CD/AB CD EF GH" => ByteFormart.BigEndian,
                "DC BA/HG FE DC BA" => ByteFormart.LittleEndian,
                "BA DC/BA DC FE HG" => ByteFormart.BigEndianByteSwap,
                "CD AB/GH EF CD AB" => ByteFormart.LittleEndianByteSwap,
                _ => ByteFormart.BigEndian
            };
        }

        /// <summary>
        /// 获取窗体选中的奇偶校验
        /// </summary>
        /// <returns></returns>
        private Parity GetSelectedParity()
        {
            switch (cbParity.SelectedItem.ToString())
            {
                case "Odd":
                    return Parity.Odd;
                case "Even":
                    return Parity.Even;
                case "Mark":
                    return Parity.Mark;
                case "Space":
                    return Parity.Space;
                case "None":
                default:
                    return Parity.None;
            }
        }

        /// <summary>
        /// 获取窗体选中的停止位
        /// </summary>
        /// <returns></returns>
        private StopBits GetSelectedStopBits()
        {
            switch (Convert.ToDouble(cbStopBits.SelectedItem))
            {
                case 1:
                    return StopBits.One;
                case 1.5:
                    return StopBits.OnePointFive;
                case 2:
                    return StopBits.Two;
                default:
                    return StopBits.One;
            }
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (IsConnected)
                _serialPortClient.TryClose();
            var portName = cbPortName.SelectedItem.ToString();
            var baudRate = (int)cbBaudRate.SelectedItem;
            var parity = GetSelectedParity();
            var dataBits = (int)cbDataBits.SelectedItem;
            var stopBits = GetSelectedStopBits();

            _serialPortClient = new SerialPortClient(portName, baudRate, parity, dataBits, stopBits);
            _serialPortClient.OnReveivedData += _serialPortClient_OnReveivedData;
            _serialPortClient.TryOpen();
            labStatus.Text = "已连接";
            btnOpen.Enabled = false;
            btnSend.Enabled = true;
            btnClose.Enabled = true;
            master = ModbusSerialMaster.CreateRtu(_serialPortClient.SerialPort);
        }

        private void _serialPortClient_OnReveivedData(byte[] receivedBytes)
        {
            //rtxbReceiveMsg.BeginInvoke(new Action(() =>
            //{
            //    var sb = new StringBuilder();
            //    foreach (var b in receivedBytes)
            //    {
            //        if (sb.Length > 0)
            //            sb.Append(" ");
            //        sb.Append(b.ToString("X2"));
            //    }
            //    rtxbReceiveMsg.Text = sb.ToString();
            //    if (!isWrite)
            //    {
            //        var mode = (ReadMethod)readWriteMode;
            //        switch (mode)
            //        {
            //            case ReadMethod.ReadCoilState:
            //            case ReadMethod.ReadInputState:
            //                {
            //                    var list = MsgAnalysis.AnalysisBool(receivedBytes);
            //                    txbReceiveData.Text = string.Join(",", list.Take(Convert.ToInt16(receivedBytes[2])).Select(x => x.ToString()).ToList());
            //                }
            //                break;
            //            case ReadMethod.ReadHoldingRegister:
            //            case ReadMethod.ReadInputRegister:
            //                {
            //                    var list = MsgAnalysis.AnalysisUInt16(receivedBytes);
            //                    txbReceiveData.Text = string.Join(",", list.Take(Convert.ToInt16(receivedBytes[2])).Select(x => x.ToString()).ToList());
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //    }

            //}));
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] msg = null;
            var slave = (int)txbSlaveStation.Value;
            var start = (ushort)txbStartAddress.Value;
            var dataType = cbDataType.SelectedItem.ToString();
            var byteFormart = GetByteFormart();

            if (isWrite)
            {
                var arr = txbSendData.Text.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var mode = (FuncMode)readWriteMode;
                switch (mode)
                {
                    case FuncMode.WriteSingleCoil:
                        {
                            var data = arr.Select(x => bool.Parse(x)).ToList();
                            msg = MsgGenerator.GetWriteSingleCoilMessage(slave, start, data[0]);
                        }
                        break;
                    case FuncMode.WriteMulCoil:
                        {
                            var data = arr.Select(x => bool.Parse(x)).ToList();
                            msg = MsgGenerator.GetMulCoilWriteMessage(slave, start, data);
                        }
                        break;
                    case FuncMode.WriteSingleRegister:
                        {
                            switch (dataType)
                            {
                                case "ushort":
                                    {
                                        var data = arr.Select(x => ushort.Parse(x)).ToList();
                                        msg = MsgGenerator.GetSingleRegisterWriteMessage(slave, start, data[0], byteFormart);
                                    }
                                    break;
                                case "short":
                                    {
                                        var data = arr.Select(x => short.Parse(x)).ToList();
                                        msg = MsgGenerator.GetSingleRegisterWriteMessage(slave, start, data[0], byteFormart);
                                    }
                                    break;
                            }
                        }
                        break;
                    case FuncMode.WriteMulRegister:
                        {
                            switch (dataType)
                            {
                                case "ushort":
                                    {
                                        var data = arr.Select(x => ushort.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "short":
                                    {
                                        var data = arr.Select(x => short.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "uint":
                                    {
                                        var data = arr.Select(x => uint.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "int":
                                    {
                                        var data = arr.Select(x => int.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "ulong":
                                    {
                                        var data = arr.Select(x => ulong.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "long":
                                    {
                                        var data = arr.Select(x => long.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "float":
                                    {
                                        var data = arr.Select(x => float.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                case "double":
                                    {
                                        var data = arr.Select(x => double.Parse(x)).ToList();
                                        msg = MsgGenerator.GetMulRegisterWriteMessage(slave, start, data, byteFormart);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
                _serialPortClient.Send(msg);
            }
            else
            {
                string responseMsg;
                var length = (ushort)txbLength.Value;
                var readMode = (FuncMode)readWriteMode;
                msg = MsgGenerator.GetReadMessage(slave, readMode, start, length);
                switch (dataType)
                {
                    case "bool":
                        {
                            var res = _serialPortClient.Send<bool>(msg, byteFormart);
                            responseMsg = string.Join(",", res.Select(x => x.ToString()));
                        }
                        break;
                    case "ushort":
                        {
                            var res = _serialPortClient.Send<ushort>(msg, byteFormart);
                            responseMsg = string.Join(",", res.Select(x => x.ToString()));
                        }
                        break;
                    case "short":
                        {
                            var res = _serialPortClient.Send<short>(msg, byteFormart);
                            responseMsg = string.Join(",", res.Select(x => x.ToString()));
                        }
                        break;
                    case "int":
                        {
                            var res = _serialPortClient.Send<int>(msg, byteFormart);
                            responseMsg = string.Join(",", res.Select(x => x.ToString()));
                        }
                        break;
                    case "float":
                        {
                            var res = _serialPortClient.Send<float>(msg, byteFormart);
                            responseMsg = string.Join(",", res.Select(x => x.ToString()));
                        }
                        break;
                    case "double":
                        {
                            var res = _serialPortClient.Send<double>(msg, byteFormart);
                            responseMsg = string.Join(",", res.Select(x => x.ToString()));
                        }
                        break;
                    default:
                        responseMsg = "";
                        break;
                }
                txbReceiveData.Text = responseMsg;
                //var a = master.ReadCoils((byte)slave, (ushort)start, (ushort)length);
            }
            rtxbSendMsg.Text = string.Join(" ", msg.Select(x => x.ToString("X2")).ToList());

            //var str = rtxbSendMsg.Text.Trim();
            //str = str.Replace(" ", "").Replace("\r\n", "");

            ////将输入的16进制字符串两两分割为字符串集合
            //var strArr = Regex.Matches(str, ".{2}").Cast<Match>().Select(m => m.Value);

            ////需要发送的字节数组
            //byte[] data = new byte[strArr.Count()];

            ////循环索引
            //int temp = 0;

            ////将字符串集合转换为字节数组
            //foreach (string item in strArr)
            //{
            //    data[temp] = Convert.ToByte(item, 16);
            //    temp++;
            //}
            //var crc = CheckSum.CRC16(data);
            //var cmd = new List<byte>();
            //cmd.AddRange(data);
            //cmd.AddRange(crc);
            //发送字节

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (IsConnected)
                _serialPortClient.TryClose();
            labStatus.Text = "未连接";
            btnClose.Enabled = false;
            btnOpen.Enabled = true;
            btnSend.Enabled = false;
        }

        /// <summary>
        /// 读写模式切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //更新状态字段
            GetReadWriteMode();

            //计数复位
            txbLength.Value = 1;
            //清空输入值
            txbSendData.Clear();
            //是否可输入值
            txbSendData.Enabled = isWrite ? true : false;
            //是否可修改计数
            txbLength.Enabled = isWrite ? false : true;
        }

        /// <summary>
        /// 根据选中的读写模式更新字段值
        /// </summary>
        private void GetReadWriteMode()
        {
            switch (cbMethod.SelectedItem.ToString())
            {
                case "读取输出线圈":
                default:
                    isWrite = false;
                    readWriteMode = FuncMode.ReadCoilState;
                    break;

                case "读取离散输入":
                    isWrite = false;
                    readWriteMode = FuncMode.ReadInputState;
                    break;

                case "读取保持型寄存器":
                    isWrite = false;
                    readWriteMode = FuncMode.ReadHoldingRegister;
                    break;

                case "读取输入寄存器":
                    isWrite = false;
                    readWriteMode = FuncMode.ReadInputRegister;
                    break;

                case "写入单个线圈":
                    isWrite = true;
                    readWriteMode = FuncMode.WriteSingleCoil;
                    break;

                case "写入多个线圈":
                    isWrite = true;
                    readWriteMode = FuncMode.WriteMulCoil;
                    break;

                case "写入单个寄存器":
                    isWrite = true;
                    readWriteMode = FuncMode.WriteSingleRegister;
                    break;

                case "写入多个寄存器":
                    isWrite = true;
                    readWriteMode = FuncMode.WriteMulRegister;
                    break;
            }
        }
    }
}
