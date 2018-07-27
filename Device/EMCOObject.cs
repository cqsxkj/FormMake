using System.Drawing;

namespace WindowMake.Device
{
    public class EMCOObject : EMEqu
    {
        public EMCOObject(Point p)
        {
            this.init(p);
        }

        public EMCOObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EM_CO;
            this.equ.EquID = (int)MyObject.ObjectType.EM_CO + "0001";
            this.picName = "EM_CO_Normal.png";
            this.equ.EquName = "一氧化碳检测";
        }
    }
}
