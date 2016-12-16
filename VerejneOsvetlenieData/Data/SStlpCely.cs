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
    [SqlClass(TableName = "S_STLP", DisplayName = "Informácie o stĺpe", TableKey = "CISLO")]
    public class SStlpCely
    {
        public Databaza Databaza { get; set; }

        public SStlp SStlp { get; set; }

        //[SqlClass(ColumnName = "DOPLNKY", IsReference = true)]
        public LinkedList<TDoplnok> Doplnky { get; set; }

        public SInfo SInfo { get; set; }

        public SStlpCely(SStlp paSStlp)
        {
            Doplnky = new LinkedList<TDoplnok>();
            Databaza = new Databaza();
            SStlp = paSStlp;
        }

        public bool SelectPodlaId(object paIdEntity)
        {
            SStlp.SelectPodlaId(paIdEntity);
            var select =
                $"sd.ID, sd.TYP_DOPLNKU, sd.POPIS, sd.DATUM_INSTALACIE, sd.DATUM_DEMONTAZE from s_stlp s, table(s.doplnky) sd where s.cislo = {SStlp.Cislo}";
            var rows = Databaza.SpecialSelect(select);
            foreach (var row in rows)
            {
                var doplnok = new TDoplnok
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Popis = row["POPIS"].ToString(),
                    DatumDemontaze = DateTime.Parse(row["DATUM_DEMONTAZE"].ToString()),
                    DatumInstalacie = DateTime.Parse(row["DATUM_INSTALACIE"].ToString()),
                    TypDoplnku = row["TYP"].ToString()[0]
                };
                Doplnky.AddLast(doplnok);
            }

            return true;
        }
    }
}
