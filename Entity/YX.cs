namespace WindowMake.Entity
{
    /// <summary>
    /// YX点位配置表
    /// </summary>
    public class yx 
    {
        #region Model
        private int? _yxinfoid;
        private string _equid;
        private string _yxinfomesg;
        private int? _equstateid;
        private int? _isstate;
        /// <summary>
        /// 
        /// </summary>
        public int? YXInfoID
        {
            set { _yxinfoid = value; }
            get { return _yxinfoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquID
        {
            set { _equid = value; }
            get { return _equid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YXInfoMesg
        {
            set { _yxinfomesg = value; }
            get { return _yxinfomesg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EquStateID
        {
            set { _equstateid = value; }
            get { return _equstateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsState
        {
            set { _isstate = value; }
            get { return _isstate; }
        }
        #endregion Model
    }
}
