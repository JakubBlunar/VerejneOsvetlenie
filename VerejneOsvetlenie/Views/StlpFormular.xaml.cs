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
    /// Interaction logic for StlpFormular.xaml
    /// </summary>
    public partial class StlpFormular : UserControl
    {
        public SStlpCely _aktualnyStlp { get; private set; }
        public StlpFormular()
        {
            InitializeComponent();
            this.DataContextChanged += StlpFormular_DataContextChanged;
        }

        private void StlpFormular_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(DataContext is SStlp))
                return;
            _aktualnyStlp = new SStlpCely((SStlp) DataContext);
            _aktualnyStlp.SelectPodlaId(null);

            Stlp.DataContext = _aktualnyStlp.SStlp;
            Doplnky.ItemsSource = _aktualnyStlp.Doplnky;


        }
    }
}
