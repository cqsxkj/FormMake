using System.Drawing;

namespace WindowMake.Device
{
    public class FygObject : FireEqu
    {
        public FygObject(PointF p)
        {
            this.init(p);
        }

        public FygObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.F_YG;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F_YG.png";
            this.equ.EquName = "火灾烟感";
        }
    }
}
