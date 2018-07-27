/* 功能：收费站信息
 * 时间：2018-7-21 21:26:45
 * 
 * 
 * */

using System;

namespace WindowMake.Entity
{
    public partial class tollInfo
    {
        public string BM { get; set; }
        public string Name { get; set; }
        public string Stake { get; set; }
        public Nullable<int> MTCLanesNum { get; set; }
        public Nullable<int> ETCLanesNum { get; set; }
        public string PointX { get; set; }
        public string PointY { get; set; }
        public Nullable<short> Direction { get; set; }
        public string MapID { get; set; }
        public string Mesg { get; set; }
        public string IP { get; set; }
        public Nullable<int> Port { get; set; }
    }
}
