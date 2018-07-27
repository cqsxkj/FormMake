using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PhlObject : PLCEqu
    {
        public PhlObject(Point p)
        {
            this.init(p);
        }

        public PhlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_HL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_HL.png";
            this.equ.EquName = "三显交通灯";
        }
    }
}
