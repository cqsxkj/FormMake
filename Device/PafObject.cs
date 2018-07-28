using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PafObject : PLCEqu
    {
        public PafObject(PointF p)
        {
            this.init(p);
        }

        public PafObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_AF;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_AF_B_Close.png";
            this.equ.EquName = "轴流风机";
        }
    }
}
