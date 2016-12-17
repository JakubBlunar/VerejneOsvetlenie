using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using VerejneOsvetlenieData.Data;
using Image = System.Windows.Controls.Image;

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for InfoStlpu.xaml
    /// </summary>
    public partial class InfoStlpu : UserControl
    {
        public SInfo Model => DataContext as SInfo;
        public bool Update { get; set; }
        public InfoStlpu()
        {
            InitializeComponent();
            this.DataContextChanged += InfoStlpu_DataContextChanged;
            Update = false;
        }

        private void InfoStlpu_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model == null)
                return;

            Upravit.Visibility = Update ? Visibility.Visible : Visibility.Collapsed;
            Vlozit.Visibility = !Update ? Visibility.Visible : Visibility.Collapsed;

            Obrazok.Source = GetImageStream(new MemoryStream(Model.Data));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg"
            };


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();
            if (result != true)
                return;
            var stream = dlg.OpenFile();
            using (stream)
            {
                var img = new Bitmap(stream);
                Model.Data = Model.ImageToByteArray(img);
                Obrazok.Source = GetImageStream(new MemoryStream(Model.Data));
            }

        }

        public static BitmapImage GetImageStream(MemoryStream paStream)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = paStream;
            bi.EndInit();
            return bi;
        }

        private void Upravit_Click(object sender, RoutedEventArgs e)
        {
            Model.Update();
        }

        private void Vlozit_Click(object sender, RoutedEventArgs e)
        {
            Model.Insert();
            DataContext = null;
            DataContext = new SInfo();
        }
    }
}
