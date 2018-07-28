using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PcoObject : PLCEqu
    {
        public PcoObject(PointF p)
        {
            this.init(p);
        }

        public PcoObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_CO;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "CO.png";
            this.equ.EquName = "CO";
        }
    }
}
