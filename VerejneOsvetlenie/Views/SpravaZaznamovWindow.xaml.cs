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
using System.Windows.Shapes;
using VerejneOsvetlenie.ViewModels;

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for SpravaZaznamovWindow.xaml
    /// </summary>
    public partial class SpravaZaznamovWindow : Window
    {
        public SpravaZaznamovViewModel Model => DataContext as SpravaZaznamovViewModel;
        public SpravaZaznamovWindow()
        {
            InitializeComponent();
            Tabulka.UserKlikolNaElementMamId += Tabulka_UserKlikolNaElementMamId;
        }

        private void Tabulka_UserKlikolNaElementMamId(object sender, object e)
        {
            Model.AktualnaEntita = Model.AktualnyTypZaznamu.DajInstanciu();
            Model.AktualnaEntita.SelectPodlaId(e);
        }

        private void VybranyNovyTypZaznamov(object sender, SelectionChangedEventArgs e)
        {
            Model.AktualnyTypZaznamu = ((ListView)sender).SelectedItem as ZaznamInfo;
            if (Model?.AktualnyTypZaznamu != null)
                Model.AktualnyVystup = Model.AktualnyTypZaznamu.DajInstanciu().GetSelectOnTableData();
        }
    }
}
