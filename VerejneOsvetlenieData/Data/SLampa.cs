using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_LAMPA", DisplayName = "Lampa", TableKey = "id_typu")]
    public class SLampa : SqlEntita
    {
        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public string IdTypu { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "svietivosù")]
        public string Svietivost { get; set; }

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