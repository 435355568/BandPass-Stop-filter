using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface{
    /// <summary>
    ///  图形过滤接口
    /// </summary>
    public interface IFilter{

        /// <summary>
        ///  低通滤波
        /// </summary>
        /// <param name="radius">滤波边界(大于该边界都不可通过)</param>
        void LowpassFilter(Bitmap srcImage, Double radius);

        /// <summary>
        ///  高通滤波
        /// </summary>
        /// <param name="radius">滤波边界(小于该边界都不可通过)</param>
        void HighpassFilter(Bitmap srcImage, Double radius);

        /// <summary>
        ///  带阻滤波
        /// </summary>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        void BandstopFilter(Bitmap srcImage, Double innerRadius, Double outerRadius);

        /// <summary>
        ///  带通滤波
        /// </summary>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        void BandpassFilter(Bitmap srcImage, Double innerRadius, Double outerRadius);
    }
}
