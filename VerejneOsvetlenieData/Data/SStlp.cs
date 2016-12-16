using PropertyChanged;
using System;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_STLP", DisplayName = "Ståp", TableKey = "CISLO")]
    public class SStlp : SqlEntita
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public int Cislo { get; set; }

        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public int IdUlice { get; set; }
        [SqlClass(ColumnName = "ID_ULICE", IsReference = true)]
        public SUlica Ulica { get; set; }

        [SqlClass(ColumnName = "VYSKA", DisplayName = "Výška")]
        public int Vyska { get; set; }

        [SqlClass(ColumnName = "PORADIE", DisplayName = "Poradie")]
        public int Poradie { get; set; }

        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "Dátum inštalácie")]
        public string DatumInstalacie { get; set; }

        [SqlClass(ColumnName = "TYP", DisplayName = "Typ")]
        public char Typ { get; set; }

        //[SqlClass(ColumnName = "DOPLNKY")]
        //public string Doplnky { get; set; }

        public override bool Update()
        {
            Databaza.UpdateStlp(Cislo, IdUlice, Vyska, Poradie, DatumInstalacie, Typ);
            return true;
        }

        public override bool Insert()
        {
            Databaza.InsertStlp(Cislo, IdUlice, Vyska, Poradie, DatumInstalacie, Typ);
            return true;
        }

        public override bool Drop()
        {
            return !Databaza.DeleteStlp(Cislo).JeChyba;
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            Ulica = new SUlica();
            bool b1 = base.SelectPodlaId(paIdEntity);
            bool b2 = Ulica.SelectPodlaId(IdUlice);
            return b1 && b2;
        }
    }
}