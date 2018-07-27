using System.Drawing;

namespace WindowMake.Device
{
    public class IObject : PLCEqu
    {
        public IObject(Point p)
        {
            this.init(p);
        }

        public IObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.I;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "I.png";
            this.equ.EquName = "凝冰喷洒";
        }
    }
}
