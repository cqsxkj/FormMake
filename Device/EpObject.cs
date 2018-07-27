using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class EpObject : EPEqu
    {
        public EpObject(Point p)
        {
            this.init(p);
        }

        public EpObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.EP;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "EP.png";
            this.equ.EquName = "紧急电话主机";
        }
    }
}
