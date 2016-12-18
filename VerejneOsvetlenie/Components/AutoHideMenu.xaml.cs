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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VerejneOsvetlenie.Components
{
    /// <summary>
    /// Interaction logic for AutoHideMenu.xaml
    /// </summary>
    public partial class AutoHideMenu : UserControl
    {
        public AutoHideMenu()
        {
            InitializeComponent();
            Hidden = true;
            Container.MaxHeight = 0;
            ShowHideButton.Content = char.ConvertFromUtf32(Hidden ? 0xE8C4 : 0xE8C5);
        }

        public bool Hidden { get; private set; }
        public UIElementCollection Children => Container.Children;

        private void ShowHideMenu(object sender, RoutedEventArgs e)
        {
            Hidden = !Hidden;
            ((Button) sender).Content = char.ConvertFromUtf32(Hidden ? 0xE8C4 : 0xE8C5);
            if(Hidden)
                ((Storyboard) this.Resources["HideAnimation"]).Begin();
            else
                ((Storyboard)this.Resources["ShowAnimation"]).Begin();
        }
    }
}
