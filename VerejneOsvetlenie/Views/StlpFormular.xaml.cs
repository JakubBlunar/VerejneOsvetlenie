using System;
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

            NovyDoplnok.Update = false;
            NovyDoplnok.Delete = false;
            NovyDoplnok.DataContext = new TDoplnok() { Cislo = _aktualnyStlp.SStlp.Cislo };
            NovyDoplnok.ActionWithNewElement = entita =>
            {
                ((TDoplnok) entita).Cislo = _aktualnyStlp.SStlp.Cislo;
            };

            Stlp.Insert = false;
            Stlp.DataContext = _aktualnyStlp.SStlp;
            Udaje.Children.Clear();
            Obrazky.Children.Clear();
            Lampy.Children.Clear();
            foreach (var doplnok in _aktualnyStlp.Doplnky)
            {
                Udaje.Children.Add(new FormularGenerator() { Insert = false, DataContext = doplnok, Margin = new Thickness(0, 5, 0, 5) });
            }

            NovyObrazok.CisloStlpu = _aktualnyStlp.SStlp.Cislo;
            NovyObrazok.Zmazat.Visibility = Visibility.Collapsed;
            NovyObrazok.Upravit.Visibility = Visibility.Collapsed;
            foreach (var sInfo in _aktualnyStlp.SInformacie)
            {
                Obrazky.Children.Add(new InfoStlpu()
                {
                    Update = true,
                    DataContext = sInfo
                });
            }

            NovaLampa.Delete = false;
            NovaLampa.Update = false;
            NovaLampa.DataContext = new SLampaNaStlpe() { Cislo = _aktualnyStlp.SStlp.Cislo };
            NovaLampa.ActionWithNewElement = entita =>
            {
                ((SLampaNaStlpe) entita).Cislo = _aktualnyStlp.SStlp.Cislo;
            };

            foreach (var lampaNaStlpe in _aktualnyStlp.SLampyNaStlpe)
            {
                Lampy.Children.Add(new FormularGenerator() { Insert = false, DataContext = lampaNaStlpe, Margin = new Thickness(0, 5, 0, 5) });
            }
        }
    }
}
