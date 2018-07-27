namespace WindowMake.Entity
{
    /// <summary>
    /// 遥信信息配置表
    /// </summary>
    public class Yx_cfg
    {
        public Yx_cfg()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        public int? ID { get; set; }
        public string EquID { get; set; }
        public string AddrAndBit { get; set; }
        public int? IsError { get; set; }
        public int? Order { get; set; }
        public int? AreaID { get; set; }

        #endregion
    }
}
