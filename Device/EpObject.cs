using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class EpObject : EPEqu
    {
        public EpObject(PointF p)
        {
            this.init(p);
        }

        public EpObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EP;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "EP.png";
            this.equ.EquName = "紧急电话主机";
        }
    }
}
