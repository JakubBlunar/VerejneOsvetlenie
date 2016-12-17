﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using VerejneOsvetlenieData.Data;
using Image = System.Windows.Controls.Image;

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for StlpFormular.xaml
    /// </summary>
    public partial class StlpFormular : UserControl
    {
        public SStlp Model => DataContext as SStlp;
        public SStlpCely _aktualnyStlp { get; private set; }
        public StlpFormular()
        {
            InitializeComponent();
            this.DataContextChanged += StlpFormular_DataContextChanged;
        }

        private void StlpFormular_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model == null)
            {
                _aktualnyStlp = null;
                return;
            }
            _aktualnyStlp = new SStlpCely(Model);
            _aktualnyStlp.SelectPodlaId(null);

            Stlp.DataContext = _aktualnyStlp.SStlp;
            Udaje.Children.Clear();
            Obrazky.Children.Clear();
            foreach (var doplnok in _aktualnyStlp.Doplnky)
            {
                Udaje.Children.Add(new FormularGenerator() { DataContext = doplnok, Margin = new Thickness(0, 5, 0, 5) });
            }

            NovyObrazok.Upravit.Visibility = Visibility.Collapsed;
            foreach (var sInfo in _aktualnyStlp.SInformacie)
            {
                //var obrazok = new Image();
                //obrazok.Source = GetImageStream(new MemoryStream(sInfo.Data));
                //Obrazky.Children.Add(obrazok);
                Obrazky.Children.Add(new InfoStlpu()
                {
                    Update = true,
                    DataContext = sInfo
                });
            }
            //Obrazky.ItemsSource = _aktualnyStlp.SInformacie;
            //Doplnky.ItemsSource = _aktualnyStlp.Doplnky;


        }

        public static BitmapImage GetImageStream(MemoryStream paStream)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = paStream;
            bi.EndInit();
            return bi;
        }

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);
    }
}
