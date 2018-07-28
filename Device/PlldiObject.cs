using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PlldiObject : PLCEqu
    {
        public PlldiObject(PointF p)
        {
            this.init(p);
        }

        public PlldiObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_LLDI;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_LLDI.png";
            this.equ.EquName = "液位检测仪";
            equTypeName = equ.EquName;
        }
    }
}
