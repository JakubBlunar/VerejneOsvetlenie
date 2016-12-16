using System;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data.Legacy
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_OBSLUHA_LAMPY", DisplayName = "obsluha lampy", TableKey = "ID_SLUZBY")]
    public class SObsluhaLampy : SqlEntita
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
        public int IdLampy { get; set; }
        [SqlClass(ColumnName = "ID_LAMPY", IsReference = true)]
        public SLampaNaStlpe LampaNaStlpe { get; set; }

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

        public override bool SelectPodlaId(object paIdEntity)
        {
            Sluzba = new SSluzba();
            LampaNaStlpe = new SLampaNaStlpe();
            bool b1 = base.SelectPodlaId(paIdEntity);
            bool b2 = Sluzba.SelectPodlaId(IdSluzby);
            bool b3 = LampaNaStlpe.SelectPodlaId(IdLampy);
            return b1 && b2 && b3;
        }
    }
}
