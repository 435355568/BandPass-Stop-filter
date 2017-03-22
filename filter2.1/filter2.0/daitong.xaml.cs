using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace filter2._0
{
    /// <summary>
    /// daitong.xaml 的交互逻辑
    /// </summary>
    public partial class daitong : Window
    {
        private BitmapImage curBitmap;
        private String curFileName, curFilePath;
        public daitong()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 重写构造函数
        /// </summary>
        /// <param name="bmp">传入图像</param>
        /// <param name="name">传入文件名</param>
        /// <param name="path">传入路径</param>
        public daitong(BitmapImage bmp, string name, string path)
        {
            curFileName = name;
            curFilePath = path;
            curBitmap = bmp;
            InitializeComponent();
        }

        public void ReFreshPic(BitmapImage BP_Bimg)
        {
            dt_Image.Source = BP_Bimg;
        }

        #region Save
        //定义dt_Save_Click保存图片的函数
        private void dt_Save_Click(object sender, RoutedEventArgs e)
        {
            //判断当前文件是否为空
             if (curBitmap == null){
                System.Windows.Forms.MessageBox.Show("当前图片为空，请确认有图片后，再保存"); 
                return;
            }
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.Filter =
                "JPEG文件 (*.jpg) | *.jpg|" +
                "Gif文件 (*.gif) | *.gif|" +
                "BMP文件 (*.bmp) | *.bmp|" +
                "PNG文件 (*.png) | *.png";
            saveDlg.FilterIndex = 1;
            saveDlg.RestoreDirectory = true;
            //DialogResult返回值的强制转换，是否确认保存
            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(curBitmap));
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
        }
        #endregion
    }
}

