using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class EprObject : EPEqu
    {
        public EprObject(Point p)
        {
            this.init(p);
        }

        public EprObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.EP_R;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "EP_R.png";
            this.equ.EquName = "紧急电话广播";
        }
    }
}
