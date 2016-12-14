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
        public Select Model => DataContext as Select;
        private Select _aktualnySelect;

        public TableGenerator()
        {
            InitializeComponent();
            DataContextChanged += TableGenerator_DataContextChanged;
        }

        private void TableGenerator_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataGrid.Columns.Clear();
            if (_aktualnySelect != null)
                _aktualnySelect.SelectDataHotove -= Model_SelectDataHotove;
            if (Model == null)
            {
                _aktualnySelect = null;
                return;
            }
            Model.SelectDataHotove += Model_SelectDataHotove;
            _aktualnySelect = Model;
            Model.SelectData();
        }

        private void Model_SelectDataHotove(object sender, EventArgs e)
        {
            GenerujTabulku();
        }

        private void GenerujTabulku()
        {
            for (int i = 0; i < Model.Columns.Count; i++)
            {
                var stlpec = new DataGridTextColumn
                {
                    Header = Model.Columns[i],
                    Binding = new Binding($"[{i}]")
                };
                DataGrid.Columns.Add(stlpec);
            }
            DataGrid.ItemsSource = Model.Rows;
            DataGrid.Columns.RemoveAt(DataGrid.Columns.Count - 1);
            DataGrid.Columns.RemoveAt(DataGrid.Columns.Count - 1);
        }
    }
}
