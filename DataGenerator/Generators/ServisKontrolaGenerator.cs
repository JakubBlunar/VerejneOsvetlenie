using System;
using System.Collections.Generic;
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

        }

        private void VytvorKontrolu(int paId)
        {

        }
    }
}
