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
using VerejneOsvetlenieData.Data.Tables;

namespace VerejneOsvetlenie.Views
{
    /// <summary>
    /// Interaction logic for TableGenerator.xaml
    /// </summary>
    public partial class TableGenerator : UserControl
    {
        public PomenovanyVystup Model => DataContext as PomenovanyVystup;
        private IVystup _aktualnyVystup;

        public TableGenerator()
        {
            InitializeComponent();
            DataContextChanged += TableGenerator_DataContextChanged;
        }

        private void TableGenerator_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataGrid.Columns.Clear();
            if (_aktualnyVystup != null)
                _aktualnyVystup.VystupSpracovany -= ModelOnVystupSpracovany;
            if (Model == null)
            {
                _aktualnyVystup = null;
                return;
            }
            _aktualnyVystup = Model.Vystup;
            _aktualnyVystup.VystupSpracovany += ModelOnVystupSpracovany;
            _aktualnyVystup.SpustiVystup();
        }

        private void ModelOnVystupSpracovany(object sender, EventArgs eventArgs)
        {
            GenerujTabulku();
        }

        private void GenerujTabulku()
        {
            for (int i = 0; i < _aktualnyVystup.Columns.Count; i++)
            {
                var stlpec = new DataGridTextColumn
                {
                    Header = _aktualnyVystup.Columns[i],
                    Binding = new Binding($"[{i}]")
                };
                DataGrid.Columns.Add(stlpec);
            }
            DataGrid.ItemsSource = _aktualnyVystup.Rows;
            while (DataGrid.Columns.Count - _aktualnyVystup.Columns.Count > 0)
                DataGrid.Columns.RemoveAt(DataGrid.Columns.Count - 1);
            PocetRiadkov.Text = _aktualnyVystup.Rows.Count().ToString();
        }
    }
}
