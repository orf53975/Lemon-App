﻿using System;
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
            im.Background = new ImageBrush(new BitmapImage(new Uri(img)));
        }
    }
}