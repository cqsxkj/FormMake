/* 紧急电话父类
 * 
 * 
 * */
using WindowMake.Entity;

namespace WindowMake.Device
{
    public class EPEqu : MyObject
    {
        public ep_c_cfg ep_pro = new ep_c_cfg();

        public EPEqu()
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
