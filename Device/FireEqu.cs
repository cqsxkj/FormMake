/* 情报板父类
 * 
 * 
 * */
using System.Xml;
using WindowMake.Entity;

namespace WindowMake.Device
{
    public class FireEqu : MyObject
    {
        public f_c_cfg fire_pro = new f_c_cfg();

        public FireEqu()
        {
            equtype = MyObject.ObjectType.UnKnow;
            picName = "\\Pic\\unkown.png";
        }
        
        public virtual void WaitControl(out bool bComplete, out bool bResult)
        {
            bComplete = true;
            bResult = false;
        }

    }
}
