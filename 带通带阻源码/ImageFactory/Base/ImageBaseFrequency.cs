using System;
using MathHelper.Infrastructure;
using System.Collections.Generic;
using ValueImage.Infrastructure;
using System.Drawing;

namespace ValueImage.ImageFactory.Base{
    abstract partial class ImageBase{

        /// <summary>
        ///  傅里叶变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        /// <param name="result">返回的结果</param>
        protected void FFT(ref Byte[] data, Int32 width, Int32 height, Boolean inv, out Complex[] result)
        {
            Int32 length = width * height;
            Byte[] tempBytes = (Byte[])data.Clone();
            Complex[] tempComp = new Complex[length];


            for (int i = 0; i < length; i++)
            {
                if (inv)
                {
                    //坐标轴位移变化
                    if ((i / width + i % width) % 2 == 0)
                        tempComp[i] = new Complex(data[i], 0);
                    else
                        tempComp[i] = new Complex(-data[i], 0);
                }
                else
                    tempComp[i] = new Complex(data[i], 0);
            }

            // 水平方向变化
            Complex[] tempCompH = new Complex[width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempCompH[j] = tempComp[i * width + j];
                }
                tempCompH = valueMath.FFT(tempCompH, width);
                for (int j = 0; j < width; j++)
                {
                    tempComp[i * width + j] = tempCompH[j];
                }
            }

            // 垂直方向变化
            Complex[] tempCompVe = new Complex[height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempCompVe[j] = tempComp[j * width + i];
                }
                tempCompVe = valueMath.FFT(tempCompVe, height);

                for (int j = 0; j < height; j++)
                {
                    tempComp[j * width + i] = tempCompVe[j];
                }
            }
            result = tempComp;
        }

        /// <summary>
        /// 逆傅里叶变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        /// <param name="result">返回的结果</param>
        protected void IFFT(ref Complex[] data, Int32 width, Int32 height, Boolean inv, out Byte[] result){
            Int32 length = width * height;
            Complex[] tempComp = (Complex[])data.Clone();
            Complex[] tempCompH = new Complex[width];
            // 水平方向变化
            for (int i = 0; i < height; i++){
                for (int j = 0; j < width; j++){
                    tempCompH[j] = tempComp[i * width + j];
                }
                tempCompH = valueMath.IFFT(tempCompH, width);
                for (int j = 0; j < width; j++){
                    tempComp[i * width + j] = tempCompH[j];
                }
            }

            Complex[] tempCompVe = new Complex[height];
            // 垂直方向变化
            for (int i = 0; i < width; i++){
                for (int j = 0; j < height; j++){
                    tempCompVe[j] = tempComp[j * width + i];
                }
                tempCompVe = valueMath.IFFT(tempCompVe, height);
                for (int j = 0; j < height; j++){
                    tempComp[j * width + i] = tempCompVe[j];
                }
            }

            result = new Byte[length];
            Double temp = 0;
            // 赋值,保留实数部分
            for (int i = 0; i < length; i++){
                if (inv){
                    //坐标轴位移变化
                    if ((i / width + i % width) % 2 == 0)
                        temp = tempComp[i].Real;
                    else
                        temp = -tempComp[i].Real;
                }
                else
                    temp = tempComp[i].Real;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;

                result[i] = Convert.ToByte(temp);
            }
        }



        /// <summary>
        ///  直线(两点,即附近关联直线)
        /// </summary>
        protected class LinePoint{
            public Point Start { get; set; }
            public Point End { get; set; }

            private List<LinePoint> nearLines;
            public List<LinePoint> NearLines{
                get{
                    if (nearLines == null)
                        nearLines = new List<LinePoint>();
                    return nearLines;
                }
                set{
                    nearLines = value;
                }
            }

            public Boolean Sorted { 
                get;
                set; 
            }
        }
    }
}