using System.Drawing;

namespace WindowMake.Device
{
    public class VcObject : MyObject
    {
        public VcObject(Point p)
        {
            this.init(p);
        }

        public VcObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.VC;
            equ.EquID = (int)equtype+"0001";
            picName = "VC.png";
            equ.EquName = "车检器";
        }
    }
}
