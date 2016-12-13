namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_TECHNIK", DisplayName = "Technik", TableKeyContraint = "rodne_cislo = {0}")]
    public class STechnik
    {
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "")]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "MENO")]
        public string Meno { get; set; }
        [SqlClass(ColumnName = "PRIEZVISKO")]
        public string Priezvisko { get; set; }
    }
}