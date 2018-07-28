using System.Drawing;

namespace WindowMake.Entity
{
    public class LocationUtil
    {
        public static int MapStartX
        {
            get
            {
                return 100;
            }
        }

        public static int MapStartY
        {
            get
            {
                return 100;
            }
        }
        /// <summary>
        /// Convert to map location from form location
        /// </summary>
        /// <param name="formLocation"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static PointF ConvertToMapLocation(PointF formLocation, double scale)
        {
            var iconWidth = 30;
            var iconHeight = 30;

            double x = (formLocation.X - MapStartX + iconWidth / 2) / scale;
            double y = ((formLocation.Y - MapStartY + iconHeight / 2) / scale) * (-1);

            return new PointF { X = (float)x, Y = (float)y };
        }

        /// <summary>
        /// Convert to form location from map location
        /// </summary>
        /// <param name="mapLocation"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static PointF ConvertToOutLocation(Point mapLocation, double scale)
        {
            var iconWidth = 30;
            var iconHeight = 30;


            float x = (float)(mapLocation.X * scale - iconWidth / 2) + MapStartX;
            float y = (float)(mapLocation.Y * scale * (-1) - iconHeight / 2) + MapStartY;

            return new PointF { X = x, Y = y };
        }
    }
}
