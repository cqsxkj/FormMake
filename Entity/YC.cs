namespace WindowMake.Entity
{
    /// <summary>
    /// YC点位配置表
    /// </summary>
    public class yc
    {
        #region Model
        private string _ycfield;
        private float? _ycfun;
        private float? _ycrealup;
        private float? _ycrealdown;
        private float? _yccollecup;
        private float? _yccollecdown;
        private int? _ycid;
        private string _equid;
        /// <summary>
        /// 
        /// </summary>
        public string YCField
        {
            set { _ycfield = value; }
            get { return _ycfield; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float? YCFun
        {
            set { _ycfun = value; }
            get { return _ycfun; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float? YCRealUP
        {
            set { _ycrealup = value; }
            get { return _ycrealup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float? YCRealDown
        {
            set { _ycrealdown = value; }
            get { return _ycrealdown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float? YCCollecUP
        {
            set { _yccollecup = value; }
            get { return _yccollecup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float? YCCollecDown
        {
            set { _yccollecdown = value; }
            get { return _yccollecdown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? YCID
        {
            set { _ycid = value; }
            get { return _ycid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquID
        {
            set { _equid = value; }
            get { return _equid; }
        }
        #endregion Model
    }
}
