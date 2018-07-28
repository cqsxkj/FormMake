using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class CLObject : CMSEqu
    {
        public CLObject(PointF p)
        {
            this.init(p);
        }

        public CLObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.CL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "cl.png";
            this.equ.EquName = "立柱情报板";
        }
    }
}
