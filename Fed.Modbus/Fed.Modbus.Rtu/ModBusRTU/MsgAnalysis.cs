using Fed.Modbus.Rtu.Extension;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// 响应报文解析器
    /// </summary>
    public static class MsgAnalysis
    {
        /// <summary>
        /// 解析响应报文
        /// </summary>
        /// <param name="receiveMsg">接收消息</param>
        /// <param name="formart">字节数组格式</param>
        /// <typeparam name="T">bool，ushort，short，uint，int，ulong，long，float，double</typeparam>
        /// <returns>返回第一个数据</returns>
        public static T AnalysisMessageSingle<T>(byte[] receiveMsg, ByteFormart formart = ByteFormart.BigEndian) where T : struct
        {
            var results = AnalysisMessage<T>(receiveMsg, formart);
            var value = results.FirstOrDefault();
            return value;
        }

        /// <summary>
        /// 解析响应报文
        /// </summary>
        /// <param name="receiveMsg"></param>
        /// <param name="length">占用字节</param>
        /// <param name="formart">字节数组格式</param>
        /// <typeparam name="T">bool，ushort，short，uint，int，ulong，long，float，double</typeparam>
        /// <returns></returns>
        public static List<T> AnalysisMessage<T>(byte[] receiveMsg, ByteFormart formart = ByteFormart.BigEndian) where T : struct
        {
            //组装消息
            var message = new ReceiveMessage<T>(receiveMsg);
            var result = new List<T>();
            //布尔特殊处理
            if (message.IsBoolType)
            {
                //获取线圈状态
                var bitArray = new BitArray(message.Data);
                //按位转布尔
                foreach (var item in bitArray)
                {
                    result.Add((T)item);
                }
            }
            else
            {
                //按所占字节转数字
                for (int i = 0; i < message.Length; i += message.ByteSize)
                {
                    //获取一个单位的数据
                    var itemBytes = message.Data.Skip(i).Take(message.ByteSize).ToArray();
                    itemBytes = itemBytes.ToSystemFormart(formart);
                    //转换数字结果
                    var value = itemBytes.ChangeType<T>();
                    result.Add(value);
                }
            }
            return result;
        }
    }
}
