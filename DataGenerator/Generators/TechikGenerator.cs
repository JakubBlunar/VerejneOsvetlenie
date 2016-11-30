using System.Collections.Generic;
using DataGenerator.Data;

namespace DataGenerator.Generators
{
    public class TechikGenerator : Generator
    {
        private List<string> _names;
        private List<string> _surnames;
        private List<string> _rcs;

        public TechikGenerator()
        {
            _names = DataReader.Read(DataReader.Names);
            _surnames = DataReader.Read(DataReader.Surna);
            _rcs = DataReader.Read(DataReader.Rcs);

        }
        
        public override void Generate(int paCount)
        {
            var namesCount = _names.Count;
            var surnamesCount = _surnames.Count;
            var rc = string.Empty;
            var name = string.Empty;
            var surname = string.Empty;

            for (int i = 0; i < paCount; i++)
            {
                rc = _rcs[i];
                name = _names[_random.Next(0, namesCount)];
                surname = _surnames[_random.Next(0, surnamesCount)];

                _database.InsertTechnik(rc, name, surname);
            }
        }
    }
}