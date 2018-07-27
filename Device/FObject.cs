using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class FObject : FireEqu
    {
        public FObject(Point p)
        {
            this.init(p);
        }

        public FObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.F;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F.png";
            this.equ.EquName = "火灾主机";
        }
    }
}
