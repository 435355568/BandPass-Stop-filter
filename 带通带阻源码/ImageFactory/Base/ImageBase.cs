using System;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MathHelper;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Base{
    abstract partial class ImageBase{
        protected ValueMath valueMath = ValueMath.GetInstance();

        #region 内存法处理图像

        private BitmapData bmpData;
        private Bitmap sourceImage;
        private IntPtr ptr;
        // 图像总长度
        protected Int32 Length;
        // 图像长度
        protected Int32 Height;
        // 图像宽度
        protected Int32 Width;
        // 图像实际宽度(已x3)
        protected Int32 RealWidth;
        // 图像实际半宽度(已x3)
        protected Int32 HalfWidth;
        // 图像长度
        protected Int32 HalfHeight;

        protected virtual Byte[] LockBits(Bitmap srcImage, ImageLockMode mode){
            sourceImage = srcImage;

            Int32 tempWidth = sourceImage.Width;
            Int32 tempHeight = sourceImage.Height;
            Rectangle rect = new Rectangle(0, 0, tempWidth, tempHeight);
            bmpData = sourceImage.LockBits(rect, mode, sourceImage.PixelFormat);
            ptr = bmpData.Scan0;

            Int32 byteLength = bmpData.Stride * tempHeight;
            Byte[] rgbBytes = new Byte[byteLength];
            Marshal.Copy(ptr, rgbBytes, 0, byteLength);

            Length = rgbBytes.Length;
            Height = srcImage.Height;
            Width = bmpData.Stride;

            // 奇偶取中值的区别
            if (RealWidth / 3 % 2 == 0)
                HalfWidth = RealWidth / 6 * 3;
            else
                HalfWidth = ((RealWidth / 3 - 1) / 2) * 3;
            // 奇偶取中值的区别
            if (Height % 2 == 0)
                HalfHeight = Height / 2;
            else
                HalfHeight = (Height - 1) / 2;

            return rgbBytes;
        }

        protected void UnlockBits(Byte[] rgbData){
            try{
                Marshal.Copy(rgbData, 0, ptr, rgbData.Length);
                sourceImage.UnlockBits(bmpData);
                sourceImage = null;
                bmpData = null;
                ptr = IntPtr.Zero;
            }
            catch{

            }
        }

        protected void UnlockBits(){
            sourceImage.UnlockBits(bmpData);
            sourceImage = null;
            bmpData = null;
            ptr = IntPtr.Zero;
        }

        #endregion
    }
}
