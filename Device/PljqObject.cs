using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PljqObject : PLCEqu
    {
        public PljqObject(Point p)
        {
            this.init(p);
        }

        public PljqObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_LJQ;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_LJQ.png";
            this.equ.EquName = "加强照明";
            equTypeName = equ.EquName;
        }
    }
}
