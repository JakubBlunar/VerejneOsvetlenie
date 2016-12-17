using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Db;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    public class SUlicaCela
    {
        public Databaza Databaza { get; set; }

        public SUlica SUlica { get; set; }

        public LinkedList<SStlp> Stlpy { get; set; }

        public SUlicaCela(SUlica sulica)
        {
            SUlica = sulica;
            Databaza = new Databaza();
            Stlpy = new LinkedList<SStlp>();
        }

        public bool SelectPodlaId(object paIdEntity)
        {
            string select =
                $"SELECT cislo FROM s_stlp WHERE id_ulice = {SUlica.IdUlice} ORDER BY poradie";
            var rows = Databaza.SpecialSelect(select);
            foreach (var row in rows)
            {
                var stlp = new SStlp();
                stlp.SelectPodlaId(int.Parse(row["CISLO"].ToString()));
                Stlpy.AddLast(stlp);
            }
            return true;
        }
    }
}
