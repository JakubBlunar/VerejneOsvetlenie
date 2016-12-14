using PropertyChanged;
using System;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_LAMPA_NA_STLPE", DisplayName = "lampa na ståpe", TableKey = "ID_LAMPY")]
    public class SLampaNaStlpe : SqlEntita
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
        public int IdLampy { get; set; }

        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public int Cislo { get; set; }
        [SqlClass(ColumnName = "CISLO", IsReference = true)]
        public SStlp Stlp { get; set; }

        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public int IdTypu { get; set; }
        [SqlClass(ColumnName = "ID_TYPU", IsReference = true)]
        public SLampa Lampa { get; set; }

        [SqlClass(ColumnName = "STAV", DisplayName = "Stav")]
        public char Stav { get; set; }

        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "Dátum inštalácie")]
        public DateTime DatumInstalacie { get; set; }

        [SqlClass(ColumnName = "DATUM_DEMONTAZE", DisplayName = "Dátum demontáže")]
        public DateTime? DatumDemontaze { get; set; }

        public override bool Update()
        {
            return !Databaza.UpdateLampaNaStlpe(IdLampy, Cislo, IdTypu, Stav, DatumInstalacie, DatumDemontaze).JeChyba;
        }

        public override bool Insert()
        {
            return !Databaza.InsertLampaNaStlpe(Cislo, IdTypu, Stav, DatumInstalacie).JeChyba;
        }

        public override bool Drop()
        {
            return !Databaza.ZmazLampuNaStlpe(IdLampy).JeChyba;
        }
    }
}