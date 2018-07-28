using System.Drawing;

namespace WindowMake.Device
{
    public class HLObject : PLCEqu
    {
        public HLObject(PointF p)
        {
            this.LocationInMap = p;
            this.equtype = MyObject.ObjectType.HL;
            this.equ.EquID = (int)MyObject.ObjectType.HL + "0001";
            this.picName = "P_HL2.png";
            this.equ.EquName = "洞口交通灯4控（直控）";
        }
    }
}
