using System.Drawing;

namespace WindowMake.Device
{
    public class TvObject : TVEqu
    {
        public TvObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.TV;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV.png";
            this.equ.EquName = "流媒体服务器";
        }
    }
}
