using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Generators
{
    public class ServisKontrolaGenerator : Generator
    {
        public override void Generate(int paCount)
        {
            double random;

            for (int i = 0; i < paCount; i++)
            {
                random = _random.NextDouble();

                if (random >= 0.5)
                    VytvorKontrolu(i);
                else
                    VytvorServis(i);
            }
        }

        private void VytvorServis(int paId)
        {
            char stav;

            if (_random.Next(0, 6) > 3)
            {
                stav = 'D';
            }
            else
            {
                stav = 'Z';
            }

            var sql = $"INSERT INTO s_kontrola (id_sluzby,stav) VALUES " +
                      $"('{paId}','{stav}');";

            using (StreamWriter w = File.AppendText("inserty.txt"))
            {
                w.WriteLine(sql);
            }
        }

        private void VytvorKontrolu(int paId)
        {
            var trvanie = _random.Next(10, 200);
            var cena = _random.Next(2, 5000);

            var sql = $"INSERT INTO s_servis VALUES " +
                      $"('{paId}','{trvanie}','{cena}');";

            using (StreamWriter w = File.AppendText("inserty.txt"))
            {
                w.WriteLine(sql);
            }
        }
    }
}
