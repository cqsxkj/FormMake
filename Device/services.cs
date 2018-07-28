using System.Drawing;

namespace WindowMake.Device
{
    public class services : MyObject
    {
        public services(PointF p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.services;
            equ.EquID = (int)equtype + "0001";
            picName = "servicezone.png";
            equ.EquName = "服务区";
        }
    }
}
