using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// Modbus大小端读取格式
    /// </summary>
    public enum ByteFormart
    {
        /// <summary>
        /// BigEndian(大端)(大部分PLC默认排序方法)(4字节AB CD)/(8字节AB CD EF GH)
        /// </summary>
        [Description("BigEndian")]
        BigEndian = 1,
        /// <summary>
        /// LittleEndian(小端) (4字节DC BA)/(8字节HG FE DC BA)
        /// </summary>
        [Description("LittleEndian")]
        LittleEndian = 2,
        /// <summary>
        /// BigEndianByteSwap(4字节BA DC)/(8字节BA DC FE HG) 
        /// </summary>
        [Description("BigEndianByteSwap")]
        BigEndianByteSwap = 3,
        /// <summary>
        /// LittleEndianByteSwap(4字节CD AB)/(8字节GH EF CD AB) 
        /// </summary>
        [Description("LittleEndianByteSwap")]
        LittleEndianByteSwap = 4
    }
}
