using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl2DownObject : PLCEqu
    {
        public Ptl2DownObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TL2_Down;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL_Down.png";
            this.equ.EquName = "车行横通";
            equTypeName = equ.EquName;
        }
    }
}
