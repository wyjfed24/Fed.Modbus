
using Fed.Modbus.Rtu.ModBusRTU;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fed.Modbus.Rtu.Extension
{
    public static class ByteExt
    {
        /// <summary>
        /// 获取类型占位字节数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetByteSize<T>() where T : struct
        {
            var size = default(T) switch
            {
                bool => 1,
                ushort => 2,
                short => 2,
                uint => 4,
                int => 4,
                ulong => 8,
                long => 8,
                float => 4,
                double => 8,
                _ => 0
            };
            return size;
        }

        /// <summary>
        /// 转换字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static byte[] ToBytes<T>(this T value) where T : struct
        {
            var bytes = default(T) switch
            {
                ushort => BitConverter.GetBytes(Convert.ToUInt16(value)),
                short => BitConverter.GetBytes(Convert.ToInt16(value)),
                uint => BitConverter.GetBytes(Convert.ToUInt32(value)),
                int => BitConverter.GetBytes(Convert.ToInt32(value)),
                ulong => BitConverter.GetBytes(Convert.ToUInt64(value)),
                long => BitConverter.GetBytes(Convert.ToInt64(value)),
                float => BitConverter.GetBytes(Convert.ToSingle(value)),
                double => BitConverter.GetBytes(Convert.ToDouble(value)),
                _ => throw new Exception("类型不支持")
            };
            return bytes;
        }

        /// <summary>
        /// 按位两两交换字节数组
        /// <para>  A B C D => B A D C</para>
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] JumpChange(this byte[] bytes)
        {
            var length = bytes.Length;//获取位数
            if (length % 2 != 0)
                throw new Exception("字节数据位数异常");
            for (int i = 0; i < length; i += 2)
            {
                var temp = bytes[i];
                bytes[i] = bytes[i + 1];
                bytes[i + 1] = temp;
            }
            return bytes;
        }

        /// <summary>
        /// 指定格式字节数组转换为系统格式字节数组
        /// </summary>
        /// <param name="bytes">源格式字节数组</param>
        /// <param name="sourceFormart">源格式</param>
        /// <returns></returns>
        public static byte[] ToSystemFormart(this byte[] bytes, ByteFormart sourceFormart)
        {
            //系统为小端处理
            if (BitConverter.IsLittleEndian)
            {
                bytes = sourceFormart switch
                {
                    ByteFormart.BigEndian => bytes.Reverse().ToArray(),
                    ByteFormart.BigEndianByteSwap => bytes.Reverse().ToArray().JumpChange(),
                    ByteFormart.LittleEndianByteSwap => bytes.JumpChange(),
                    _ => bytes
                };
            }
            else//系统为大端处理
            {
                bytes = sourceFormart switch
                {
                    ByteFormart.LittleEndian => bytes.Reverse().ToArray(),
                    ByteFormart.BigEndianByteSwap => bytes.JumpChange(),
                    ByteFormart.LittleEndianByteSwap => bytes.Reverse().ToArray().JumpChange(),
                    _ => bytes
                };
            }
            return bytes;
        }

        /// <summary>
        /// 系统格式字节数组转换为指定格式字节数组
        /// </summary>
        /// <param name="bytes">系统格式字节数组</param>
        /// <param name="targetFormart">目标格式</param>
        /// <returns></returns>
        public static byte[] ToTargetFormart(this byte[] bytes, ByteFormart targetFormart)
        {
            //系统为小端处理
            if (BitConverter.IsLittleEndian)
            {
                bytes = targetFormart switch
                {
                    ByteFormart.BigEndian => bytes.Reverse().ToArray(),
                    ByteFormart.BigEndianByteSwap => bytes.Reverse().ToArray().JumpChange(),
                    ByteFormart.LittleEndianByteSwap => bytes.JumpChange(),
                    _ => bytes
                };
            }
            else//系统为大端处理
            {
                bytes = targetFormart switch
                {
                    ByteFormart.LittleEndian => bytes.Reverse().ToArray(),
                    ByteFormart.BigEndianByteSwap => bytes.JumpChange(),
                    ByteFormart.LittleEndianByteSwap => bytes.Reverse().ToArray().JumpChange(),
                    _ => bytes
                };
            }
            return bytes;
        }

        /// <summary>
        /// 互换byte高低位
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte ChangeHighLowBit(this byte b)
        {
            var newByte = (byte)((b >> 4 & 0x0F) + (b << 4 & 0xF0));
            return newByte;
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ChangeType<T>(this byte[] bytes) where T : struct
        {
            object value = default(T) switch
            {
                ushort => BitConverter.ToUInt16(bytes, 0),
                short => BitConverter.ToInt16(bytes, 0),
                uint => BitConverter.ToUInt32(bytes, 0),
                int => BitConverter.ToInt32(bytes, 0),
                ulong => BitConverter.ToUInt64(bytes, 0),
                long => BitConverter.ToInt64(bytes, 0),
                float => BitConverter.ToSingle(bytes, 0),
                double => BitConverter.ToDouble(bytes, 0),
                _ => null
            };
            return (T)value;
        }
    }
}
