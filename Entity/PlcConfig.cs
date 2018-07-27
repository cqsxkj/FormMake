using System.Collections.Generic;

namespace WindowMake.Entity
{
    public class PLCConfig
    {
        //分区
        public List<p_area_cfg> areas { get; set; }
        public List<yc> ycList { get; set; }
        public List<PlcEquConfig> config { get; set; }
    }
    public class PlcEquConfig
    {
        public string equType { get; set; }
        public List<yx> yxList { get; set; }
        public List<yk> ykList { get; set; }
    }
}
