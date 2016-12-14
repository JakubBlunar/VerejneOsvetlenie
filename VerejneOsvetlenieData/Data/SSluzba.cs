using PropertyChanged;
using System;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_SLUZBA", DisplayName = "", TableKey = "ID_SLUZBY")]
    public class SSluzba : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = null)]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "RODNE_CISLO", IsReference = true)]
        public STechnik Technik { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public DateTime Datum { get; set; }

        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis")]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

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
            return Databaza.ZmazSluzbu(IdSluzby).JeChyba;
        }
    }
}