using System.Drawing;

namespace WindowMake.Device
{
    public class TvGunObject : TVEqu
    {
        public TvGunObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.TV_CCTV_Gun;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV_CCTV_Gun.png";
            this.equ.EquName = "枪机";
        }
    }
}
