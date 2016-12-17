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
        public string DatumInstalacie { get; set; }

        [SqlClass(ColumnName = "DATUM_DEMONTAZE", DisplayName = "Dátum demontáže")]
        public string DatumDemontaze { get; set; }

        public SLampaNaStlpe()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            DateTime? demontaz = null;
            if (DatumDemontaze != "")
                demontaz = DateTime.Parse(DatumDemontaze);
            return UseDbMethod(Databaza.UpdateLampaNaStlpe(IdLampy, Cislo, IdTypu, Stav, DateTime.Parse(DatumInstalacie),demontaz));
        }

        public override bool Insert()
        {
            return UseDbMethod(Databaza.InsertLampaNaStlpe(Cislo, IdTypu, Stav, DateTime.Parse(DatumInstalacie)));
        }

        public override bool Drop()
        {
            return UseDbMethod(Databaza.ZmazLampuNaStlpe(IdLampy));
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            Stlp = new SStlp();
            Lampa = new SLampa();
            return base.SelectPodlaId(paIdEntity)
                && Stlp.SelectPodlaId(Cislo)
                && Lampa.SelectPodlaId(IdTypu);
        }
    }
}