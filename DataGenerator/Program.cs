using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator.Generators;

namespace DataGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var tg = new TechikGenerator();
            tg.Generate(498);
        }
    }
}
