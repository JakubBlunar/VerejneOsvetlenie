using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
using VerejneOsvetlenieData.Data.Interfaces;
using Image = System.Windows.Controls.Image;

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for FormularGenerator.xaml
    /// </summary>
    public partial class FormularGenerator : UserControl
    {
        public SqlEntita ModelAkoEntita => Model as SqlEntita;
        public object Model => this.DataContext;
        public StavyFormulara AktualnyStav { get; private set; }
        private SqlEntita _aktualnaEntita;

        public FormularGenerator()
        {
            InitializeComponent();
            this.DataContextChanged += FormularGenerator_DataContextChanged;
        }

        private void FormularGenerator_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Reset();
            GenerujFormular();
        }

        private void Reset()
        {
            Formular.Children.Clear();
            Formular.RowDefinitions.Clear();
            AktualnyStav = StavyFormulara.Init;
            HlavnyGrid.IsEnabled = false;
            NastavTlacidla(true, false, true);
        }

        public void GenerujFormular()
        {
            if (Model == null)
            {
                if (ModelAkoEntita == null)
                    NastavTlacidla(false, false, false);
                return;
            }
            if (ModelAkoEntita != null)
                GenerujPodlaSqlEntity();
            //else
            //    GenerujPodlaNeznamehoTypu();

        }

        private void GenerujPodlaNeznamehoTypu()
        {
            var props = DataContext.GetType().GetProperties();
            FormularTitulok.Text = DataContext.GetType().Name;
            foreach (var propertyInfo in props)
            {
                this.DajNovyRiadok();
                var label = this.DajLabel(propertyInfo);
                var box = this.DajInputBox(propertyInfo);

                label.SetValue(Grid.RowProperty, Formular.RowDefinitions.Count - 1);
                label.SetValue(Grid.ColumnProperty, 0);
                box.SetValue(Grid.RowProperty, Formular.RowDefinitions.Count - 1);
                box.SetValue(Grid.ColumnProperty, 1);
                Formular.Children.Add(label);
                Formular.Children.Add(box);
            }
        }

        private void GenerujPodlaSqlEntity(SqlEntita paRefencia = null)
        {
            var model = _aktualnaEntita = paRefencia ?? ModelAkoEntita;
            var atr = SqlClassAttribute.ExtractSqlClassAttribute(model);
            if (atr.IgnoreEntity)
                return;
            FormularTitulok.Text = DajAtributTabulky(model).ElementName;
            var props = model.GetType().GetProperties();
            foreach (var propertyInfo in props)
            {
                var atribut = this.DajAtributStlpca(propertyInfo);
                if (atribut.ShowElement == false)
                    continue;
                if (atribut.IsReference)
                {
                    var property = propertyInfo.GetValue(model) as SqlEntita;// ?? Activator.CreateInstance(propertyInfo.PropertyType) as SqlEntita;
                    if (property == null)
                        continue;
                    GenerujPodlaSqlEntity(property);
                }
                DajNovyRiadok();
                var label = DajLabel(propertyInfo, atribut);
                UIElement inputBoxOrImage = null;
                if (atribut?.IsBitmapImage == false)
                    inputBoxOrImage = DajInputBox(propertyInfo, model, atribut);
                else
                {
                    inputBoxOrImage = new Image();
                    var img = new BitmapImage
                    {
                        StreamSource = (MemoryStream)propertyInfo.GetValue(Model)
                    };
                    ((Image)inputBoxOrImage).Source = img;
                }
                label.SetValue(Grid.RowProperty, Formular.RowDefinitions.Count - 1);
                label.SetValue(Grid.ColumnProperty, 0);
                inputBoxOrImage.SetValue(Grid.RowProperty, Formular.RowDefinitions.Count - 1);
                inputBoxOrImage.SetValue(Grid.ColumnProperty, 1);
                Formular.Children.Add(label);
                Formular.Children.Add(inputBoxOrImage);
            }
        }

        private TextBox DajInputBox(PropertyInfo paPropertyInfo, SqlEntita paEntita = null, SqlClassAttribute paAttribut = null)
        {
            var box = new TextBox
            {
                FontSize = 20,
                IsReadOnly = !paPropertyInfo.CanWrite || paAttribut?.ReadOnly == true,
                Margin = new Thickness(5, 5, 0, 5),
                MaxWidth = 180,
                MaxLines = 10,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = (paAttribut?.Length ?? 0) != 0 ? paAttribut.Length : 50
            };
            box.SetBinding(TextBox.TextProperty, new Binding()
            {
                Path = new PropertyPath(paPropertyInfo.Name),
                Source = paEntita ?? DataContext,
                Mode = paPropertyInfo.CanWrite ? BindingMode.TwoWay : BindingMode.OneWay,
                ConverterCulture = CultureInfo.CurrentCulture,
                StringFormat = paAttribut?.SpecialFormat
            });
            return box;
        }

        private TextBlock DajLabel(PropertyInfo paPropertyInfo, SqlClassAttribute paAttribut = null)
        {
            return new TextBlock
            {
                FontSize = 20,
                Text = paAttribut?.ElementName ?? paPropertyInfo.Name,
                Margin = new Thickness(0, 5, 5, 5)
            };
        }

        private SqlClassAttribute DajAtributTabulky(SqlEntita paEntita)
        {
            return SqlClassAttribute.ExtractSqlClassAttribute(paEntita);
        }

        private SqlClassAttribute DajAtributStlpca(PropertyInfo paPropertyInfo)
        {
            return SqlClassAttribute.ExtractSqlClassAttribute(paPropertyInfo);
        }

        private RowDefinition DajNovyRiadok()
        {
            var riadok = new RowDefinition
            {
                Height = GridLength.Auto
            };
            Formular.RowDefinitions.Add(riadok);
            return riadok;
        }

        private RowDefinition DajNovyNovyStlpec()
        {
            return new RowDefinition
            {
                Height = GridLength.Auto
            };
        }

        private void Upravit_Click(object sender, RoutedEventArgs e)
        {
            HlavnyGrid.IsEnabled = true;
            AktualnyStav = StavyFormulara.Update;
            NastavTlacidla(false, true, true);
        }

        private void Ulozit_Click(object sender, RoutedEventArgs e)
        {
            HlavnyGrid.IsEnabled = false;
            if (AktualnyStav == StavyFormulara.Update)
                _aktualnaEntita.Update();
            else if (AktualnyStav == StavyFormulara.Insert)
                _aktualnaEntita.Insert();

            AktualnyStav = StavyFormulara.Init;
            NastavTlacidla(true, false, true);
        }

        private void NastavTlacidla(bool paUprav, bool paUloz, bool paNovy)
        {
            Novy.Visibility = paNovy ? Visibility.Visible : Visibility.Collapsed;
            Upravit.Visibility = paUprav ? Visibility.Visible : Visibility.Collapsed;
            Ulozit.Visibility = paUloz ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Novy_Click(object sender, RoutedEventArgs e)
        {
            var novaEntita = Activator.CreateInstance(ModelAkoEntita.GetType()) as SqlEntita;
            Reset();
            AktualnyStav = StavyFormulara.Insert;
            NastavTlacidla(false, true, true);
            GenerujPodlaSqlEntity(novaEntita);
            HlavnyGrid.IsEnabled = true;
        }
    }

    public enum StavyFormulara
    {
        Init, Update, Insert
    }
}
