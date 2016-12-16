using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_TECHNIK", DisplayName = "Technik", TableKey = "RODNE_CISLO")]
    public class STechnik : SqlEntita
    {
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "Rodné èíslo", Length = 10)]
        public string RodneCislo { get; set; }

        [SqlClass(ColumnName = "MENO", DisplayName = "Meno")]
        public string Meno { get; set; }

        [SqlClass(ColumnName = "PRIEZVISKO", DisplayName = "Priezvisko")]
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