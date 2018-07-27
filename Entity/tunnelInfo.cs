using System;

namespace WindowMake.Entity
{
    public partial class tunnelInfo
    {
        public string BM { get; set; }
        public string Name { get; set; }
        public Nullable<int> LanesNum { get; set; }
        public Nullable<float> Length { get; set; }
        public string CenterStake { get; set; }
        public string PointX { get; set; }
        public string PointY { get; set; }
        public Nullable<short> Direction { get; set; }
        public string MapID { get; set; }
        public string Mesg { get; set; }
    }
}