using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// Modbus功能码
    /// </summary>
    public enum FuncMode
    {
        /// <summary>
        /// 读取线圈寄存器
        /// </summary>
        [Description("读取线圈寄存器")]
        ReadCoilState = 0x01,
        /// <summary>
        /// 读取离散输入寄存器
        /// </summary>
        [Description("读取离散输入寄存器")]
        ReadInputState = 0x02,
        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        [Description("读取保持寄存器")]
        ReadHoldingRegister = 0x03,
        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        [Description("读取输入寄存器")]
        ReadInputRegister = 0x04,
        /// <summary>
        /// 写单个线圈寄存器
        /// </summary>
        [Description("写单个线圈寄存器")]
        WriteSingleCoil = 0x05,
        /// <summary>
        /// 写单个保持寄存器
        /// </summary>
        [Description("写单个保持寄存器")]
        WriteSingleRegister = 0x06,
        /// <summary>
        /// 写多个线圈寄存器
        /// </summary>
        [Description("写多个线圈寄存器")]
        WriteMulCoil = 0x0F,
        /// <summary>
        /// 写多个保持寄存器
        /// </summary>
        [Description("写多个保持寄存器")]
        WriteMulRegister = 0x10
    }
}
