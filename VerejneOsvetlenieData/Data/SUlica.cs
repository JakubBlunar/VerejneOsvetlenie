using System.ComponentModel;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_ULICA", DisplayName = "Ulica", TableKey = "ID_ULICE")]
    public class SUlica : SqlEntita
    {
        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public int IdUlice { get; set; }

        [SqlClass(ColumnName = "NAZOV", DisplayName = "Ulica", Length = 30)]
        public string Nazov { get; set; }

        [SqlClass(ColumnName = "MESTO", DisplayName = "Mesto", Length = 30)]
        public string Mesto { get; set; }

        public override bool Update()
        {
            return useDbMethod(Databaza.UpdateUlica(IdUlice, Nazov, Mesto));
        }

        public override bool Insert()
        {
            return useDbMethod(Databaza.InsertUlica(Nazov, Mesto));
        }

        public override bool Drop()
        {
            return useDbMethod(Databaza.ZmazUlicu(IdUlice));
        }
    }
}