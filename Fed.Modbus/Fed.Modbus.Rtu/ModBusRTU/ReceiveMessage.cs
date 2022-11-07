using Fed.Modbus.Rtu.Extension;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// 响应报文
    /// </summary>
    internal class ReceiveMessage<T> where T : struct
    {
        /// <summary>
        /// 从站地址
        /// </summary>
        public ushort SlaveStation { get; }
        /// <summary>
        /// 功能码
        /// </summary>
        public FuncMode FuncMode { get; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Data { get; }
        /// <summary>
        /// 数据类型占用字节位数
        /// </summary>
        public int ByteSize { get; }
        /// <summary>
        /// 是否是布尔类型
        /// </summary>
        public bool IsBoolType { get; }

        public ReceiveMessage(byte[] receiveMsg)
        {
            //泛型类型验证
            var defaultT = default(T);
            if (defaultT is not ushort && defaultT is not short &&
                defaultT is not uint && defaultT is not int &&
                defaultT is not ulong && defaultT is not long &&
                defaultT is not float && defaultT is not double && defaultT is not bool)
                throw new Exception("响应报文仅支持bool，ushort，short，uint，int，ulong，long，float，double类型的解析");
            //数据完整性验证
            if (receiveMsg == null || receiveMsg.Length < 5)
            {
                throw new Exception("接收数据不完整");
            }
            //功能码
            var func = Convert.ToInt32(receiveMsg[1]);
            //判断是否异常
            if (Enum.IsDefined(typeof(FuncExceptionCode), func))
            {
                var funcErrorCode = (FuncExceptionCode)func;
                var code = receiveMsg[2];
                var errorCode = (ErrorCode)code;
                throw new Exception($"{funcErrorCode}[{func.ToString("X2")}]：{errorCode}[{code.ToString("X2")}]");
            }
            //校验码验证
            if (!CheckSum.ValidateCRC16(receiveMsg))
            {
                throw new Exception("响应报文CRC16校验不通过");
            }
            //是否是布尔
            IsBoolType = defaultT is bool;
            //获取功能码
            FuncMode = (FuncMode)func;
            //获取字节数
            Length = Convert.ToInt32(receiveMsg[2]);
            //获取数据部分
            Data = receiveMsg.Skip(3).Take(Length).ToArray();
            //获取类型占用字节位数
            ByteSize = ByteExt.GetByteSize<T>();
            //校验类型是否匹配
            if (Length % ByteSize != 0)
            {
                throw new Exception($"接收数据长度“{Length}”和类型“{defaultT.GetType().Name}”不匹配");
            }
            //读取寄存器时，如果返回类型需要转bool，则需要反转数据高低位
            if (IsBoolType && (FuncMode == FuncMode.ReadHoldingRegister || FuncMode == FuncMode.ReadInputRegister))
            {
                Data.JumpChange();
            }
        }
    }
}
