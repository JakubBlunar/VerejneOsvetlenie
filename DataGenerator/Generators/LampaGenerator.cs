using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Generators
{
    public class LampaGenerator : Generator
    {
        public override void Generate(int paCount)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char ch;
            int svietiv;
            for (int i = 0; i < paCount; i++)
            {
                ch = chars[i];
                svietiv = _random.Next(1000, 25000);
                _database.InsertLampa(i, ch, svietiv);
            }
        }
    }
}
