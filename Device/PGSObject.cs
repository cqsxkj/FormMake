using System.Drawing;

namespace WindowMake.Device
{
    public class PGSObject : PLCEqu
    {
        public PGSObject(PointF p)
        {
            this.init(p);
        }

        public PGSObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_GS;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "GS_Normal.png";
            this.equ.EquName = "瓦斯检测";
        }
    }
}
