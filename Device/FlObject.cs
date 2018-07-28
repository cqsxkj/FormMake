using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class FlObject : FireEqu
    {
        public FlObject(PointF p)
        {
            this.init(p);
        }

        public FlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.F_L;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F_L.png";
            this.equ.EquName = "火灾光纤";
        }
    }
}
