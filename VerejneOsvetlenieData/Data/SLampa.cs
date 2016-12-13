namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_LAMPA", DisplayName = "Lampa", TableKeyContraint = "id_typu = {0}")]
    public class SLampa
    {
        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public string IdTypu { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "svietivosù")]
        public string Svietivost { get; set; }
    }
}