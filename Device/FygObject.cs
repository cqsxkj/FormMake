using System.Drawing;

namespace WindowMake.Device
{
    public class FygObject : FireEqu
    {
        public FygObject(Point p)
        {
            this.init(p);
        }

        public FygObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.F_YG;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "F_YG.png";
            this.equ.EquName = "火灾烟感";
        }
    }
}
