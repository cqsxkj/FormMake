using System.Drawing;

namespace WindowMake.Device
{
    public class tunnel : MyObject
    {
        public tunnel(Point p)
        {
            this.init(p);
        }

        public tunnel()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.tunnel;
            equ.EquID = (int)equtype+"0001";
            picName = "tunnel.png";
            equ.EquName = "隧道";
        }
    }
}
