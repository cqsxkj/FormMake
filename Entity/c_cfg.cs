using System;

namespace WindowMake.Entity
{
    public class c_cfg
    {
        public string EquID { get; set; }
        public Nullable<int> CMSHeight { get; set; }
        public Nullable<int> CMSWidth { get; set; }
        public string FontType { get; set; }
        public string FontColor { get; set; }
        public string BackColor { get; set; }
        public Nullable<int> FontSize { get; set; }
        public Nullable<int> ContentType { get; set; }
        public Nullable<int> CharBetween { get; set; }
        public Nullable<int> OutType { get; set; }
        public Nullable<int> OutSpeed { get; set; }
        public Nullable<int> StayTime { get; set; }
        public Nullable<int> BlankCount { get; set; }
        public string Pno { get; set; }
        public Nullable<int> MinFontSize { get; set; }
        public Nullable<int> MaxLength { get; set; }
        /// <summary>
        /// 1为支持发送图片
        /// </summary>
        public Nullable<int> SupportPic { get; set; }
        /// <summary>
        /// 图片所在文件夹名
        /// </summary>
        public string PicLocation { get; set; }
    }
}
