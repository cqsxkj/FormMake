namespace WindowMake.Entity
{
    /// <summary>
    /// 遥控信息配置表
    /// </summary>
    public class Yk_cfg
    {
        public Yk_cfg()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        public int? ID { get; set; }
        public string EquID { get; set; }
        public string AddrAndBit { get; set; }
        public int? Order { get; set; }
        public int? AreaID { get; set; }
        #endregion
    }
}
