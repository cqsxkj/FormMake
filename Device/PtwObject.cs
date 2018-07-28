using System.Drawing;

namespace WindowMake.Device
{
    public class PtwObject : PLCEqu
    {
        public PtwObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_TW;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_TW.png";
            this.equ.EquName = "风速风向";
        }
    }
}
