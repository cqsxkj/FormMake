using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class TvBallObject : TVEqu
    {
        public TvBallObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.TV_CCTV_Ball;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV_CCTV_Ball.png";
            this.equ.EquName = "球机";
        }
    }
}
