namespace WindowMake.Entity
{
    /// <summary>
    /// 命令
    /// </summary>
    public class Command 
    {
        public Command()
        { }
        #region Model
        private string _commandid;
        private string _equtypeid;
        private string _name;
        private string _describe;
        private int? _cycletime;
        private int? _needparam;
        private int? _needresponce;
        private int? _isshow;
        private string _funname;
        /// <summary>
        /// 命令编号
        /// </summary>
        public string CommandID
        {
            set { _commandid = value; }
            get { return _commandid; }
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
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 循环时间
        /// </summary>
        public int? CycleTime
        {
            set { _cycletime = value; }
            get { return _cycletime; }
        }
        /// <summary>
        /// 是否需要参数
        /// </summary>
        public int? NeedParam
        {
            set { _needparam = value; }
            get { return _needparam; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int? IsShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunName
        {
            set { _funname = value; }
            get { return _funname; }
        }
        /// <summary>
        /// 是否需要设备返回信息 0为不需要，1为需要
        /// </summary>
        public int? NeedResponce
        {
            get { return _needresponce; }
            set { _needresponce = value; }
        }

        public int? EquStateID { get; set; }
        #endregion Model

    }
}
