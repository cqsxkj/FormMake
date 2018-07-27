using System.Drawing;

namespace WindowMake.Device
{
    public class PclObject : PLCEqu
    {
        public PclObject(Point p)
        {
            this.init(p);
        }

        public PclObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            //this.end = new Point(p.X + 30, p.Y + 30);
            this.equtype = MyObject.ObjectType.P_CL;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_CL.png";
            this.equ.EquName = "车行横通标志";
        }
    }
}
