using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class FlObject : FireEqu
    {
        public FlObject(Point p)
        {
            this.init(p);
        }

        public FlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.F_L;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F_L.png";
            this.equ.EquName = "火灾光纤";
        }
    }
}
