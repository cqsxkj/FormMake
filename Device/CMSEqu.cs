/* 情报板父类
 * 
 * 
 * */
using System;
using System.Drawing;
using System.Xml;
using WindowMake.Entity;

namespace WindowMake.Device
{
    public class CMSEqu : MyObject
    {
        public c_cfg cms_pro = new c_cfg();

        public CMSEqu()
        {
            equtype = MyObject.ObjectType.CF;
            picName = "\\Pic\\cf.png";
        }
        public virtual void WaitControl(out bool bComplete, out bool bResult)
        {
            bComplete = true;
            bResult = false;
        }
    }
}
