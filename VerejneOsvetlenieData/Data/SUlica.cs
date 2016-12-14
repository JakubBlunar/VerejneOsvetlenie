using System.ComponentModel;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_ULICA", DisplayName = "Ulica")]
    public class SUlica : SqlEntita
    {
        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public int IdUlice { get; set; }
        [SqlClass(ColumnName = "NAZOV", DisplayName = "názov")]
        public string Nazov { get; set; }
        [SqlClass(ColumnName = "MESTO", DisplayName = "")]
        public string Mesto { get; set; }

        public override bool Update()
        {
            return !Databaza.UpdateUlica(IdUlice, Nazov, Mesto).JeChyba;
        }

        public override bool Insert()
        {
            return !Databaza.InsertUlica(Nazov, Mesto).JeChyba;
        }

        public override bool Drop()
        {
            return !Databaza.ZmazUlicu(IdUlice).JeChyba;
        }
    }
}