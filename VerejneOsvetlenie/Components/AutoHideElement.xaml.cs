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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VerejneOsvetlenie.Components
{
    /// <summary>
    /// Interaction logic for AutoHideElement.xaml
    /// </summary>
    [ContentProperty(nameof(Children))]
    public partial class AutoHideElement : UserControl
    {
        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly(
            nameof(Children),  // Prior to C# 6.0, replace nameof(Children) with "Children"
            typeof(UIElementCollection),
            typeof(AutoHideElement),
            new PropertyMetadata());

        public UIElementCollection Children
        {
            get { return (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty); }
            private set { SetValue(ChildrenProperty, value); }
        }


        public TValue Value<TValue>() where TValue : class => StackPanel.Children[0] as TValue;

        public AutoHideElement()
        {
            InitializeComponent();
            Hidden = true;
            Children = StackPanel.Children;
            StackPanel.MaxHeight = 0;
            ShowHideButton.Content = char.ConvertFromUtf32(Hidden ? 0xE8C4 : 0xE8C5);
        }

        public bool Hidden { get; private set; }

        private void ShowHideMenu(object sender, RoutedEventArgs e)
        {
            Hidden = !Hidden;
            ((Button)sender).Content = char.ConvertFromUtf32(Hidden ? 0xE8C4 : 0xE8C5);
            if (Hidden)
                ((Storyboard)this.Resources["HideAnimation"]).Begin();
            else
                ((Storyboard)this.Resources["ShowAnimation"]).Begin();
        }
    }
}
