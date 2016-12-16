using System;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data.Legacy
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_SERVIS", DisplayName = "Servis", TableKey = "ID_SLUZBY")]
    public class SServis : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SSluzba Sluzba { get; set; }

        [SqlClass(ColumnName = "CENA")]
        public int Cena { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SObsluhaLampy ObsluhaLampy { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SObsluhaStlpu ObsluhaStlpu { get; set; }

        public override bool Update()
        {
            if (ObsluhaStlpu != null && IdSluzby == ObsluhaStlpu.IdSluzby)
                return !Databaza.UpdateServisuStlpu(IdSluzby, Sluzba.RodneCislo, ObsluhaStlpu.Cislo, Sluzba.Popis,
                    Sluzba.Trvanie, DateTime.Parse(Sluzba.Datum), Cena).JeChyba;
            if (ObsluhaLampy != null && IdSluzby == ObsluhaLampy.IdSluzby)
                return !Databaza.UpdateServisuLampy(IdSluzby, Sluzba.RodneCislo, ObsluhaLampy.IdLampy, Sluzba.Popis,
                    Sluzba.Trvanie, DateTime.Parse(Sluzba.Datum), Cena).JeChyba;
            return false;
        }

        public override bool Insert()
        {
            if (ObsluhaStlpu != null && IdSluzby == ObsluhaStlpu.IdSluzby)
                return !Databaza.VlozServisStlpu(Sluzba.RodneCislo, ObsluhaStlpu.Cislo, Sluzba.Popis, Sluzba.Trvanie, DateTime.Parse(Sluzba.Datum), Cena).JeChyba;
            if (ObsluhaLampy != null && IdSluzby == ObsluhaLampy.IdSluzby)
                return !Databaza.VlozServisLampy(Sluzba.RodneCislo, ObsluhaLampy.IdLampy, Sluzba.Popis, Sluzba.Trvanie, DateTime.Parse(Sluzba.Datum), Cena).JeChyba;
            return false;
        }

        public override bool Drop()
        {
            return Databaza.ZmazSluzbu(IdSluzby).JeChyba;
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
            return ObsluhaLampy != null || ObsluhaStlpu != null;
        }
    }
}