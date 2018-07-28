using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl5LeftObject : PLCEqu
    {
        public Ptl5LeftObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TL5_Left;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL_Left.png";
            this.equ.EquName = "车道指示器";
            equTypeName = equ.EquName;
        }
    }
}
