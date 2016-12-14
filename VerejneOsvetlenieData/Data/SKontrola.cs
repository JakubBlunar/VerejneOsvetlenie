using System.Drawing;
using System.Windows.Media.Imaging;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_KONTROLA", DisplayName = "Kontrola", TableKey = "ID_SLUZBY")]
    public class SKontrola : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SSluzba sluzba { get; set; }

        [SqlClass(ColumnName = "STAV", DisplayName = "Stav")]
        public char Stav { get; set; }

        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivosť")]
        public int Svietivost { get; set; }

        public override bool Update()
        {
            throw new System.NotImplementedException();
        }

        public override bool Insert()
        {
            throw new System.NotImplementedException();
        }

        public override bool Drop()
        {
            throw new System.NotImplementedException();
        }
    }
}
