/* 环境监测父类
 * 
 * 
 * */
using System.Drawing;

namespace WindowMake.Device
{
    public class EMEqu : MyObject
    {
        public EMEqu()
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
