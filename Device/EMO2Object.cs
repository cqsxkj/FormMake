using System.Drawing;

namespace WindowMake.Device
{
    public class EMO2Object : EMEqu
    {
        public EMO2Object(Point p)
        {
            this.init(p);
        }

        public EMO2Object()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EM_O2;
            this.equ.EquID = (int)MyObject.ObjectType.EM_O2 + "0001";
            this.picName = "EM_O2_Normal.png";
            this.equ.EquName = "氧气检测";
        }
    }
}
