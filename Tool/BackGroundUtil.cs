using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using WindowMake.Entity;

namespace WindowMake.Tool
{
    public class BackGroundUtil
    {
        /// <summary>
        /// 底图绘制
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Image CreateBackgroundImage(Image originalImage, double scale)
        {
            var resultImage = new Bitmap((int)(originalImage.Width * scale) + LocationUtil.MapStartX * 2, (int)(originalImage.Height * scale) + LocationUtil.MapStartY * 2);

            Graphics gh = Graphics.FromImage(resultImage);
            gh.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gh.Clear(Color.White);
            int destX = LocationUtil.MapStartX;
            int destY = LocationUtil.MapStartY;
            int destWidth = (int)(originalImage.Width * scale);
            int destHeight = (int)(originalImage.Height * scale);

            int sourceX = 0;
            int sourceY = 0;
            int sourceWidth = originalImage.Width;
            int sourceHeight = originalImage.Height;

            gh.DrawImage(originalImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            gh.Dispose();

            return resultImage;
        }
    }
}
