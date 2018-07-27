namespace WindowMake.Entity
{
    public class Gc_triggerequ
    {
        public Gc_triggerequ()
        { }
        #region Model
        private string _equid;
        private int _alarmtypeid;
        private int _equrstateid;
        private string _gcid;
        private int _gctid;
        private int _isalarm;
        /// <summary>
        /// 是否是报警
        /// </summary>
        public int IsAlarm
        {
            set { _isalarm = value; }
            get { return _isalarm; }
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
        /// 
        /// </summary>
        public int AlarmTypeID
        {
            set { _alarmtypeid = value; }
            get { return _alarmtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int EquRStateID
        {
            set { _equrstateid = value; }
            get { return _equrstateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GCID
        {
            set { _gcid = value; }
            get { return _gcid; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public int GCTID
        {
            set { _gctid = value; }
            get { return _gctid; }
        }
        #endregion Model
    }
}
