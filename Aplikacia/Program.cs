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

            Vysledok res = db.VlozKontroluStlpu("9405248677", -5, "asdad", "D", 5, DateTime.Now);
            if (res.JeChyba)
                Console.WriteLine("error");
            Console.WriteLine(res.Popis);
      
            
            Console.ReadLine();
        }
    }
}
