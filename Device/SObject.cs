using System.Drawing;

namespace WindowMake.Device
{
    public class SObject : PLCEqu
    {
        public SObject(Point p)
        {
            this.init(p);
        }

        public SObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.S;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "S_Normal.png";
            this.equ.EquName = "限速标志";
        }
    }
}
