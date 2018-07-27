using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class TvObject : TVEqu
    {
        public TvObject(Point p)
        {
            this.init(p);
        }

        public TvObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.TV;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "TV.png";
            this.equ.EquName = "流媒体服务器";
        }
    }
}
