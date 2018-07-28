using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PlyjObject : PLCEqu
    {
        public PlyjObject(PointF p)
        {
            this.init(p);
        }

        public PlyjObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_LYJ;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_LYJ.png";
            this.equ.EquName = "应急照明";
        }
    }
}
