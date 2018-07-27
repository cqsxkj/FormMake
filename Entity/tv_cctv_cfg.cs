namespace WindowMake.Entity
{
    public class Tv_cctv_cfg
    {
        /// <summary>
        /// 摄像机编号
        /// </summary>
        public string CCTVID { get; set; }
        /// <summary>
        /// 外部地址 index_code
        /// </summary>
        public string OutsideAddr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 事件检测编号
        /// </summary>
        public string EventCheckID { get; set; }
        /// <summary>
        /// 关联事件检测摄像机编号
        /// </summary>
        public string EventLinkCameraID { get; set; }
    }
}
