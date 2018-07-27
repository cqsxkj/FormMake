using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class TvEObject : TVEqu
    {
        public TvEObject(Point p)
        {
            this.init(p);
        }

        public TvEObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.TV_CCTV_E;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV_CCTV_E.png";
            this.equ.EquName = "事件检测摄像机";
        }
    }
}
