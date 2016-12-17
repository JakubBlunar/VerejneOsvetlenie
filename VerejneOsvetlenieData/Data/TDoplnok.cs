using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "T_DOPLNKY", DisplayName = "doplnok", TableKey = "Id")]
    public class TDoplnok : SqlEntita
    {
        [SqlClass(ColumnName = "ID", DisplayName = null)]
        public int Id { get; set; }

        [SqlClass(ColumnName = "TYP_DOPLNKU", DisplayName = "typ doplnku", Length = 1)]
        public char TypDoplnku { get; set; }

        [SqlClass(ColumnName = "POPIS", Length = 500)]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public int Cislo { get; set; }

        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "dátum inštalácie", SpecialFormat = "d")]
        public string DatumInstalacie { get; set; }

        [SqlClass(ColumnName = "DATUM_DEMONTAZE", DisplayName = "dátum demontáže", SpecialFormat = "d")]
        public string DatumDemontaze { get; set; }

        public TDoplnok()
        {
            Cislo = -1;
        }

        public override bool Update()
        {
            if (string.IsNullOrEmpty(DatumDemontaze))
                return useDbMethod(Databaza.UpdateDoplnokStlpu(Cislo, Id, TypDoplnku, Popis, DateTime.Parse(DatumInstalacie)));
            return useDbMethod(Databaza.UpdateDoplnokStlpu(Cislo, Id, TypDoplnku, Popis, DateTime.Parse(DatumInstalacie), DateTime.Parse(DatumDemontaze)));
        }

        public override bool Insert()
        {
            if (string.IsNullOrEmpty(DatumDemontaze))
                return useDbMethod(Databaza.VlozDoplnokStlpu(Cislo,TypDoplnku, Popis, DateTime.Parse(DatumInstalacie)));
            return useDbMethod(Databaza.VlozDoplnokStlpu(Cislo, TypDoplnku, Popis, DateTime.Parse(DatumInstalacie), DateTime.Parse(DatumDemontaze)));
        }

        public override bool Drop()
        {
            return useDbMethod(Databaza.ZmazDoplnokStlpu(Id, Cislo));
        }
    }
}
