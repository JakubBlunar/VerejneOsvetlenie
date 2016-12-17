using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_LAMPA", DisplayName = "Lampa", TableKey = "ID_TYPU")]
    public class SLampa : SqlEntita
    {
        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public int IdTypu { get; set; }

        [SqlClass(ColumnName = "TYP")]
        public char Typ { get; set; }

        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivosù")]
        public int Svietivost { get; set; }

        public SLampa()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            return useDbMethod(Databaza.UpdateTypLampy(IdTypu,Svietivost,Typ));
        }

        public override bool Insert()
        {
            return useDbMethod(Databaza.InsertTypLampy(Typ, Svietivost));
        }

        public override bool Drop()
        {
            return useDbMethod(Databaza.ZmazTypLampy(IdTypu));
        }
    }
}