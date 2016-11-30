using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator.Data;

namespace DataGenerator.Generators
{
    public class UlicaGenerator : Generator
    {
        private List<string> _mesta;
        private List<string> _ulice;

        public UlicaGenerator()
        {
            _mesta = DataReader.Read(DataReader.Cities);
            _ulice = DataReader.Read(DataReader.Ulice);
        }

        public override void Generate(int paCount)
        {
            var ulica = string.Empty;
            var pouzite = new List<string>();
            int id = 0;
            foreach (var mesto in _mesta)
            {
                pouzite.Clear();
                for (int i = 0; i < paCount; i++)
                {
                    ulica = _ulice[_random.Next(0, _ulice.Count)];
                    if(pouzite.Contains(ulica)) continue;
                    pouzite.Add(ulica);
                    _database.InsertUlica(id, ulica, mesto);
                    id++;
                }
            }
        }
    }
}
