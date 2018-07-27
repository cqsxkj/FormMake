using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class TvGunObject : TVEqu
    {
        public TvGunObject(Point p)
        {
            this.init(p);
        }

        public TvGunObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.TV_CCTV_Gun;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV_CCTV_Gun.png";
            this.equ.EquName = "枪机";
        }
    }
}
