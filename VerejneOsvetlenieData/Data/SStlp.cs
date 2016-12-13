namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_STLP", DisplayName = "Ståp", TableKeyContraint = "cislo = {0}")]
    public class SStlp
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public string IdUlice { get; set; }
        [SqlClass(ColumnName = "VYSKA", DisplayName = "výška")]
        public string Vyska { get; set; }
        [SqlClass(ColumnName = "PORADIE")]
        public string Poradie { get; set; }
        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "dátum inštalácie")]
        public string DatumInstalacie { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "DOPLNKY")]
        public string Doplnky { get; set; }
    }
}