namespace WindowMake.Entity
{
    /// <summary>
    /// 群控
    /// </summary>
    public class Gc
    {
        public Gc()
        { }
        #region Model
        private long? _runtime;
        private string _gcid;
        private string _name;
        private long? _addtime;
        private long? _edittime;
        private int? _cycle;
        private int? _schemetype;
        private string _equid;
        private string _planid;
        private int? _isenable;
        private int? _isimplemented;
        private string _remark;
        private int? _isOr;
        private int? _isConfirm;
        private int? _mapId;
        public int? MapId { get { return _mapId; } set { _mapId = value; } }
        /// <summary>
        /// 是否用户确认
        /// </summary>
        public int? IsConfirm
        {
            get { return _isConfirm; }
            set { _isConfirm = value; }
        }
        /// <summary>
        /// 运行时间
        /// </summary>
        public long? RunTime
        {
            set { _runtime = value; }
            get { return _runtime; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string GCID
        {
            set { _gcid = value; }
            get { return _gcid; }
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
        /// 添加时间
        /// </summary>
        public long? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long? EditTime
        {
            set { _edittime = value; }
            get { return _edittime; }
        }
        /// <summary>
        /// 周期
        /// </summary>
        public int? Cycle
        {
            set { _cycle = value; }
            get { return _cycle; }
        }
        /// <summary>
        /// 方案类型： 0/定时任务属于： 1/报警或设备状态
        /// </summary>
        public int? SchemeType
        {
            set { _schemetype = value; }
            get { return _schemetype; }
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquID
        {
            set { _equid = value; }
            get { return _equid; }
        }
        /// <summary>
        /// 预案编号
        /// </summary>
        public string PlanID
        {
            set { _planid = value; }
            get { return _planid; }
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        public int? IsEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }
        /// <summary>
        /// 是否已经执行 0/1
        /// </summary>
        public int? IsImplemented
        {
            set { _isimplemented = value; }
            get { return _isimplemented; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        public int? IsOr
        {
            set { _isOr = value; }
            get { return _isOr; }
        }
        #endregion Model

    }
}
