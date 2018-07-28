using System.Drawing;

namespace WindowMake.Device
{
    public class TvEObject : TVEqu
    {
        public TvEObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.TV_CCTV_E;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV_CCTV_E.png";
            this.equ.EquName = "事件检测摄像机";
        }
    }
}
