using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl2CloseObject : PLCEqu
    {
        public Ptl2CloseObject(Point p)
        {
            this.init(p);
        }

        public Ptl2CloseObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_TL2_Close;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL2_Close.png";
            this.equ.EquName = "车道指示器";
            equTypeName = equ.EquName;
        }
    }
}
