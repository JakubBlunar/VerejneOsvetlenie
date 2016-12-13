using System.Drawing;
using System.Windows.Media.Imaging;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_KONTROLA", DisplayName = "Kontrola", TableKeyContraint = "id_sluzby = {0}")]
    public class SKontrola : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "STAV")]
        public string Stav { get; set; }
        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivosť")]
        public string Svietivost { get; set; }

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

    //[SqlClass(TableName = "S_OBSLUHA_STLPU", DisplayName = "")]
    //public class SObsluhaStlpu
    //{
    //    [SqlClass(ColumnName = "CISLO", DisplayName = "")]
    //    public string Cislo { get; set; }
    //    [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = "")]
    //    public string IdSluzby { get; set; }
    //}

    //[SqlClass(TableName = "S_OBSLUHA_LAMPY", DisplayName = null)]
    //public class SObsluhaLampy
    //{
    //    [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
    //    public string IdLampy { get; set; }
    //    [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
    //    public string IdSluzby { get; set; }
    //}


}
