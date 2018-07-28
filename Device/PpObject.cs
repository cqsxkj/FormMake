using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PpObject : PLCEqu
    {
        public PpObject(PointF p)
        {
            this.init(p);
        }

        public PpObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_P;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_P.png";
            this.equ.EquName = "水泵";
            equTypeName = equ.EquName;
        }
    }
}
