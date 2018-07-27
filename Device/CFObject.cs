using System.Drawing;

namespace WindowMake.Device
{
    public class CFObject : CMSEqu
    {
        public CFObject(Point p)
        {
            this.init(p);
        }

        public CFObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.CF;
            this.equ.EquID = (int)MyObject.ObjectType.CF + "0001";
            this.picName = "cf.png";
            this.equ.EquName = "F型情报板";
        }
    }
}
