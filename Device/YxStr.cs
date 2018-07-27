namespace WindowMake.Device
{
    public class YxStr
    {
        //public YxStr()
        //{
        //    _yxInfoId = -1;
        //    _stateId = -1;
        //    _yxMesg = "";
        //}
        private int _yxInfoId;
        private int _stateId;
        private string _yxMesg;
        public string YxName { get; set; }
        public int YxInfoID { get { return _yxInfoId; } set { _yxInfoId = value; } }
        public int StateID { get { return _stateId; } set { _stateId = value; } }
        public string YxMesg { get { return _yxMesg; } set { _yxMesg = value; } }
        public ushort isState { get; set; }
    }
}