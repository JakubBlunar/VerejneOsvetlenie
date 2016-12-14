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

        public override bool Update()
        {
            return !Databaza.UpdateTypLampy(IdTypu,Svietivost,Typ).JeChyba;
        }

        public override bool Insert()
        {
            return !Databaza.InsertTypLampy(Typ, Svietivost).JeChyba;
        }

        public override bool Drop()
        {
            return !Databaza.ZmazTypLampy(IdTypu).JeChyba;
        }
    }
}