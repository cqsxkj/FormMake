using System.Drawing;

namespace WindowMake.Device
{
    public class services : MyObject
    {
        public services(Point p)
        {
            this.init(p);
        }

        public services()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.services;
            equ.EquID = (int)equtype+"0001";
            picName = "servicezone.png";
            equ.EquName = "服务区";
        }
    }
}
