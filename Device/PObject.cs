using System.Collections.Generic;
using System.Drawing;
using WindowMake.Entity;

namespace WindowMake.Device
{
    public class PObject : MyObject
    {
        public List<p_area_cfg> area = new List<p_area_cfg>();
        public PObject(Point p)
        {
            this.init(p);
        }

        public PObject()
        {
            this.init(this.LocationInMap);
        }

        public void init(Point p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.P;
            this.equ.EquID = (int)equtype + "0001";
            this.picName = "P.png";
            this.equ.EquName = "PLC主机";
        }
    }
}
