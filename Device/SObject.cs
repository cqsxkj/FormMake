using System.Drawing;

namespace WindowMake.Device
{
    public class SObject : PLCEqu
    {
        public SObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.S;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "S_Normal.png";
            this.equ.EquName = "限速标志";
        }
    }
}
