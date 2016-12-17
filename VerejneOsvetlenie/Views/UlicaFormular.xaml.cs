using System;
using System.Collections.Generic;
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

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for UlicaFormular.xaml
    /// </summary>
    public partial class UlicaFormular : UserControl
    {
        public SUlica Model => DataContext as SUlica;
        public SUlicaCela _aktualnaUlica { get; private set; }
        public UlicaFormular()
        {
            InitializeComponent();
            this.DataContextChanged += UlicaFormular_DataContextChanged;
        }

        private void UlicaFormular_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model == null)
            {
                _aktualnaUlica = null;
                return;
            }
            _aktualnaUlica = new SUlicaCela(Model);
            _aktualnaUlica.SelectPodlaId(null);
            NovyStlp.IdUlice = _aktualnaUlica.SUlica.IdUlice;

            Ulica.DataContext = _aktualnaUlica.SUlica;
            Udaje.Children.Clear();
            foreach (var stlp in _aktualnaUlica.Stlpy)
            {
                Udaje.Children.Add(new FormularGenerator() { DataContext = stlp, Margin = new Thickness(0, 5, 0, 5) });
            }
        }

    }
}
