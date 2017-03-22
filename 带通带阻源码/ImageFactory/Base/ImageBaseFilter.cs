using System;
using System.Diagnostics;
using ValueImage.Infrastructure;
using MathHelper.Infrastructure;

namespace ValueImage.ImageFactory.Base{
    abstract partial class ImageBase{
        /// <summary>
        ///  低通滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="radius">滤波圆周边界(大于该边界都不可通过)</param>
        protected void lowpassFilter(ref Byte[] data, Int32 width, Int32 height, Double radius, out Byte[] result){
            Debug.Assert(valueMath.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(width)) { 
                result = (Byte[])data.Clone(); 
                return; 
            }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            radius = radius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++){
                Int32 row = i / width;
                Int32 col = i % width;
                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);

                if (distance > radius){
                    tempComp[i] = new Complex(0.0, 0.0);
                }
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }

        /// <summary>
        ///  高通滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="radius">滤波边界(小于该边界都不可通过)</param>
        protected void highpassFilter(ref Byte[] data, Int32 width, Int32 height, Double radius, out Byte[] result) {
            Debug.Assert(valueMath.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(width)) { 
                result = (Byte[])data.Clone(); return; 
            }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            radius = radius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++){
                Int32 row = i / width;
                Int32 col = i % width;

                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);
                if (distance < radius){
                    tempComp[i] = new Complex(0.0, 0.0);
                }
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }

        #region 带阻滤波
        /// <summary>
        ///  带阻滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        protected void bandstopFilter(ref Byte[] data, Int32 width, Int32 height, Double innerRadius, Double outerRadius, out Byte[] result){
            Debug.Assert(valueMath.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(width)) { 
                result = (Byte[])data.Clone(); 
                return; 
            }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            innerRadius = innerRadius * minLen / 100;
            outerRadius = outerRadius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++){
                Int32 row = i / width;
                Int32 col = i % width;
                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);

                if (distance < outerRadius && distance > innerRadius){
                    tempComp[i] = new Complex(0.0, 0.0);
                }
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }
        #endregion

        #region 带通滤波
        /// <summary>
        ///  带通滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        /// <param name="result"></param>
        protected void bandpassFilter(ref Byte[] data, Int32 width, Int32 height, Double innerRadius, Double outerRadius, out Byte[] result){
            Debug.Assert(valueMath.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(width)) { 
                result = (Byte[])data.Clone(); return; 
            }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            innerRadius = innerRadius * minLen / 100;
            outerRadius = outerRadius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++){
                Int32 row = i / width;
                Int32 col = i % width;

                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);
                if (distance < innerRadius || distance > outerRadius)
                    tempComp[i] = new Complex(0.0, 0.0);
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }
        #endregion
    }
}