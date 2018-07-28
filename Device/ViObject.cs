using System.Drawing;

namespace WindowMake.Device
{
    public class ViObject : MyObject
    {
        public ViObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.VI;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "VI.png";
            this.equ.EquName = "气象仪";
        }
    }
}
