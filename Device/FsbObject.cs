using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class FsbObject : FireEqu
    {
        public FsbObject(PointF p)
        {
            this.init(p);
        }

        public FsbObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.F_SB;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F_SB.png";
            this.equ.EquName = "火灾手报";
        }
    }
}
