using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Generators
{
    public class DoplnenieLampGenerator : Generator
    {
        public override void Generate(int paCount)
        {
            var stlpy = _database.SpecialSelect("SELECT cislo from s_stlp WHERE svietivost_stlpu(cislo) = 0");
            var maxId = _database.SpecialSelect("SELECT max(id_lampy) as M FROM s_lampa_na_stlpe");
            var intMaxId = 0;
            foreach (var id in maxId)
            {
                intMaxId = int.Parse(id["M"].ToString());
            }

            var sql = string.Empty;
            foreach (var stlp in stlpy)
            {
                if (_random.NextDouble() > 0.18)
                {
                    intMaxId++;
                    sql = $"INSERT INTO s_lampa_na_stlpe VALUES({intMaxId}, {int.Parse(stlp["CISLO"].ToString())}," +
                      $"{_random.Next(0, 9)},'S', '{RandomDay(new DateTime(2000, 1, 1)).ToShortDateString()}',NULL)";
                    _database.ExecuteNonQueryOwn(sql);
                }
            }
            var i = 0;
        }
    }
}
