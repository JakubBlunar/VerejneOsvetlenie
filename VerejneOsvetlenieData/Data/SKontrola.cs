using System.Drawing;
using System.Windows.Media.Imaging;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;
using System;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_KONTROLA", DisplayName = "Kontrola", TableKey = "ID_SLUZBY")]
    public class SKontrola : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SSluzba Sluzba { get; set; }

        [SqlClass(ColumnName = "STAV", DisplayName = "Stav")]
        public char Stav { get; set; }

        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivosť")]
        public int Svietivost { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SObsluhaLampy ObsluhaLampy { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SObsluhaStlpu ObsluhaStlpu { get; set; }

        public override bool Update()
        {
            throw new System.NotImplementedException();
        }

        public override bool Insert()
        {
            if (ObsluhaStlpu != null && IdSluzby == ObsluhaStlpu.IdSluzby)
                return !Databaza.VlozKontroluStlpu(Sluzba.RodneCislo, ObsluhaStlpu.Cislo, Sluzba.Popis, Stav, 
                    Sluzba.Trvanie,DateTime.Parse(Sluzba.Datum)).JeChyba;
            if (ObsluhaLampy != null && IdSluzby == ObsluhaLampy.IdSluzby)
                return !Databaza.VlozKontroluLampy(Sluzba.RodneCislo, ObsluhaLampy.IdLampy, Sluzba.Popis, Stav,
                    Sluzba.Trvanie, DateTime.Parse(Sluzba.Datum), Svietivost).JeChyba;
            return false;
        }

        public override bool Drop()
        {
            throw new System.NotImplementedException();
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            Sluzba = new SSluzba();
            ObsluhaLampy = new SObsluhaLampy();
            ObsluhaStlpu = new SObsluhaStlpu();
            if (base.SelectPodlaId(paIdEntity) && Sluzba.SelectPodlaId(IdSluzby) == false)
                return false;
            ObsluhaLampy = ObsluhaLampy.SelectPodlaId(IdSluzby) ? ObsluhaLampy : null;
            ObsluhaStlpu = ObsluhaStlpu.SelectPodlaId(IdSluzby) ? ObsluhaStlpu : null;
            return ObsluhaStlpu != null|| ObsluhaLampy != null ;        
        }
    }
}
