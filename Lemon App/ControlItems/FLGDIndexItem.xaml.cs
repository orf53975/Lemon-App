﻿using LemonLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// FLGDIndexItem.xaml 的交互逻辑
    /// </summary>
    public partial class FLGDIndexItem : UserControl
    {
        public string id { get; set; }
        public string sname { get; set; }
        public string img { get; set; }

        public FLGDIndexItem(string Id,string nae,string pic)
        {
            InitializeComponent();
            id = Id;
            sname = nae;
            img = pic;
            name.Text = nae;
            if (!File.Exists(InfoHelper.GetPath() + "Cache/FLGD" + id + ".jpg"))
            {
                WebClient v = new WebClient();
                v.DownloadFileAsync(new Uri(img), InfoHelper.GetPath() + "Cache/FLGD" + id + ".jpg");
                v.DownloadFileCompleted += delegate
                {
                    v.Dispose();
                    im.Background = new ImageBrush(new BitmapImage(new Uri(InfoHelper.GetPath() + "Cache/FLGD" + id + ".jpg", UriKind.Relative)));
                    var dt = new System.Drawing.Bitmap(InfoHelper.GetPath() + "Cache/FLGD" + id + ".jpg").GetMajorColor();
                    var color = Color.FromArgb(dt.A, dt.R, dt.G, dt.B);
                    back.Background = new SolidColorBrush(color);
                };
            }
            else
            {
                im.Background = new ImageBrush(new BitmapImage(new Uri(InfoHelper.GetPath() + "Cache/FLGD" + id + ".jpg", UriKind.Relative)));
                var dt = new System.Drawing.Bitmap(InfoHelper.GetPath() + "Cache/FLGD" + id + ".jpg").GetMajorColor();
                var color = Color.FromArgb(dt.A, dt.R, dt.G, dt.B);
                back.Background = new SolidColorBrush(color);
            }
        }
    }
}
