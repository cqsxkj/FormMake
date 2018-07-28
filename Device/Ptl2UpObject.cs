using System.Drawing;

namespace WindowMake.Device
{
    public class Ptl2UpObject : PLCEqu
    {
        public Ptl2UpObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TL2_UP;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TL_UP.png";
            this.equ.EquName = "车行横通";
            equTypeName = equ.EquName;
        } 
    }
}
