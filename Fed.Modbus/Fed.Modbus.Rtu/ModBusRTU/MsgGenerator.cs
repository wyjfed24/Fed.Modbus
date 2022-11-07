using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fed.Modbus.Rtu.Extension;


namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// 报文生成器
    /// </summary>
    public static class MsgGenerator
    {
        #region 读报文生成

        /// <summary>
        /// 获取读取数据请求报文
        /// </summary>
        /// <param name="slaveStation">从站地址</param>
        /// <param name="readMethod">读取模式</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        public static byte[] GetReadMessage(int slaveStation, FuncMode readMethod, ushort startAddress, ushort length)
        {
            var resultBytes = new List<byte>();
            //获取起始地址及读取长度
            var startAddressBytes = startAddress.ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            var lengthBytes = length.ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //依次放入头两位字节（站地址和读取模式）
            resultBytes.Add((byte)slaveStation);
            resultBytes.Add((byte)readMethod);
            //插入起始地址
            resultBytes.AddRange(startAddressBytes);
            //插入读取长度
            resultBytes.AddRange(lengthBytes);
            //获取校验码并在最后放入
            var crc16 = CheckSum.CreateCRC16(resultBytes.ToArray());
            resultBytes.AddRange(crc16);
            return resultBytes.ToArray();
        }

        #endregion

        #region 写报文生成

        #region 线圈

        /// <summary>
        /// 获取写入单个线圈的报文
        /// </summary>
        /// <param name="slaveStation">从站地址</param>
        /// <param name="startAdr">线圈地址</param>
        /// <param name="value">写入值</param>
        /// <returns>写入单个线圈的报文</returns>
        public static byte[] GetWriteSingleCoilMessage(int slaveStation, ushort startAddress, bool value)
        {
            var resultBytes = new List<byte>();
            //从站地址
            byte slaveStationByte = (byte)slaveStation;
            //功能码
            byte method = (byte)FuncMode.WriteSingleCoil;
            //获取线圈地址
            byte[] startAddressBytes = startAddress.ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //插入站地址
            resultBytes.Add(slaveStationByte);
            //插入功能码
            resultBytes.Add(method);
            //插入线圈地址
            resultBytes.AddRange(startAddressBytes);
            //插入写入值
            resultBytes.Add((byte)(value ? 0xFF : 0x00));
            //补位
            resultBytes.Add(0x00);
            //计算校验码并拼接，返回最后的报文结果
            var crc16 = CheckSum.CreateCRC16(resultBytes.ToArray());
            resultBytes.AddRange(crc16);
            return resultBytes.ToArray();
        }

        /// <summary>
        /// 获取写入多个线圈的报文
        /// </summary>
        /// <param name="slaveStation">从站地址</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="values">写入值</param>
        /// <returns>写入多个线圈的报文</returns>
        public static byte[] GetMulCoilWriteMessage(int slaveStation, ushort startAddress, IEnumerable<bool> values)
        {
            var resultBytes = new List<byte>();
            //从站地址
            byte slaveStationByte = (byte)slaveStation;
            //功能码
            byte method = (byte)FuncMode.WriteMulCoil;
            //获取线圈地址
            byte[] startAddressBytes = startAddress.ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //获取写入线圈数量
            byte[] lengthBytes = Convert.ToInt16(values.Count()).ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //转换写入值字节集合
            var valueBytes = ConvertToBytes(values);
            //获取写入值的字节数
            byte byteCount = (byte)valueBytes.Length;
            //插入站地址
            resultBytes.Add(slaveStationByte);
            //插入功能码
            resultBytes.Add(method);
            //插入线圈地址
            resultBytes.AddRange(startAddressBytes);
            //插入写入线圈数量
            resultBytes.AddRange(lengthBytes);
            //插入写入值的字节数
            resultBytes.Add(byteCount);
            //插入值字节集合
            resultBytes.AddRange(valueBytes);
            //计算校验码并拼接，返回最后的报文结果
            var crc16 = CheckSum.CreateCRC16(resultBytes.ToArray());
            resultBytes.AddRange(crc16);
            return resultBytes.ToArray();
        }

        #region 辅助方法

        /// <summary>
        /// 布尔集合转换比特数组
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static byte[] ConvertToBytes(IEnumerable<bool> values)
        {
            //定义写入值字节集合
            var resultBytes = new List<byte>();
            //由于一个字节只有八个位，所以如果需要写入的值超过了八个，
            //则需要生成一个新的字节用以存储，
            //所以循环截取输入的值，然后生成对应的写入值字节
            for (int i = 0; i < values.Count(); i += 8)
            {
                //写入值字节临时字节集合
                var curValues = values.Skip(i).Take(8).ToList();
                //剩余位不足八个，则把剩下的所有位都放到同一个字节里
                if (curValues.Count != 8)
                {
                    //取余获取剩余的位的数量
                    int m = values.Count() % 8;
                    //截取位放入临时字节集合中
                    curValues = values.Skip(i).Take(m).ToList();
                }
                //获取位生成的写入值字节
                byte tempByte = GetBitArray(curValues);
                //将生成的写入值字节拼接到写入值字节集合中
                resultBytes.Add(tempByte);
            }
            return resultBytes.ToArray();
        }

        /// <summary>
        /// 反转顺序并生成字节
        /// </summary>
        /// <param name="values">位数据</param>
        /// <returns></returns>
        public static byte GetBitArray(IEnumerable<bool> values)
        {
            var reverseValues = values;

            //定义初始字节，值为0000 0000
            byte bin = 0x00;
            //循环计数
            int index = 0;
            //循环位集合
            foreach (bool item in reverseValues)
            {
                //判断每一位的数据，为true则左移一个1到对应的位置
                //0000 0000
                //0000 0001
                //0000 0010
                if (item)
                {
                    bin = (byte)(bin | (0x01 << index));
                }
                //计数+1
                index++;
            }
            //返回最后使用位数据集合生成的二进制字节
            return bin;
        }

        #endregion

        #endregion

        #region 寄存器

        /// <summary>
        /// 获取写入单个寄存器的报文
        /// </summary>
        /// <param name="slaveStation">从站地址</param>
        /// <param name="startAddress">寄存器地址</param>
        /// <param name="value">写入值</param>
        /// <typeparam name="T">ushort,short</typeparam>
        /// <returns>写入单个寄存器的报文</returns>
        public static byte[] GetSingleRegisterWriteMessage<T>(int slaveStation, ushort startAddress, T value, ByteFormart byteFormart = ByteFormart.BigEndian) where T : struct
        {
            if (value is not short && value is not ushort)
                throw new Exception("单个寄存器仅支持short和ushort类型的写入");
            var resultBytes = new List<byte>();
            //从站地址
            byte slaveStationByte = (byte)slaveStation;
            //功能码
            byte method = (byte)FuncMode.WriteSingleRegister;
            //寄存器地址
            byte[] startAddressBytes = startAddress.ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //值
            byte[] valueBytes = value.ToBytes().ToTargetFormart(byteFormart);
            //插入站地址
            resultBytes.Add(slaveStationByte);
            //插入功能码
            resultBytes.Add(method);
            //插入线圈地址
            resultBytes.AddRange(startAddressBytes);
            //插入写入值
            resultBytes.AddRange(valueBytes);
            //计算校验码并拼接，返回最后的报文结果
            var crc16 = CheckSum.CreateCRC16(resultBytes.ToArray());
            resultBytes.AddRange(crc16);
            return resultBytes.ToArray();
        }

        /// <summary>
        /// 获取写入多个连续寄存器的报文
        /// </summary>
        /// <param name="slaveStation">从站地址</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="values">写入值</param>
        /// <typeparam name="T">ushort，short，uint，int，ulong，long，float，double</typeparam>
        /// <returns>写入多个寄存器的报文</returns>
        public static byte[] GetMulRegisterWriteMessage<T>(int slaveStation, ushort startAddress, IEnumerable<T> values, ByteFormart byteFormart = ByteFormart.BigEndian) where T : struct
        {
            var resultBytes = new List<byte>();
            //从站地址
            byte slaveStationByte = (byte)slaveStation;
            //功能码
            byte method = (byte)FuncMode.WriteMulRegister;
            //获取寄存器地址
            byte[] startAddressBytes = startAddress.ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //获取类型占用字节位数
            var unitLength = ByteExt.GetByteSize<T>();
            //一个寄存器=2个byte位，寄存器数量=值数量*值站位/2
            var realCount = values.Count() * unitLength / 2;
            //获取写入寄存器数量,强制转int16使byte[]长度为2位，否则Count()为int32会转成4位byte[]
            byte[] lengthBytes = Convert.ToInt16(realCount).ToBytes().ToTargetFormart(ByteFormart.BigEndian);
            //值集合转换比特数组
            var valueBytes = values.Select(x => x.ToBytes()).SelectMany(x => x.ToTargetFormart(byteFormart)).ToArray();
            //获取写入值的字节数
            var countByte = Convert.ToByte(valueBytes.Length);
            //插入站地址
            resultBytes.Add(slaveStationByte);
            //插入功能码
            resultBytes.Add(method);
            //插入线圈地址
            resultBytes.AddRange(startAddressBytes);
            //插入写入线圈数量
            resultBytes.AddRange(lengthBytes);
            //插入写入值的字节数
            resultBytes.Add(countByte);
            //插入写入值字节集合
            resultBytes.AddRange(valueBytes);
            //计算校验码并拼接，返回最后的报文结果
            var crc16 = CheckSum.CreateCRC16(resultBytes.ToArray());
            resultBytes.AddRange(crc16);
            return resultBytes.ToArray();
        }

        #region 辅助方法

        #endregion

        #endregion

        #endregion
    }
}
