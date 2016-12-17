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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VerejneOsvetlenieData.Data;

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for NovyStlp.xaml
    /// </summary>
    public partial class Stlp : UserControl
    {
        public SStlp Model => DataContext as SStlp;
        public bool Update { get; set; }

        public Stlp()
        {
            InitializeComponent();
            this.DataContextChanged += Stlp_DataContextChanged;
            Update = false;
        }

        private void Stlp_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model == null)
                return;

            Upravit.Visibility = Update ? Visibility.Visible : Visibility.Collapsed;
            Vlozit.Visibility = !Update ? Visibility.Visible : Visibility.Collapsed;
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
