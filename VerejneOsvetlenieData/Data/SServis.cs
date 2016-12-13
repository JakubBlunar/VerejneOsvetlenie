namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_SERVIS", DisplayName = "Servis")]
    public class SServis
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "CENA")]
        public string Cena { get; set; }
    }
}