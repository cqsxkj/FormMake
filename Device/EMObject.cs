using System.Drawing;

namespace WindowMake.Device
{
    public class EMObject : EMEqu
    {
        public EMObject(PointF p)
        {
            this.init(p);
        }

        public EMObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EM;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "EM.png";
            this.equ.EquName = "环境监测";
        }
    }
}
