using System;
using ValueImage.Interface;
using ValueImage.ImageFactory.Base;
using System.Drawing;
using ValueImage.Infrastructure;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ValueImage.ImageFactory.Bit24{
    sealed partial class ImageBit24 : ImageBase, IValueImage{
        private ImageBit24() {
        }
        private static ImageBit24 instance = null;
        public static ImageBit24 Instance{
            get{
                if (instance == null)
                    instance = new ImageBit24();
                return instance;
            }
        }

        protected override byte[] LockBits(Bitmap srcImage, ImageLockMode mode){
            Debug.Assert(srcImage.PixelFormat == PixelFormat.Format24bppRgb, "图片必须为24位像素,才能调用该类方法");
            if (srcImage.PixelFormat != PixelFormat.Format24bppRgb) throw new InvalidOperationException();

            RealWidth = srcImage.Width * 3;
            return base.LockBits(srcImage, mode);
        }

        /// <summary>
        ///  创建图片
        /// </summary>
        /// <param name="datab">b分量数据</param>
        /// <param name="datag">g分量数据</param>
        /// <param name="datar">r分量数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="format">图片格式</param>
        /// <param name="result">图片结果</param>
        private void createImage(ref Byte[] datab, ref Byte[] datag, ref Byte[] datar, Int32 width, Int32 height, PixelFormat format, out Bitmap result){
            result = new Bitmap(width, height, format);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = result.LockBits(rect, ImageLockMode.ReadOnly, format);
            IntPtr ptr = bmpData.Scan0;
            Int32 byteLength = bmpData.Stride * height;
            Byte[] rgbBytes = new Byte[byteLength];
            Marshal.Copy(ptr, rgbBytes, 0, byteLength);

            for (int i = 0; i < height; i++){
                for (int j = 0; j < width; j++){
                    rgbBytes[i * bmpData.Stride + j * 3] = datab[i * width + j];
                    rgbBytes[i * bmpData.Stride + j * 3 + 1] = datag[i * width + j];
                    rgbBytes[i * bmpData.Stride + j * 3 + 2] = datar[i * width + j];
                }
            }

            Marshal.Copy(rgbBytes, 0, ptr, rgbBytes.Length);
            result.UnlockBits(bmpData);
        }
    }
}
