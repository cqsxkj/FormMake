using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PlyjObject : PLCEqu
    {
        public PlyjObject(Point p)
        {
            this.init(p);
        }

        public PlyjObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_LYJ;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_LYJ.png";
            this.equ.EquName = "应急照明";
        }
    }
}
