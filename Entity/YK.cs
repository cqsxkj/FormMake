namespace WindowMake.Entity
{
    /// <summary>
    /// YK点位配置表
    /// </summary>
    public class yk
    {
        #region Model
        private int? _areaid;
        private int? _ykid;
        private int? _commandid;
        private string _equid;
        private string _mesg;
        private string _points;

        public int? AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? YKID
        {
            set { _ykid = value; }
            get { return _ykid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CommandID
        {
            set { _commandid = value; }
            get { return _commandid; }
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
        public string Mesg
        {
            set { _mesg = value; }
            get { return _mesg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Points
        {
            set { _points = value; }
            get { return _points; }
        }
        #endregion Model
    }
}
