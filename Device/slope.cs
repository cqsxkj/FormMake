using System.Drawing;

namespace WindowMake.Device
{
    public class slope : MyObject
    {
        public slope(Point p)
        {
            this.init(p);
        }

        public slope()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.slope;
            equ.EquID = (int)equtype+"0001";
            picName = "bedslope.png";
            equ.EquName = "边坡";
        }
    }
}
