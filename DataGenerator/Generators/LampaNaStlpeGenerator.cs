using System;

namespace DataGenerator.Generators
{ 
    public class LampaNaStlpeGenerator: Generator
    {
        public int MaxIdLampy { get;}
        public int MaxIdStlpu { get;}

        public LampaNaStlpeGenerator(int paMaxIdLampy, int paMaxIdStlpu)
        {
            MaxIdLampy = paMaxIdLampy;
            MaxIdStlpu = paMaxIdStlpu;
        }

        public override void Generate(int paCount)
        {
            var idTypu = 0;
            var pocet = 0;
            var id = 0;
            string datupMintaze;
            string datumDemontaze;
            DateTime stlpPostavenie;
            char stav;

            var datumyStlpov = _database.GetLampaDatum();
            for (int idStlpu = 0; idStlpu <= MaxIdStlpu; idStlpu++)
            {
                if (_random.Next(0, 100) > 88) continue;
                idTypu = _random.Next(0, MaxIdLampy + 1);
                pocet = _random.Next(0, 10);
                if (datumyStlpov[idStlpu] == null) continue;
                stlpPostavenie = DateTime.Parse(datumyStlpov[idStlpu]);
                if(stlpPostavenie > DateTime.Today.AddDays(-10)) continue;
                for (int i = 0; i < pocet; i++)
                {
                    if (_random.Next(0, 6) > 3)
                    {
                        stav = 'N';
                    }
                    else
                    {
                        stav = 'S';
                    }
                    datupMintaze = RandomDay(stlpPostavenie).ToShortDateString();
                    if (_random.Next(0, 10) > 6)
                    {
                        datumDemontaze = RandomDay(DateTime.Parse(datupMintaze)).ToShortDateString();
                    }
                    else
                    {
                        datumDemontaze = "NULL";
                    }

                    _database.InsertLampaNaStlpe(id, idStlpu, idTypu, stav, datupMintaze, datumDemontaze);

                    id++;
                }
            }
        }
    }
}
