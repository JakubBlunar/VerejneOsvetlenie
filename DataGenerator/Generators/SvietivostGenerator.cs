using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Generators
{
    public class SvietivostGenerator : Generator
    {
        public List<int> Svietivosti { get; set; }
        public List<int> IdLapm { get; set; }
        public List<int> IdSluzby { get; set; }
        public List<char> Stavy { get; set; }

        public SvietivostGenerator()
        {
            Svietivosti = new List<int>();
            IdLapm = new List<int>();
            IdSluzby = new List<int>();
            Stavy = new List<char>();

            var sql = "SELECT sl.svietivost, id_lampy, id_sluzby, sk.stav FROM s_lampa sl JOIN s_lampa_na_stlpe sln USING(id_typu) JOIN s_obsluha_lampy so USING(id_lampy) JOIN s_kontrola sk USING(id_sluzby)";
            var result = _database.SpecialSelect(sql);

            foreach (var row in result)
            {
                IdSluzby.Add(int.Parse(row["ID_SLUZBY"].ToString()));
                IdLapm.Add(int.Parse(row["ID_LAMPY"].ToString()));
                Svietivosti.Add(int.Parse(row["SVIETIVOST"].ToString()));
                Stavy.Add(char.Parse(row["STAV"].ToString()));
            }
        }

        public override void Generate(int paCount)
        {
            string sql;
            var svietivost = 0;
            for (int i = 0; i < IdSluzby.Count; i++)
            {
                if (Stavy[i] == 'Z') continue;
                svietivost = _random.Next(Svietivosti[i] - 300, Svietivosti[i]);
                sql = $"UPDATE s_kontrola SET svietivost = {svietivost} WHERE id_sluzby = {IdSluzby[i]}";
                _database.ExecuteNonQueryOwn(sql);
            }
        }

       
    }
}
