using System;
using System.Drawing;

namespace WindowMake.Device
{
    public class EObject : PLCEqu
    {
        public EObject(Point p)
        {
            this.init(p);
        }

        public EObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.E;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "E.png";
            this.equ.EquName = "事件检测服务器";
        }
    }
}
