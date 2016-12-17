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
    /// Interaction logic for TechnikFormular.xaml
    /// </summary>
    public partial class TechnikFormular : UserControl
    {
        public STechnik Model => DataContext as STechnik;
        public STechnikCely _aktualnyTechnik { get; private set; }

        public TechnikFormular()
        {
            InitializeComponent();
            this.DataContextChanged += TechnikFormular_DataContextChanged;
        }

        private void TechnikFormular_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model == null)
            {
                _aktualnyTechnik = null;
                return;
            }
            _aktualnyTechnik = new STechnikCely(Model);
            //Technik.DataContext = _aktualnyTechnik.Technik;






        }
    }
}
