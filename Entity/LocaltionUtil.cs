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
        public static Point ConvertToMapLocation(Point formLocation, double scale)
        {
            var iconWidth = 30;
            var iconHeight = 30;

            int x = (int)((formLocation.X - MapStartX + iconWidth / 2) / scale);
            int y = (int)((formLocation.Y - MapStartY + iconHeight / 2) / scale) * (-1);

            return new Point { X = x, Y = y };
        }

        /// <summary>
        /// Convert to form location from map location
        /// </summary>
        /// <param name="mapLocation"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Point ConvertToOutLocation(Point mapLocation, double scale)
        {
            var iconWidth = 30;
            var iconHeight = 30;


            int x = (int)(mapLocation.X * scale - iconWidth / 2) + MapStartX;
            int y = (int)(mapLocation.Y * scale * (-1) - iconHeight / 2) + MapStartY;

            return new Point { X = x, Y = y };
        }
    }
}
