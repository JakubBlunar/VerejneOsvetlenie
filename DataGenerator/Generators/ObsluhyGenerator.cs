using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db.GeneratorHelpers;

namespace DataGenerator.Generators
{
    public class ObsluhyGenerator : Generator
    {
        public List<LampaGeneratorHelper> LampaGeneratorHelpers { get; set; }
        public List<StlpGeneratorHelper> StlpGeneratorHelpers { get; set; }
        public Dictionary<int, DateTime> Sluzby { get; set; }

        public ObsluhyGenerator()
        {
            LampaGeneratorHelpers = _database.LampySelect();
            StlpGeneratorHelpers = _database.StlpySelect();
            Sluzby = _database.GetSluzbyDatumy();
            var i = 0;
        }

        public override void Generate(int paCount)
        {
            double random;

            foreach (var sluzba in Sluzby)
            {
                random = _random.NextDouble();

                if (random > 0.68)
                    ObsluhyStlpu(sluzba.Key, sluzba.Value);
                else
                    ObsluhaLampy(sluzba.Key, sluzba.Value);
            }
        }

        private void ObsluhyStlpu(int paIdSluzby, DateTime paDate)
        {
            var stlpId = RandomStlp(paDate);

            var sql = $"INSERT INTO s_obsluha_stlpu VALUES " +
                      $"('{stlpId}','{paIdSluzby}');";

            using (StreamWriter w = File.AppendText("inserty.txt"))
            {
                w.WriteLine(sql);
            }
        }

        private void ObsluhaLampy(int paIdSluzby, DateTime paDate)
        {
            var lampaId = RandomLampa(paDate);
            var sql = $"INSERT INTO s_obsluha_lampy VALUES " +
                      $"('{lampaId}','{paIdSluzby}');";

            using (StreamWriter w = File.AppendText("inserty.txt"))
            {
                w.WriteLine(sql);
            }
        }

        private int RandomStlp(DateTime paBefore)
        {
            var id = -1;
            while (id == -1)
            {
                var stlp = StlpGeneratorHelpers[_random.Next(0, StlpGeneratorHelpers.Count)];
                if (stlp.DatumInstalacie < paBefore)
                {
                    id = stlp.Cislo;
                }
            }
            return id;
        }

        private int RandomLampa(DateTime paDate)
        {
            var id = -1;
            while (id == -1)
            {
                var lampa = LampaGeneratorHelpers[_random.Next(0, LampaGeneratorHelpers.Count)];
                if (lampa.DatumInstalacie < paDate && (lampa.DatumOdinstalacie == null || lampa.DatumOdinstalacie > paDate))
                {
                    id = lampa.Id;
                }
            }
            return id;
        }


    }
}
