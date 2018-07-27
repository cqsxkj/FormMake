using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl2UpObject : PLCEqu
    {
        public Ptl2UpObject(Point p)
        {
            this.init(p);
        }

        public Ptl2UpObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_TL2_UP;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL_UP.png";
            this.equ.EquName = "车行横通";
            equTypeName = equ.EquName;
        }
    }
}
