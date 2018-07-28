using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl5RightObject : PLCEqu
    {
        public Ptl5RightObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TL5_Right;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL_Right.png";
            this.equ.EquName = "车道指示器";
            equTypeName = equ.EquName;
        }
    }
}
