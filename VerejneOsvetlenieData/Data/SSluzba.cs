namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_SLUZBA", DisplayName = "", TableKeyContraint = "id_sluzby = {0}")]
    public class SSluzba
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = null)]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "DATUM", DisplayName = "dátum")]
        public string Datum { get; set; }
        [SqlClass(ColumnName = "POPIS")]
        public string Popis { get; set; }
        [SqlClass(ColumnName = "TRVANIE")]
        public string Trvanie { get; set; }
    }
}