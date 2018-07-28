using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class CMObject : CMSEqu
    {
        public CMObject(PointF p)
        {
            this.init(p);
        }

        public CMObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.CM;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "cm.png";
            this.equ.EquName = "门架情报板";
        }
    }
}
