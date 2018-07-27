using System.Drawing;

namespace WindowMake.Device
{
    public class PEPMObject : PLCEqu
    {
        public PEPMObject(Point p)
        {
            this.init(p);
        }

        public PEPMObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P_EPM;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P_EPM_Normal.png";
            this.equ.EquName = "电力监控";
        }
    }
}
