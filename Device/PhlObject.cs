using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PhlObject : PLCEqu
    {
        public PhlObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_HL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_HL.png";
            this.equ.EquName = "三显交通灯";
        }

        public PhlObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_HL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_HL.png";
            this.equ.EquName = "三显交通灯";
        }
    }
}
