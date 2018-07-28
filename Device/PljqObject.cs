using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class PljqObject : PLCEqu
    {
        public PljqObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_LJQ;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_LJQ.png";
            this.equ.EquName = "加强照明";
            equTypeName = equ.EquName;
        }
    }
}
