using PropertyChanged;
using System;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_STLP", DisplayName = "St�p", TableKey = "CISLO")]
    public class SStlp : SqlEntita
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public string Cislo { get; set; }

        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public string IdUlice { get; set; }
        [SqlClass(ColumnName = "ID_ULICE", IsReference = true)]
        public SUlica Ulica { get; set; }

        [SqlClass(ColumnName = "VYSKA", DisplayName = "V��ka")]
        public int Vyska { get; set; }

        [SqlClass(ColumnName = "PORADIE", DisplayName = "Poradie")]
        public int Poradie { get; set; }

        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "D�tum in�tal�cie")]
        public DateTime? DatumInstalacie { get; set; }

        [SqlClass(ColumnName = "TYP", DisplayName = "Typ")]
        public string Typ { get; set; }

        //[SqlClass(ColumnName = "DOPLNKY")]
        //public string Doplnky { get; set; }

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