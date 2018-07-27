/* 功能：摄像机信息实体
 * 时间：2017-8-24 11:01:41
 * 作者：张伟
 * 
 * 描述：
 * 
 * */

namespace WindowMake.Entity
{
    public class 摄像机信息表
    {
        public int 隧道号
        { get; set; }

        public int 对象号
        { get; set; }

        public string 对象名
        {
            get; set;
        }

        public string 海康摄像机名 { get; set; }

        public string INDEX_CODE { get; set; }
    }
}
