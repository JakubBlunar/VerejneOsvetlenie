namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_ULICA", DisplayName = "")]
    public class SUlica
    {
        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public string IdUlice { get; set; }
        [SqlClass(ColumnName = "NAZOV", DisplayName = "názov")]
        public string Nazov { get; set; }
        [SqlClass(ColumnName = "MESTO")]
        public string Mesto { get; set; }
    }
}