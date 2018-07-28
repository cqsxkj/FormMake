using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PjfObject : PLCEqu
    {
        public PjfObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_JF;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_JF.png";
            this.equ.EquName = "射流风机";
            equTypeName = equ.EquName;
        }
    }
}
