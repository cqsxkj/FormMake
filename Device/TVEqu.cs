/* 摄像机父类
 * 
 * 
 * */
using WindowMake.Entity;

namespace WindowMake.Device
{
    public class TVEqu : MyObject
    {
        public Tv_cctv_cfg tv_pro = new Tv_cctv_cfg();

        public TVEqu()
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
