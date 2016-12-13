using System.IO;

namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_INFO", DisplayName = "info o ståpe", TableKeyContraint = "id = {0}")]
    public class SInfo
    {
        [SqlClass(ColumnName = "ID", DisplayName = null)]
        public string Id { get; set; }
        [SqlClass(ColumnName = "CISLO", DisplayName = null, IsBitmapImage = true)]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "DATA", DisplayName = "obrázok", IsBitmapImage = true)]
        public FileStream Data { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "DATUM", DisplayName = "dátum")]
        public string Datum { get; set; }
    }
}