using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PviObject : PLCEqu
    {
        public PviObject(Point p)
        {
            this.init(p);
        }

        public PviObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_VI;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_VI.png";
            this.equ.EquName = "VI";
        }
    }
}
