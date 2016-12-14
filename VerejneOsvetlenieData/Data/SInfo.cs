using System;
using System.IO;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_INFO", DisplayName = "info o ståpe", TableKey = "id")]
    public class SInfo : SqlEntita
    {
        [SqlClass(ColumnName = "ID", DisplayName = null)]
        public int Id { get; set; }

        [SqlClass(ColumnName = "CISLO", DisplayName = null, IsBitmapImage = true)]
        public int Cislo { get; set; }

        [SqlClass(ColumnName = "DATA", DisplayName = "obrázok", IsBitmapImage = true)]
        public MemoryStream Data { get; set; }

        [SqlClass(ColumnName = "TYP")]
        public char Typ { get; set; }

        ~SInfo()
        {
            Data?.Dispose();
        }

        //[SqlClass(ColumnName = "DATUM", DisplayName = "dátum")]
        //public DateTime Datum { get; set; }

        public override bool Update()
        {
            return !Databaza.UpdateInfoStlpu(Id, Cislo, Typ, Data.ToArray()).JeChyba;
        }

        public override bool Insert()
        {
            return !Databaza.VlozInfoStlpu(Cislo, Typ, Data.ToArray()).JeChyba;
        }

        public override bool Drop()
        {
            return !Databaza.ZmazInfoOStlpe(Id).JeChyba;
        }
    }
}