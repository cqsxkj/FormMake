using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl2CloseObject : PLCEqu
    {
        public Ptl2CloseObject(PointF p)
        {

            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TL2_Close;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL2_Close.png";
            this.equ.EquName = "车道指示器";
            equTypeName = equ.EquName;
        }
    }
}
