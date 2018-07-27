using System;
using System.Drawing;

namespace WindowMake.Entity
{
    public class Equ
    {
        public string EquID { get; set; }
        public string EquName { get; set; }
        public string EquTypeID { get; set; }
        public Point LocationMap { get; set; }
        public string PointX { get; set; }
        public string PointY { get; set; }
        public string PileNo { get; set; }
        public string Code { get; set; }
        public string MapID { get; set; }
        public Nullable<int> DirectionID { get; set; }
        public string AddressDiscribe { get; set; }
        public string AlarmMethod { get; set; }
        public string IP { get; set; }
        public Nullable<int> Port { get; set; }
        public string FatherEquID { get; set; }
        public Nullable<float> TaskWV { get; set; }
        public Nullable<int> threadInfoID { get; set; }
        public Nullable<int> dllID { get; set; }
        public Nullable<int> msgTimeoutSec { get; set; }
        public string Encode { get; set; }
        public string Note { get; set; }
        public string plcStationAddress { get; set; }
        public string Vendor { get; set; }
        public string RunMode { get; set; }
    }
}
