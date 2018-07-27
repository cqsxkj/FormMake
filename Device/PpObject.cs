using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PpObject : PLCEqu
    {
        public PpObject(Point p)
        {
            this.init(p);
        }

        public PpObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_P;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_P.png";
            this.equ.EquName = "水泵";
            equTypeName = equ.EquName;
        }
    }
}
