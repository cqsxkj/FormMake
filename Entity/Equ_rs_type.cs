namespace WindowMake.Entity
{
    /// <summary>
	/// 设备运行状态类型
	/// </summary>
    public class Equ_rs_type
    {
        public Equ_rs_type()
        { }
        #region Model
        private int _equstateid;
        private string _name;
        private string _note;
        private string _equtypeid;
        /// <summary>
        /// 状态编号
        /// </summary>
        public int EquStateID
        {
            set { _equstateid = value; }
            get { return _equstateid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 设备类型编号
        /// </summary>
        public string EquTypeID
        {
            set { _equtypeid = value; }
            get { return _equtypeid; }
        }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }
        #endregion Model
    }
}
