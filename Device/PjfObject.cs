using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PjfObject : PLCEqu
    {
        public PjfObject(Point p)
        {
            this.init(p);
        }

        public PjfObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_JF;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_JF.png";
            this.equ.EquName = "射流风机";
            equTypeName = equ.EquName;
        }
    }
}
