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
    /// Interaction logic for TechniciFormular.xaml
    /// </summary>
    public partial class TechniciFormular : UserControl
    {
        public STechnik Model => DataContext as STechnik;
        public STechnikCely _aktualnyTechnik { get; private set; }

        public TechniciFormular()
        {
            InitializeComponent();
            this.DataContextChanged += TechniciFormular_DataContextChanged;
        }

        private void TechniciFormular_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model == null)
            {
                _aktualnyTechnik = null;
                return;
            }
            _aktualnyTechnik = new STechnikCely(Model);
            _aktualnyTechnik.SelectPodlaId(Model.RodneCislo);


            KontrolaLamp.Children.Clear();
            KontrolaStlpov.Children.Clear();
            ServisLamp.Children.Clear();
            ServisStlpov.Children.Clear();

            foreach (var kontrolaLampy in _aktualnyTechnik.KontrolyLamp)
            {
                KontrolaLamp.Children.Add(new FormularGenerator() { Insert = false, DataContext = kontrolaLampy, Margin = new Thickness(0, 5, 0, 5) });
            }

            foreach (var kontrolaStlpu in _aktualnyTechnik.KontrolyStlpov)
            {
                KontrolaStlpov.Children.Add(new FormularGenerator() { Insert = false, DataContext = kontrolaStlpu, Margin = new Thickness(0, 5, 0, 5) });
            }

            foreach (var servisLampy in _aktualnyTechnik.ServisLamp)
            {
                ServisLamp.Children.Add(new FormularGenerator() { Insert = false, DataContext = servisLampy, Margin = new Thickness(0, 5, 0, 5) });
            }

            foreach (var servisStlpu in _aktualnyTechnik.ServisStlpov)
            {
                ServisStlpov.Children.Add(new FormularGenerator() { Insert = false, DataContext = servisStlpu, Margin = new Thickness(0, 5, 0, 5) });
            }

        }
    }
}
