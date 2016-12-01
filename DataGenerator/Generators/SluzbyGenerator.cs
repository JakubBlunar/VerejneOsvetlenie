using System;
using DataGenerator.Data;

namespace DataGenerator.Generators
{
    public class SluzbyGenerator : Generator
    {
        public override void Generate(int paCount)
        {
            var technici = _database.GetTechnici();
            var ipsum = DataReader.Read(DataReader.LoreIps);
            string rodCis;
            string datum;
            string popis;
            for (int i = 0; i < paCount; i++)
            {
                rodCis = technici[_random.Next(0, technici.Count)];
                datum = RandomDay().ToShortDateString();
                popis = ipsum[_random.Next(ipsum.Count)];
                var trvanie = _random.Next(10, 200);
                var sql = $"INSERT INTO s_sluzba VALUES " +
                      $"('{i}','{rodCis}','{datum}','{popis}','{trvanie}')";
                Write(sql);
            }
        }

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(1981, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
