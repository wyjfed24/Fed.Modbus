using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fed.Modbus.Rtu
{
    /// <summary>
    /// 校验器
    /// </summary>
    public static class CheckSum
    {
        /// <summary>
        /// 生成CRC16校验码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] CreateCRC16(byte[] data)
        {
            int len = data.Length;
            if (len > 0)
            {
                ushort crc = 0xFFFF;
                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                    }
                }
                byte high = (byte)((crc & 0xFF00) >> 8); //高位置
                byte low = (byte)(crc & 0x00FF); //低位置

                return BitConverter.IsLittleEndian ? new byte[] { low, high } : new byte[] { high, low };
            }
            return new byte[] { 0, 0 };
        }

        /// <summary>
        /// 校验响应报文
        /// </summary>
        /// <param name="receiveMsg"></param>
        /// <returns></returns>
        public static bool ValidateCRC16(byte[] receiveMsg)
        {
            //报文原始校验码
            var sourceCrc16 = receiveMsg.Skip(receiveMsg.Length - 2).Take(2).ToArray();
            //命令部分报文
            var codePartMsg = receiveMsg.Skip(0).Take(receiveMsg.Length - 2).ToArray();
            //计算的校验码
            byte[] crc16 = CreateCRC16(codePartMsg);
            var result = Enumerable.SequenceEqual(sourceCrc16, crc16);
            return result;
        }
    }
}
