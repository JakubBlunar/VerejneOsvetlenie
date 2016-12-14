using System;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_OBSLUHA_LAMPY", DisplayName = "obsluha lampy", TableKey = "ID_SLUZBY")]
    public class SObsluhaLampy : SqlEntita
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
        public int IdLampy { get; set; }
        [SqlClass(ColumnName = "ID_LAMPY", IsReference = true)]
        public SLampa Lampa { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SSluzba Sluzba { get; set; }
        
        public override bool Drop()
        {
            throw new NotImplementedException();
        }

        public override bool Insert()
        {
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
