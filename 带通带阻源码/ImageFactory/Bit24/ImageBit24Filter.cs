using System;
using ValueImage.Interface;
using ValueImage.Infrastructure;
using System.Diagnostics;

namespace ValueImage.ImageFactory.Bit24{
    sealed partial class ImageBit24 : IFilter{

        #region     低通
        public void LowpassFilter(System.Drawing.Bitmap srcImage, double radius){
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { 
                base.UnlockBits();
                return; 
            }

            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            Byte[] resub, resug, resur;
            base.lowpassFilter(ref tempb, singleWidth, Height, radius, out resub);
            base.lowpassFilter(ref tempg, singleWidth, Height, radius, out resug);
            base.lowpassFilter(ref tempr, singleWidth, Height, radius, out resur);

            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }
        #endregion

        #region 高通
        public void HighpassFilter(System.Drawing.Bitmap srcImage, double radius){
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { 
                base.UnlockBits(); return; 
            }

            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            Byte[] resub, resug, resur;
            base.highpassFilter(ref tempb, singleWidth, Height, radius, out resub);
            base.highpassFilter(ref tempg, singleWidth, Height, radius, out resug);
            base.highpassFilter(ref tempr, singleWidth, Height, radius, out resur);

            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }
        #endregion

        #region 带阻
        public void BandstopFilter(System.Drawing.Bitmap srcImage, double innerRadius, double outerRadius){
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            Byte[] resub, resug, resur;
            base.bandstopFilter(ref tempb, singleWidth, Height, innerRadius, outerRadius, out resub);
            base.bandstopFilter(ref tempg, singleWidth, Height, innerRadius, outerRadius, out resug);
            base.bandstopFilter(ref tempr, singleWidth, Height, innerRadius, outerRadius, out resur);

            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }
        #endregion

        #region 带通
        public void BandpassFilter(System.Drawing.Bitmap srcImage, double innerRadius, double outerRadius){
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { 
                base.UnlockBits(); 
                return; 
            }

            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            Byte[] resub, resug, resur;
            base.bandpassFilter(ref tempb, singleWidth, Height, innerRadius, outerRadius, out resub);
            base.bandpassFilter(ref tempg, singleWidth, Height, innerRadius, outerRadius, out resug);
            base.bandpassFilter(ref tempr, singleWidth, Height, innerRadius, outerRadius, out resur);

            for (int i = 0; i < Height; i++){
                for (int j = 0; j < singleWidth; j++){
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }
        #endregion
    }
}
