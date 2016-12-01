using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Generators
{
    public class StlpGenerator : Generator
    {
        public int MaxIdUlice { get; }

        public StlpGenerator(int paMaxIdUlice)
        {
            MaxIdUlice = paMaxIdUlice;
        }

        public override void Generate(int paCount)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int cislo = 0;
            int vyska;
            string date;
            char typ;
            for (int i = 0; i <= MaxIdUlice; i++)
            {
                if (_random.Next(0, 100) > 90) continue;
                
                for (int j = 0; j < paCount; j++)
                {
                    vyska = _random.Next(250, 1000);
                    date = RandomDay().ToShortDateString();
                    typ = chars[_random.Next(2, 10)];

                    var sql = $"INSERT INTO s_stlp (cislo, id_ulice, vyska, poradie, datum_instalacie, typ) VALUES " +
                      $"('{cislo}','{i}','{vyska}','{j}'," +
                      $"TO_DATE('{date}','dd.mm.yyyy'),'{typ}')";

                    Write(sql);
                    cislo++;
                }
            }
        }

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(1980, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
