using System.Drawing;

namespace WindowMake.Device
{
    public class bridge : MyObject
    {
        public bridge(PointF p)
        {
            this.init(p);
        }

        public bridge()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.bridge;
            equ.EquID = (int)equtype+"0001";
            picName = "bridge.png";
            equ.EquName = "桥梁";
        }
    }
}
