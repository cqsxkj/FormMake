using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PKObject : MyObject
    {
        public PKObject(PointF p)
        {
            this.init(p);
        }

        public PKObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.PK;
            equ.EquID = (int)equtype+"0001";
            picName = "PARK.png";
            equ.EquName = "停车场车位检测";
        }
    }
}
