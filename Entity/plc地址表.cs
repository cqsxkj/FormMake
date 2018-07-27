/* 功能描述：交大数据库中设备表
 * 
 * 
 * 
 * */
namespace WindowMake.Entity
{
    public class plc地址表
    {
        public double 编号 { get; set; }
        public string 设备类型 { get; set; }
        public int 设备隧道号 { get; set; }
        public string 隧道描述 { get; set; }
        public long 站地址 { get; set; }
        public string YX起始地址 { get; set; }
        public string YX查询位数 { get; set; }
        public string YC起始地址 { get; set; }
        public string YC查询字节数 { get; set; }
        public string IP地址 { get; set; }
        public int? 通讯端口号 { get; set; }
        public int? 通讯字长 { get; set; }
        public double 串口设备数 { get; set; }
        public string 串口设备类型 { get; set; }
        public string 串口设备类型名 { get; set; }
        public string 串口站号 { get; set; }
        public string 设备类别 { get; set; }
        public string 备注 { get; set; }
    }
}
