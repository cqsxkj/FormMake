using System.Drawing;

namespace WindowMake.Device
{
    public class EMCH4Object : EMEqu
    {
        public EMCH4Object(Point p)
        {
            this.init(p);
        }

        public EMCH4Object()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.EM_CH4;
            this.equ.EquID = (int)MyObject.ObjectType.EM_CH4 + "0001";
            this.picName = "EM_CH4_Normal.png";
            this.equ.EquName = "甲烷检测";
        }
    }
}
