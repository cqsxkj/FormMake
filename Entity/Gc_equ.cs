namespace WindowMake.Entity
{
    /// <summary>
    /// 群控设备
    /// </summary>
    public class Gc_equ 
    {
        public Gc_equ()
        { }
        #region Model
        private string _equid;
        private int _commandid;
        private string _gcid;
        private string _mesg;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquID
        {
            set { _equid = value; }
            get { return _equid; }
        }
        /// <summary>
        /// 命令编号
        /// </summary>
        public int CommandID
        {
            set { _commandid = value; }
            get { return _commandid; }
        }
        /// <summary>
        /// 群控编号
        /// </summary>
        public string GCID
        {
            set { _gcid = value; }
            get { return _gcid; }
        }
        /// <summary>
        /// 备注（限速标志的速度。。）
        /// </summary>
        public string Mesg
        {
            set { _mesg = value; }
            get { return _mesg; }
        }
        #endregion Model

    }
}
