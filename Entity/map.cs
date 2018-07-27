using System;

namespace WindowMake.Entity
{
    public partial class Map
    {
        public string MapID { get; set; }
        public string MapName { get; set; }
        public Nullable<int> IsRoad { get; set; }
        public string MapAddress { get; set; }
    }
}
