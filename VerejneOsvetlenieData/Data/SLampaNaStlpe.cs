namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_LAMPA_NA_STLPE", DisplayName = "lampa na ståpe", TableKeyContraint = "id_lampy = {0}")]
    public class SLampaNaStlpe
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
        public string IdLampy { get; set; }
        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public string IdTypu { get; set; }
        [SqlClass(ColumnName = "STAV")]
        public string Stav { get; set; }
        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "")]
        public string DatumInstalacie { get; set; }
        [SqlClass(ColumnName = "DATUM_DEMONTAZE", DisplayName = "")]
        public string DatumDemontaze { get; set; }
    }
}