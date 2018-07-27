using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PtwObject : PLCEqu
    {
        public PtwObject(Point p)
        {
            this.init(p);
        }

        public PtwObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_TW;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TW.png";
            this.equ.EquName = "风速风向";
        }
    }
}
