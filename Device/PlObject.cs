using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PlObject : PLCEqu
    {
        public PlObject(Point p)
        {
            this.init(p);
        }

        public PlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_L;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_L.png";
            this.equ.EquName = "基本照明";
            equTypeName = equ.EquName;
        }
    }
}
