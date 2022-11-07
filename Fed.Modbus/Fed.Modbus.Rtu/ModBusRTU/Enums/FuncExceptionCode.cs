using System.ComponentModel;

namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// Modbus异常功能码
    /// </summary>
    public enum FuncExceptionCode
    {
        /// <summary>
        /// 读取线圈异常
        /// </summary>
        [Description("读取线圈异常")]
        ReadCoilStateError = 0x81,
        /// <summary>
        /// 读取离散输入寄存器异常
        /// </summary>
        [Description("读取离散输入寄存器异常")]
        ReadInputStateError = 0x82,
        /// <summary>
        /// 读取保持寄存器异常
        /// </summary>
        [Description("读取保持寄存器异常")]
        ReadHoldingRegisterError = 0x83,
        /// <summary>
        /// 读取输入寄存器异常
        /// </summary>
        [Description("读取输入寄存器异常")]
        ReadInputRegisterError = 0x84,
        /// <summary>
        /// 写单个线圈异常
        /// </summary>
        [Description("写单个线圈异常")]
        WriteSingleCoilError = 0x85,
        /// <summary>
        /// 写单个保持寄存器异常
        /// </summary>
        [Description("写单个保持寄存器异常")]
        WriteSingleRegisterError = 0x86,
        /// <summary>
        /// 写多个线圈异常
        /// </summary>
        [Description("写多个线圈异常")]
        WriteMulCoilError = 0x8F,
        /// <summary>
        /// 写多个保持寄存器异常
        /// </summary>
        [Description("写多个保持寄存器异常")]
        WriteMulRegisterError = 0x90,
        /// <summary>
        /// 屏蔽写寄存器异常
        /// </summary>
        [Description("屏蔽写寄存器异常")]
        ShieldWriteRegisterError = 0x96,
        /// <summary>
        /// 读/写多个保持寄存器异常
        /// </summary>
        [Description("读/写多个保持寄存器异常")]
        ReadWriteMulRegisterError = 0x97,
    }
}
