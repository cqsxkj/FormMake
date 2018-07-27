using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class Phl2Object : PLCEqu
    {
        public Phl2Object(Point p)
        {
            this.init(p);
        }

        public Phl2Object()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_HL2;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_HL2.png";
            this.equ.EquName = "四显交通灯";
        }
    }
}
