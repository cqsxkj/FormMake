using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PrlObject : PLCEqu
    {
        public PrlObject(Point p)
        {
            this.init(p);
        }

        public PrlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_RL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_RL.png";
            this.equ.EquName = "路灯";
            equTypeName = equ.EquName;
        }
    }
}
