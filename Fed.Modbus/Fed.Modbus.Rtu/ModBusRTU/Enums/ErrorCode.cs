using System.ComponentModel;

namespace Fed.Modbus.Rtu.ModBusRTU
{
    /// <summary>
    /// Modbus错误码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 非法功能
        /// </summary>
        [Description("非法功能")]
        IllegalFunction = 0x01,
        /// <summary>
        /// 非法数据地址
        /// </summary>
        [Description("非法数据地址")]
        IllegalAddress = 0x02,
        /// <summary>
        /// 非法数据值
        /// </summary>
        [Description("非法数据值")]
        IllegalData = 0x03,
        /// <summary>
        /// 从站设备故障
        /// </summary>
        [Description("从站设备故障")]
        SlaveDeviceBreakdown = 0x04,
        /// <summary>
        /// 从站设备接受请求正在处理
        /// </summary>
        [Description("从站设备接受请求正在处理")]
        Ack = 0x05,
        /// <summary>
        /// 从站设备忙
        /// </summary>
        [Description("从站设备忙")]
        Busy = 0x07,
        /// <summary>
        /// 存储奇偶性差错
        /// </summary>
        [Description("存储奇偶性差错")]
        StorageParityError = 0x08,
        /// <summary>
        /// 不可用网关路径
        /// </summary>
        [Description("不可用网关路径")]
        UnavailableGateway = 0x0A,
        /// <summary>
        /// 网关目标设备响应失败
        /// </summary>
        [Description("网关目标设备响应失败")]
        DeviceResponseFailed = 0x0B,
    }
}
