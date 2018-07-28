using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class Phl2Object : PLCEqu
    {
        public Phl2Object(PointF p)
        {
            this.init(p);
        }

        public Phl2Object()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_HL2;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_HL2.png";
            this.equ.EquName = "四显交通灯";
        }
    }
}
