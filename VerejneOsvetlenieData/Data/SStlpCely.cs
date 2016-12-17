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

        public SStlpCely(SStlp paSStlp)
        {
            Doplnky = new LinkedList<TDoplnok>();
            Databaza = new Databaza();
            SStlp = paSStlp;
            SInformacie = new LinkedList<SInfo>();
        }

        public bool SelectPodlaId(object paIdEntity)
        {
            //SStlp.SelectPodlaId(paIdEntity);
            var select =
                $"select sd.ID, sd.TYP_DOPLNKU, sd.POPIS, to_char(sd.DATUM_INSTALACIE, 'DD.MM. YYYY') as DATUM_INSTALACIE, to_char(sd.DATUM_DEMONTAZE, 'DD.MM.YYYY') as DATUM_DEMONTAZE from s_stlp s, table(s.doplnky) sd where s.cislo = {SStlp.Cislo}";
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
                    Data = (byte[]) informacia["DATA"],
                    Typ = informacia["TYP"].ToString()[0]
                };
                //info.NastavObrazok();
                SInformacie.AddLast(info);
            }

            return true;
        }
    }
}
