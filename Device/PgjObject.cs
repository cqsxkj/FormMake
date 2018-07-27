using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PgjObject : PLCEqu
    {
        public PgjObject(Point p)
        {
            this.init(p);
        }

        public PgjObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_GJ;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_GJ.png";
            this.equ.EquName = "光强检测";
        }
    }
}
