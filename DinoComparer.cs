using System.Collections.Generic;
using WindowMake.Device;

namespace WindowMake
{
    public class DinoComparer : IComparer<MyObject>
    {
        public int Compare(MyObject x, MyObject y)
        {
            return x.equ.EquID.CompareTo(y.equ.EquID);
        }
    }
}
