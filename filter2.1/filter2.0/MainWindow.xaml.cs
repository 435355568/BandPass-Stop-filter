using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using addNoise;
using MathHelper;
using System.Drawing;
using MathHelper.Infrastructure;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ValueImage.Infrastructure;
using ValueImage.Interface;
using ValueImage;

namespace filter2._0
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window{
        string fileName;    //文件名
        string filePath;    //文件路径
        double varGauss ,varSpeckle,varSalt;
        int flag = 0;       //通过定义一个全局变量作为标志来判断图像是否已经经过噪声添加，若未加噪声，flag=0，一旦添加噪声，flag=1.
        Boolean Bandpassflag = false;
        Boolean Bandstopflag = false;
        Boolean Gaussflag = false;
        Boolean Saltflag = false;
        Boolean Speckleflag = false;
        Boolean Poissonflag = false;
        OpenFileDialog openFileDialog = new OpenFileDialog();   //OpenFileDialog 打开文件夹（是一个类）
        double bpWCriRad, bpNCriRad, bsWCriRad, bsNCriRad;      //定义带通外圆半径，带通内圆半径，带阻外圆半径，带阻内圆半径 
        IValueImage valueImage = ValueImageManager.GetValueImage(System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        protected ValueMath valueMath = ValueMath.GetInstance();

        #region 限定图片格式
        public MainWindow(){
            InitializeComponent();
            openFileDialog.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf|" +
                "位图( *.bmp; *.jpg; *.png;...) | *.bmp; *.pcx; *.png; *.jpg; *.gif; *.tif; *.ico|" +
                "矢量图( *.wmf; *.eps; *.emf;...) | *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";//设置能够打开文件的格式
        }
        #endregion

        #region 带通按钮
        private void daitong_Click(object sender, RoutedEventArgs e){
            if (flag == 0){
                filePath = openFileDialog.FileName;
                flag = 1;
            }
            else{
                filePath = @"D:\temp.jpg";
            }
            if (Bandpassflag){
                Bitmap bmp = new Bitmap(filePath);
                ((IFilter)valueImage).BandpassFilter(bmp, bpNCriRad, bpWCriRad);
                //((IFilter)valueImage).BandpassFilter(bmp, ValueImage.Infrastructure.RateFilterRadius.BandPassInner, ValueImage.Infrastructure.RateFilterRadius.BandPassOuter);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                BitmapImage bImage = new BitmapImage();
                bImage.BeginInit();
                bImage.StreamSource = new MemoryStream(ms.ToArray());
                bImage.EndInit();
                ms.Dispose();
                bmp.Dispose();
                System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                daitong ow = new daitong(bImage, fileName, filePath);
                ow.ReFreshPic(bImage);
                ow.Show();
                Bandpassflag = false;
            }else {
                System.Windows.Forms.MessageBox.Show("请确定带通内外径参数！");
            }

        }
        #endregion

        #region 带阻按钮
        private void daizu_Click(object sender, RoutedEventArgs e){
            if (flag == 0){
                filePath = openFileDialog.FileName;
                flag = 1;
            }
            else{
                filePath = @"D:\temp.jpg";
            }
            if (Bandstopflag){
                Bitmap bmp = new Bitmap(filePath);
                ((IFilter)valueImage).BandstopFilter(bmp, bsNCriRad, bsWCriRad);
                //((IFilter)valueImage).BandstopFilter(bmp, ValueImage.Infrastructure.RateFilterRadius.BandStopInner, ValueImage.Infrastructure.RateFilterRadius.BandStopOuter);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                BitmapImage bImage = new BitmapImage();
                bImage.BeginInit();
                bImage.StreamSource = new MemoryStream(ms.ToArray());
                bImage.EndInit();
                ms.Dispose();
                bmp.Dispose();
                System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                daizu ow = new daizu(bImage, fileName, filePath);
                ow.ReFreshPic(bImage);
                ow.Show();
                Bandstopflag = false;
            }else {
                System.Windows.Forms.MessageBox.Show("请确定带阻内外径参数！");
            }
        }
        #endregion

        #region 帮助按钮
        //定义UserHelp_Click的函数
        private void UserHelp_Click(object sender, RoutedEventArgs e){
            //UserHelp在这作为一个类。这里写的是弹出帮助窗口
            UserHelp ow = new UserHelp();
            ow.Show();
        }
        #endregion

        #region 添加图片
        private void addImage_Click(object sender, RoutedEventArgs e){
            if(openFileDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK){
                fileName = openFileDialog.FileName;//若点击打开，得到图片文件名
            }
            BitmapImage image = new BitmapImage(new Uri(fileName));
            image1.Source = image;//将原图显示在Image控件上
            image2.Source = image;
            flag = 0;
            Bandpassflag = false;
            Bandstopflag = false;
            Boolean Gaussflag = false;
            Boolean Saltflag = false;
            Boolean Speckleflag = false;
            Boolean Poissonflag = false;
        }
        #endregion

        public ImageSourceConverter imageSourceConverter
        {
            get;
            set;
        }

        #region 退出按钮功能
        //定义Exit_Click函数，即退出程序的函数
        private void Exit_Click(object sender, RoutedEventArgs e){
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        #region 定量改变带通参数
        #region 内圆半径
        /*定义tb_dtNCriRad_dl_Down_Click，即带通内圆半径减小的函数*/
        private void tb_dtNCriRad_dl_Down_Click(object sender, RoutedEventArgs e){
            double cs;
            double.TryParse(tb_dtNCriRad_dl.Text, out cs);  //判断数据是否可传给cs
            cs -= 1;                                        //修改参数
            tb_dtNCriRad_dl.Text = Convert.ToString(cs);    //将cs转换为string，更新
        }

        //定义tb_dtNCriRad_dl_Up_Click，即带通内圆半径增大的函数
        private void tb_dtNCriRad_dl_Up_Click(object sender, RoutedEventArgs e){
            double cs;
            double.TryParse(tb_dtNCriRad_dl.Text, out cs);//判断数据是否可传给cs
            cs += 1;//修改参数
            tb_dtNCriRad_dl.Text = Convert.ToString(cs);//将cs转换为string，更新
        }
        #endregion

        #region 外圆半径
        //定义tb_dtWCriRad_dl_Up_Click，即带通外圆半径增加的函数
        private void tb_dtWCriRad_dl_Up_Click(object sender, RoutedEventArgs e)
        {
            double cs;
            double.TryParse(tb_dtWCriRad_dl.Text, out cs);//判断数据是否可传给cs
            cs += 1;//参数修改
            tb_dtWCriRad_dl.Text = Convert.ToString(cs);//将cs转换为string，更新
        }

        //定义tb_dtWCriRad_dl_Down_Click，即带通外圆半径减小的函数
        private void tb_dtWCriRad_dl_Down_Click(object sender, RoutedEventArgs e)
        {
            double cs;
            double.TryParse(tb_dtWCriRad_dl.Text, out cs);//判断数据是否可传给cs
            cs -= 1;//修改参数
            tb_dtWCriRad_dl.Text = Convert.ToString(cs); //将cs转换为string，更新
        }
        #endregion
        private void BandPassDataInsure_dl_Click(object sender, RoutedEventArgs e){
            double OutSide   = Convert.ToDouble(tb_dtWCriRad_dl.Text);
            double InSide    = Convert.ToDouble(tb_dtNCriRad_dl.Text);
            if (OutSide > InSide){
                bpWCriRad = OutSide;
                bpNCriRad = InSide;
                Bandpassflag = true;
            }
            else {
                System.Windows.Forms.MessageBox.Show("外圆半径不可小于内圆半径！"); 
            }
        }
        #endregion

        #region 定量改变带阻参数
        //定义tb_dzWCriRad_dl_Down_Click，即带阻内圆半径减小的函数
        private void tb_dzWCriRad_dl_Down_Click(object sender, RoutedEventArgs e){
            double cs;
            double.TryParse(tb_dzWCriRad_dl.Text, out cs);  //判断数据是否可传给cs
            cs -= 1;                                        //修改参数
            tb_dzWCriRad_dl.Text = Convert.ToString(cs);    //将cs转换为string，更新
        }
        //定义tb_dzWCriRad_dl_Up_Click，即带阻内圆半径增大的函数
        private void tb_dzWCriRad_dl_Up_Click(object sender, RoutedEventArgs e)
        {
            double cs;
            double.TryParse(tb_dzWCriRad_dl.Text, out cs);//判断数据是否可传给cs
            cs += 0.1;//修改参数
           tb_dzWCriRad_dl.Text = Convert.ToString(cs); //将cs转换为string，更新
        }
        //定义tb_dzNCriRad_dl_Up_Click，即带阻内圆半径减小的函数
        private void tb_dzNCriRad_dl_Down_Click(object sender, RoutedEventArgs e)
        {
            double cs;
            double.TryParse(tb_dzNCriRad_dl.Text, out cs);//判断数据是否可传给cs
            cs -= 1;//修改参数
            tb_dzNCriRad_dl.Text = Convert.ToString(cs);//将cs转换为string，更新
        }
        //定义tb_dzNCriRad_dl_Up_Click，即带阻内圆半径增大的函数
        private void tb_dzNCriRad_dl_Up_Click(object sender, RoutedEventArgs e)
        {
            double cs;
            double.TryParse(tb_dzNCriRad_dl.Text, out cs);//判断数据是否可传给cs
            cs += 1;//修改参数
            tb_dzNCriRad_dl.Text = Convert.ToString(cs);//将cs转换为string，更新
        }
        //定量改变带带阻参数的确认
        private void BandStopDataInsure_Click(object sender, RoutedEventArgs e){
            double OutSide = Convert.ToDouble(tb_dzWCriRad_dl.Text);
            double InSide = Convert.ToDouble(tb_dzNCriRad_dl.Text);
            if (OutSide > InSide){
            bsWCriRad = Convert.ToDouble(tb_dzWCriRad_dl.Text);
            bsNCriRad = Convert.ToDouble(tb_dzNCriRad_dl.Text);
            Bandstopflag = true;
            }
           
        }
        #endregion
        private void BandStopDataInsure_dl_Click(object sender, RoutedEventArgs e){
            double OutSide = Convert.ToDouble(tb_dzWCriRad_dl.Text);
            double InSide = Convert.ToDouble(tb_dzNCriRad_dl.Text);
            if (OutSide > InSide){
            bsWCriRad = Convert.ToDouble(tb_dzWCriRad_dl.Text);
            bsNCriRad = Convert.ToDouble(tb_dzNCriRad_dl.Text);
            Bandstopflag = true;
            }
        }
        //#region 带阻自主改变参数
        //private void BandStopDataInsure_zz_Click(object sender, RoutedEventArgs e){
        //    double OutSide = Convert.ToDouble(dzWCriRad_zz.Text);
        //    double InSide = Convert.ToDouble(dzNCriRad_zz.Text);
        //    if (OutSide > InSide)
        //    {
        //        bsWCriRad = Convert.ToDouble(dzWCriRad_zz.Text);
        //        bsNCriRad = Convert.ToDouble(dzNCriRad_zz.Text);
        //        Bandstopflag = true;
        //    }
        //}
        //#endregion

        //#region 带通自主改变参数
        //private void BandPassDataInsure_zz_Click(object sender, RoutedEventArgs e)
        //{
        //    double OutSide = Convert.ToDouble(dtWCriRad_zz.Text);
        //    double InSide = Convert.ToDouble(dtNCriRad_zz.Text);
        //    if (OutSide > InSide)
        //    {
        //        bsWCriRad = Convert.ToDouble(dtWCriRad_zz.Text);
        //        bsNCriRad = Convert.ToDouble(dtNCriRad_zz.Text);
        //        Bandstopflag = true;
        //    }
        //}
        //#endregion

        #region 添加高斯、椒盐、乘性及泊松等噪声按钮
        private void btnAddNoiseGauss_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0)
            {
                fileName = openFileDialog.FileName;
                flag = 1;
            }
            else
                fileName = @"D:\temp.jpg";
           // double var_gau;
            if (Gaussflag = true)
            {
                addNoise.addNoiseClass st = new addNoise.addNoiseClass();
                st.addNoise(fileName, "gaussian", varGauss);
                FileStream Pic = new FileStream(@"D:\temp.jpg", FileMode.Open);
                byte[] PicByte = new byte[Pic.Length];
                Pic.Read(PicByte, 0, PicByte.Length);
                Pic.Close();
                MemoryStream stream = new MemoryStream(PicByte);
                imageSourceConverter = new ImageSourceConverter();
                image2.Source = imageSourceConverter.ConvertFrom(stream) as BitmapFrame;
            }
            else {
                System.Windows.Forms.MessageBox.Show("请确认需要添加的高斯噪声的参数！");
            }
        }
        
        private void sddNoiseSalt_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0)
            {
                fileName = openFileDialog.FileName;
                flag = 1;
            }
            else
                fileName = @"D:\temp.jpg";
            if (Saltflag == true)
            {
                addNoise.addNoiseClass st = new addNoise.addNoiseClass();
                st.addNoise(fileName, "salt&pepper",varSalt);
                FileStream Pic = new FileStream(@"D:\temp.jpg", FileMode.Open);
                byte[] PicByte = new byte[Pic.Length];
                Pic.Read(PicByte, 0, PicByte.Length);
                Pic.Close();
                MemoryStream stream = new MemoryStream(PicByte);
                imageSourceConverter = new ImageSourceConverter();
                image2.Source = imageSourceConverter.ConvertFrom(stream) as BitmapFrame;
            }
            else {
                System.Windows.Forms.MessageBox.Show("请确定需要添加的椒盐噪声参数！");
            }
            
        }

        private void btnSpeckle_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0)
            {
                fileName = openFileDialog.FileName;
                flag = 1;
            }
            else
                fileName = @"D:\temp.jpg";
            if (Speckleflag == true)
            {
                addNoise.addNoiseClass st = new addNoise.addNoiseClass();
                st.addNoise(fileName, "speckle",varSpeckle);
                FileStream Pic = new FileStream(@"D:\temp.jpg", FileMode.Open);
                byte[] PicByte = new byte[Pic.Length];
                Pic.Read(PicByte, 0, PicByte.Length);
                Pic.Close();
                MemoryStream stream = new MemoryStream(PicByte);
                imageSourceConverter = new ImageSourceConverter();
                image2.Source = imageSourceConverter.ConvertFrom(stream) as BitmapFrame;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("请确定需要添加乘性噪声参数！");
            }
        }

        private void btnPoisson_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0)
            {
                fileName = openFileDialog.FileName;
                flag = 1;
            }
            else
                fileName = @"D:\\temp.jpg";
            if (Poissonflag == true)
            {
                addNoise.addNoiseClass st = new addNoise.addNoiseClass();
                st.addNoise(fileName, "poisson");
                FileStream Pic = new FileStream(@"D:\\temp.jpg", FileMode.Open);
                byte[] PicByte = new byte[Pic.Length];
                Pic.Read(PicByte, 0, PicByte.Length);
                Pic.Close();
                MemoryStream stream = new MemoryStream(PicByte);
                imageSourceConverter = new ImageSourceConverter();
                image2.Source = imageSourceConverter.ConvertFrom(stream) as BitmapFrame;
            }
            else {
                System.Windows.Forms.MessageBox.Show("请确定添加泊松噪声！");
            }
        }
        #endregion

        #region 重置图像
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            fileName = openFileDialog.FileName;
            BitmapImage image = new BitmapImage(new Uri(fileName));
            image1.Source = image;
            image2.Source = image;
            flag = 0;
            Bandpassflag = false;
            Bandstopflag = false;
            Boolean Gaussflag = false;
            Boolean Saltflag = false;
            Boolean Speckleflag = false;
            Boolean Poissonflag = false;
        }
        #endregion

        #region 改变各种噪声参数
        private void GaussInsure_Click(object sender, RoutedEventArgs e)
        {
            varGauss = Convert.ToDouble(var_gau.Text);
            Gaussflag = true;
        }

        private void SpeckleInsure_Click(object sender, RoutedEventArgs e)
        {
            varSpeckle = Convert.ToDouble(var_speckle .Text);
            Speckleflag = true;
        }

        private void PoissonInsure_Click(object sender, RoutedEventArgs e)
        {
            Poissonflag = true;
        }

        private void SaltInsure_Click(object sender, RoutedEventArgs e)
        {
            varSalt = Convert.ToDouble(var_salt.Text);
            Saltflag = true;
        }
        #endregion

    }
}
       