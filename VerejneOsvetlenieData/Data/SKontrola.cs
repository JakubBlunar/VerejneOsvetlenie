using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_KONTROLA", DisplayName = "Kontrola")]
    public class SKontrola
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "STAV")]
        public string Stav { get; set; }
        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivosť")]
        public string Svietivost { get; set; }
    }

    [SqlClass(TableName = "S_LAMPA_NA_STLPE", DisplayName = "lampa na stĺpe")]
    public class SLampaNaStlpe
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
        public string IdLampy { get; set; }
        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public string IdTypu { get; set; }
        [SqlClass(ColumnName = "STAV")]
        public string Stav { get; set; }
        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "")]
        public string DatumInstalacie { get; set; }
        [SqlClass(ColumnName = "DATUM_DEMONTAZE", DisplayName = "")]
        public string DatumDemontaze { get; set; }
    }

    [SqlClass(TableName = "S_INFO", DisplayName = "info o stĺpe")]
    public class SInfo
    {
        [SqlClass(ColumnName = "ID", DisplayName = null)]
        public string Id { get; set; }
        [SqlClass(ColumnName = "CISLO", DisplayName = null, IsBitmapImage = true)]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "DATA", DisplayName = "obrázok", IsBitmapImage = true)]
        public FileStream Data { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "DATUM", DisplayName = "dátum")]
        public string Datum { get; set; }
    }

    [SqlClass(TableName = "S_SLUZBA", DisplayName = "")]
    public class SSluzba
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = null)]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "DATUM", DisplayName = "dátum")]
        public string Datum { get; set; }
        [SqlClass(ColumnName = "POPIS")]
        public string Popis { get; set; }
        [SqlClass(ColumnName = "TRVANIE")]
        public string Trvanie { get; set; }
    }

    [SqlClass(TableName = "S_TECHNIK", DisplayName = "Technik")]
    public class STechnik
    {
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "")]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "MENO")]
        public string Meno { get; set; }
        [SqlClass(ColumnName = "PRIEZVISKO")]
        public string Priezvisko { get; set; }
    }

    [SqlClass(TableName = "S_LAMPA", DisplayName = "")]
    public class SLampa
    {
        [SqlClass(ColumnName = "ID_TYPU", DisplayName = null)]
        public string IdTypu { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "svietivosť")]
        public string Svietivost { get; set; }
    }

    [SqlClass(TableName = "S_ULICA", DisplayName = "")]
    public class SUlica
    {
        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public string IdUlice { get; set; }
        [SqlClass(ColumnName = "NAZOV", DisplayName = "názov")]
        public string Nazov { get; set; }
        [SqlClass(ColumnName = "MESTO")]
        public string Mesto { get; set; }
    }

    [SqlClass(TableName = "S_STLP", DisplayName = "Stĺp")]
    public class SStlp
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "ID_ULICE", DisplayName = null)]
        public string IdUlice { get; set; }
        [SqlClass(ColumnName = "VYSKA", DisplayName = "výška")]
        public string Vyska { get; set; }
        [SqlClass(ColumnName = "PORADIE")]
        public string Poradie { get; set; }
        [SqlClass(ColumnName = "DATUM_INSTALACIE", DisplayName = "dátum inštalácie")]
        public string DatumInstalacie { get; set; }
        [SqlClass(ColumnName = "TYP")]
        public string Typ { get; set; }
        [SqlClass(ColumnName = "DOPLNKY")]
        public string Doplnky { get; set; }
    }

    [SqlClass(TableName = "S_OBSLUHA_STLPU", DisplayName = "")]
    public class SObsluhaStlpu
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = "")]
        public string Cislo { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = "")]
        public string IdSluzby { get; set; }
    }

    [SqlClass(TableName = "S_SERVIS", DisplayName = "Servis")]
    public class SServis
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "CENA")]
        public string Cena { get; set; }
    }

    [SqlClass(TableName = "S_OBSLUHA_LAMPY", DisplayName = null)]
    public class SObsluhaLampy
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = null)]
        public string IdLampy { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
    }


}
