using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PcoObject : PLCEqu
    {
        public PcoObject(Point p)
        {
            this.init(p);
        }

        public PcoObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_CO;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "CO.png";
            this.equ.EquName = "CO";
        }
    }
}
