using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class EprObject : EPEqu
    {
        public EprObject(PointF p)
        {
            this.init(p);
        }

        public EprObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EP_R;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "EP_R.png";
            this.equ.EquName = "紧急电话广播";
        }
    }
}
