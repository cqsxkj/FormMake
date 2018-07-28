using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PtdObject : PLCEqu
    {
        public PtdObject(PointF p)
        {

            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TD;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TD.png";
            this.equ.EquName = "横通门";
            equTypeName = equ.EquName;
        }
    }
}
