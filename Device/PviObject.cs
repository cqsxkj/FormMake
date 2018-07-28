using System.Drawing;

namespace WindowMake.Device
{
    public class PviObject : PLCEqu
    {
        public PviObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_VI;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_VI.png";
            this.equ.EquName = "VI";
        }
    }
}
