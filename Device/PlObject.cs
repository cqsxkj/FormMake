using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PlObject : PLCEqu
    {
        public PlObject(PointF p)
        {
            this.init(p);
        }

        public PlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_L;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_L.png";
            this.equ.EquName = "基本照明";
            equTypeName = equ.EquName;
        }
    }
}
