using PropertyChanged;
using System;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_STLP", DisplayName = "Ståp", TableKey = "CISLO")]
    public class SStlp : SqlEntita
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = "Èíslo ståpu", ReadOnly = true)]
        public int Cislo { get; set; }

        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public int IdUlice { get; set; }

        [SqlClass(ColumnName = "VYSKA", DisplayName = "Výška")]
        public int Vyska { get; set; }

        [SqlClass(ColumnName = "PORADIE", DisplayName = "Poradie")]
        public int Poradie { get; set; }

        [SqlClass(ColumnName = "TYP", DisplayName = "Typ")]
        public char Typ { get; set; }

        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "Dátum inštalácie", IsDate = true)]
        public string DatumInstalacie { get ; set; }

        //[SqlClass(ColumnName = "DOPLNKY")]
        //public string Doplnky { get; set; }

        public SStlp()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            DateTime datum;
            try
            {
                datum = DateTime.Parse(DatumInstalacie);
            }
            catch
            {
                ErrorMessage = "Nespravny datum";
                return false;
            }
            char? typ = Typ;
            if (string.IsNullOrWhiteSpace(Typ + "")) typ = null;

            int? poradie = Poradie;
            if (string.IsNullOrWhiteSpace(Poradie + "")) poradie = null;
            if (poradie != null && poradie < -1) poradie = null;

            return UseDbMethod(Databaza.UpdateStlp(Cislo, IdUlice, Vyska, datum, poradie, typ));
        }

        public override bool Insert()
        {
            DateTime datum;
            try
            {
                datum = DateTime.Parse(DatumInstalacie);
            }
            catch
            {
                ErrorMessage = "Nesprávny datum.";
                return false;
            }

            char? typ = Typ;
            if (string.IsNullOrWhiteSpace(Typ + "")) typ = null;

            int? poradie = Poradie;
            if (string.IsNullOrWhiteSpace(Poradie + "")) poradie = null;
            if (poradie != null && poradie < -1) poradie = null;

            return UseDbMethod(Databaza.InsertStlp(IdUlice, Vyska, datum, poradie, typ));
        }

        public override bool Drop()
        {
            return UseDbMethod(Databaza.DeleteStlp(Cislo));
        }

   
    }
}