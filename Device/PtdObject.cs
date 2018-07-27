using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PtdObject : PLCEqu
    {
        public PtdObject(Point p)
        {
            this.init(p);
        }

        public PtdObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_TD;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TD.png";
            this.equ.EquName = "横通门";
            equTypeName = equ.EquName;
        }
    }
}
