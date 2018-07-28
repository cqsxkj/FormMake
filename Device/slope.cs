using System.Drawing;

namespace WindowMake.Device
{
    public class slope : MyObject
    {
        public slope(PointF p)
        {
            this.LocationInMap = p;
            equtype = MyObject.ObjectType.slope;
            equ.EquID = (int)equtype + "0001";
            picName = "bedslope.png";
            equ.EquName = "边坡";
        }
    }
}
