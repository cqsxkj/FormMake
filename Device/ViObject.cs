using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class ViObject : MyObject
    {
        public ViObject(Point p)
        {
            this.init(p);
        }

        public ViObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.VI;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "VI.png";
            this.equ.EquName = "气象仪";
        }
    }
}
