using System.Drawing;

namespace WindowMake.Device
{
    public class VcObject : MyObject
    {
        public VcObject(PointF p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.VC;
            equ.EquID = (int)equtype + "0001";
            picName = "VC.png";
            equ.EquName = "车检器";
        }
    }
}
