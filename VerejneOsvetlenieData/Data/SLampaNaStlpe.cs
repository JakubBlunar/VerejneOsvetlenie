using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_LAMPA_NA_STLPE", DisplayName = "lampa na ståpe", TableKey = "id_lampy")]
    public class SLampaNaStlpe : SqlEntita
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

        public override bool Update()
        {
            throw new System.NotImplementedException();
        }

        public override bool Insert()
        {
            throw new System.NotImplementedException();
        }

        public override bool Drop()
        {
            throw new System.NotImplementedException();
        }
    }
}