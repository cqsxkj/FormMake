using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class FsbObject : FireEqu
    {
        public FsbObject(Point p)
        {
            this.init(p);
        }

        public FsbObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.F_SB;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F_SB.png";
            this.equ.EquName = "火灾手报";
        }
    }
}
