using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PgjObject : PLCEqu
    {
        public PgjObject(PointF p)
        {
            this.init(p);
        }

        public PgjObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_GJ;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_GJ.png";
            this.equ.EquName = "光强检测";
        }
    }
}
