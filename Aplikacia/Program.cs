using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace Aplikacia
{
    class Program
    {
        static void Main(string[] args)
        {
            Databaza db = new Databaza();
            List<string> osoby = db.GetOsoby();

            foreach (var o in osoby)
            {
                Console.WriteLine(o);
            }

            Console.ReadLine();
        }
    }
}
