using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl5LeftObject : PLCEqu
    {
        public Ptl5LeftObject(Point p)
        {
            this.init(p);
        }

        public Ptl5LeftObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_TL5_Left;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL_Left.png";
            this.equ.EquName = "车道指示器";
            equTypeName = equ.EquName;
        }
    }
}
