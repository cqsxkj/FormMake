using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class FObject : FireEqu
    {
        public FObject(PointF p)
        {
            this.init(p);
        }

        public FObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.F;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F.png";
            this.equ.EquName = "火灾主机";
        }
    }
}
