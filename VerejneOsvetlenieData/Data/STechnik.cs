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

        [SqlClass(ColumnName = "MENO", DisplayName = "Meno", Length = 30)]
        public string Meno { get; set; }

        [SqlClass(ColumnName = "PRIEZVISKO", DisplayName = "Priezvisko", Length = 30)]
        public string Priezvisko { get; set; }

        public STechnik()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            return UseDbMethod(Databaza.UpdateTechnik(RodneCislo, Meno, Priezvisko));
        }

        public override bool Insert()
        {
            return UseDbMethod(Databaza.VlozTechnika(RodneCislo, Meno, Priezvisko));
        }

        public override bool Drop()
        {
            return UseDbMethod(Databaza.ZmazTechnika(RodneCislo));
        }
    }
}