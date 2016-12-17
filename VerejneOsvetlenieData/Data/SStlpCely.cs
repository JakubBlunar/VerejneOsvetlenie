using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    //[SqlClass(TableName = "S_STLP", DisplayName = "Informácie o stĺpe", TableKey = "CISLO")]
    public class SStlpCely
    {
        public Databaza Databaza { get; set; }

        public SStlp SStlp { get; set; }

        //[SqlClass(ColumnName = "DOPLNKY", IsReference = true)]
        public LinkedList<TDoplnok> Doplnky { get; set; }

        public LinkedList<SInfo> SInformacie { get; set; }

        public LinkedList<SLampaNaStlpe> SLampyNaStlpe { get; set; }

        public SStlpCely(SStlp paSStlp)
        {
            Doplnky = new LinkedList<TDoplnok>();
            Databaza = new Databaza();
            SStlp = paSStlp;
            SInformacie = new LinkedList<SInfo>();
            SLampyNaStlpe = new LinkedList<SLampaNaStlpe>();
        }

        public bool SelectPodlaId(object paIdEntity)
        {
            //SStlp.SelectPodlaId(paIdEntity);
            var select =
                $"select sd.ID, sd.TYP_DOPLNKU, sd.POPIS, to_char(sd.DATUM_INSTALACIE, 'DD.MM.YYYY') as DATUM_INSTALACIE, to_char(sd.DATUM_DEMONTAZE, 'DD.MM.YYYY') as DATUM_DEMONTAZE from s_stlp s, table(s.doplnky) sd where s.cislo = {SStlp.Cislo}";
            var rows = Databaza.SpecialSelect(select);
            foreach (var row in rows)
            {
                var doplnok = new TDoplnok
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Popis = row["POPIS"].ToString(),
                    DatumDemontaze = row["DATUM_DEMONTAZE"].ToString(),
                    DatumInstalacie = row["DATUM_INSTALACIE"].ToString(),
                    TypDoplnku = row["TYP_DOPLNKU"].ToString()[0],
                    Cislo = SStlp.Cislo
                };
                Doplnky.AddLast(doplnok);
            }

            var selectInfo = $"select * from s_info where cislo = {SStlp.Cislo}";
            var informacie = Databaza.SpecialSelect(selectInfo);

            foreach (var informacia in informacie)
            {
                var info = new SInfo
                {
                    Id = int.Parse(informacia["ID"].ToString()),
                    Cislo = int.Parse(informacia["CISLO"].ToString()),
                    Data = (byte[])informacia["DATA"],
                    Typ = informacia["TYP"].ToString()[0]
                };
                DateTime d;
                info.Datum = DateTime.TryParse(informacia["DATUM"].ToString(), out d) ? d.ToString("dd.MM.yyyy") : string.Empty;
                SInformacie.AddLast(info);
            }

            var selectLampy = $"select * from s_lampa_na_stlpe where cislo = {SStlp.Cislo}";
            var lampy = Databaza.SpecialSelect(selectLampy);
            foreach (var lampaNaStlpe in lampy)
            {
                var lampa = new SLampaNaStlpe()
                {
                    IdLampy = int.Parse(lampaNaStlpe["ID_LAMPY"].ToString()),
                    Cislo = int.Parse(lampaNaStlpe["CISLO"].ToString()),
                    IdTypu = int.Parse(lampaNaStlpe["ID_TYPU"].ToString()),
                    Stav = lampaNaStlpe["STAV"].ToString()[0],
                    DatumInstalacie = DateTime.Parse(lampaNaStlpe["DATUM_INSTALACIE"].ToString()).ToString("DD.MM.YYYY"),
                };
                DateTime dod;
                lampa.DatumDemontaze = DateTime.TryParse(lampaNaStlpe["DATUM_DEMONTAZE"].ToString(), out dod) ? dod.ToString("DD.MM.YYYY") : string.Empty;
                SLampyNaStlpe.AddLast(lampa);
            }

            return true;
        }
    }
}
