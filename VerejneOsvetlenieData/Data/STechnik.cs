using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_TECHNIK", DisplayName = "Technik", TableKey = "rodne_cislo")]
    public class STechnik : SqlEntita
    {
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "rodné èíslo")]
        public string RodneCislo { get; set; }

        [SqlClass(ColumnName = "MENO")]
        public string Meno { get; set; }
        [SqlClass(ColumnName = "PRIEZVISKO")]
        public string Priezvisko { get; set; }

        public override bool Update()
        {
            return !Databaza.UpdateTechnik(RodneCislo, Meno, Priezvisko).JeChyba;
        }

        public override bool Insert()
        {
            return !Databaza.VlozTechnika(RodneCislo, Meno, Priezvisko).JeChyba;
        }

        public override bool Drop()
        {
            return !Databaza.ZmazTechnika(RodneCislo).JeChyba;
        }
    }
}