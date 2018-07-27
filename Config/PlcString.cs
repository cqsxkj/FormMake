/* 描述：plc遥控和遥信字符串
 * 时间：2018年3月8日15:13:42
 * 阿里木客
 * 
 * */

using System.Collections.Generic;
using WindowMake.Entity;

namespace WindowMake.Config
{
    public static class PlcString
    {
        public static PLCConfig p_config { get; set; }
        public static List<p_area_cfg> p_area_cfg { get; set; }
        public static string[] stryk2byte = { "10", "01" };
        public static string[] strYKtl = { "10", "01", "00" };
        public static string[] strYKtl1 = { "001", "010", "100" };
        public static string[] strYKTD = { "100", "010", "001" };
        public static string[] strYKhl2 = { "1000", "0100", "0010", "1001", "0000" };
        public static string[] strYKhl = { "100", "010", "001", "000" };
        public static string[] strYKjf = { "100", "010", "001" };

        public static string[] strYX2byte = { "10", "11", "00", "01" };
        public static string[] strYXtl = { "10", "01", "00" };
        public static string[] strYXTD = { "10", "01", "00" };
        public static string[] strYXhl2 = { "1000", "0100", "0010", "0000", "1001" };
        public static string[] strYXhl = { "100", "010", "001", "000" };
        public static string[] strYXjf = { "100", "101", "010", "011", "001", "000" };
    }
}
