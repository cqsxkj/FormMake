using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class EptObject : EPEqu
    {
        public EptObject(PointF p)
        {
            this.init(p);
        }

        public EptObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EP_T;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "EP_T.png";
            this.equ.EquName = "紧急电话";
        }
    }
}
