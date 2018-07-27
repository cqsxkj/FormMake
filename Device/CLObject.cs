using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class CLObject : CMSEqu
    {
        public CLObject(Point p)
        {
            this.init(p);
        }

        public CLObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.CL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "cl.png";
            this.equ.EquName = "立柱情报板";
        }
    }
}
