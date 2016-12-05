using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator.Data;
using Db;

namespace DataGenerator.Generators
{
    public class DoplnokGenerator : Generator
    {
        private static readonly string[] Obchody = new[]
        {
            "BILLA", "Deichman", "Max", "LIDL", "Coop jednota", "McDonald", "Carrefour", "Okey", "Mirrage", "Aupark",
            "Aquapark", "Koruna", "Tempo", "Benzínová stanica"
        };
        private const string ReklamaPopis = "{0} {1} metrov {2}.";
        private static readonly string[] Smer = new[] { "vpravo", "vlavo", "rovno" };

        private static readonly string[] Vzdialenosti = new[]
        {"100", "200", "300", "400", "500", "600", "700", "800", "900"};

        private static readonly string[] RychlostneObmedzenie = new[] { "30", "40", "50", "70", "80", "100" };

        private readonly List<string> _znacky;

        public DoplnokGenerator()
        {
            _znacky = DataReader.ReadNonOptimalized("znacky.txt");
        }

        public object GetRandomValue(IList paArray)
        {
            return paArray[_random.Next() % paArray.Count];
        }

        public string GenerujReklamu()
        {
            return string.Format(ReklamaPopis, GetRandomValue(Obchody), GetRandomValue(Vzdialenosti), GetRandomValue(Smer));
        }

        public string GenerujZnacku()
        {
            return GetRandomValue(_znacky).ToString();
        }

        public string GenerujRychlostneObmedzenie()
        {
            return $"Rychlostne obmedzenie {GetRandomValue(RychlostneObmedzenie)}km/h";
        }

        /// <summary>
        /// Pocet stĺpov na ktoré je treba generovať generuje sa od 1-2 doplnky 
        /// na stĺpe a ako count sa berie Min(pocet stĺpov, paCount)
        /// </summary>
        /// <param name="paCount"></param>
        public override void Generate(int paCount)
        {
            var db = new Databaza();
            var reader = db.SpecialSelect("select cislo, to_char(DATUM_INSTALACIE, 'DD.MM.YYYY') as datum from s_stlp where DATUM_INSTALACIE is not null");

            var count = 0;

            const char znacka = 'Z';
            const char reklama = 'R';
            const char obmedzenie = 'O';

            foreach (var row in reader)
            {
                var idStlpu = int.Parse(row["CISLO"].ToString());
                var datumInstalacieStlpa = DateTime.Parse(row["DATUM"].ToString());
                var prvyDatumInstalacieDoplnku = RandomDay(datumInstalacieStlpa, DateTime.Now);
                var druhyDatumInstalacieDoplnku = RandomDay(prvyDatumInstalacieDoplnku, DateTime.Now);

                var result = _random.Next() % 2;
                if (result == 0)
                {
                    db.VlozDoplnokStlpu(idStlpu, reklama, GenerujReklamu(), prvyDatumInstalacieDoplnku);
                    if (_random.Next() % 2 == 0)
                        db.VlozDoplnokStlpu(idStlpu, znacka, GenerujZnacku(), druhyDatumInstalacieDoplnku);
                }
                else if (result == 1)
                {
                    db.VlozDoplnokStlpu(idStlpu, znacka, GenerujZnacku(), prvyDatumInstalacieDoplnku);
                    if (_random.Next() % 2 == 0)
                        db.VlozDoplnokStlpu(idStlpu, reklama, GenerujReklamu(), druhyDatumInstalacieDoplnku);
                }
                else
                {
                    db.VlozDoplnokStlpu(idStlpu, obmedzenie, GenerujRychlostneObmedzenie(), prvyDatumInstalacieDoplnku);
                }

                if (++count == paCount)
                    break;
            }
        }

        public void PridajDatumyDemontaze()
        {
            var db = new Databaza();
            var reader = db.SpecialSelect("select cislo, to_char(DATUM_INSTALACIE, 'DD.MM.YYYY') as datum from s_stlp where DATUM_INSTALACIE is not null");

            foreach (var row in reader)
            {
                if (_random.Next() % 2 == 0)
                {
                    var idStlpu = int.Parse(row["CISLO"].ToString());
                    var datumInstalacieStlpa = DateTime.Parse(row["DATUM"].ToString());
                }
            }
        }
    }
}
