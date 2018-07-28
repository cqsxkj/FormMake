using System;

namespace WindowMake
{
    public class SelectEventArgs : EventArgs
    {
        private bool bMultiSelect;
        private bool bMultiCopy;

        public SelectEventArgs(bool select, bool copy)
        {
            bMultiSelect = select;
            bMultiCopy = copy;
        }
        public bool bSelect
        {
            get { return bMultiSelect; }
            set { bMultiSelect = value; }
        }
        public bool bCopy
        {
            get { return bMultiCopy; }
            set { bMultiCopy = value; }
        }
    }
}
