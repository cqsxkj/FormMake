using System.Drawing;

namespace WindowMake.Device
{
    public class toll : MyObject
    {
        public toll(Point p)
        {
            this.init(p);
        }

        public toll()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.toll;
            equ.EquID = (int)equtype+"0001";
            picName = "toll.png";
            equ.EquName = "收费站";
        }
    }
}
