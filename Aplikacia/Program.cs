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
            Console.WriteLine(db.UpdateTechnik("9405248677","Jakub", "Blunar"));
            
            Console.ReadLine();
        }
    }
}
