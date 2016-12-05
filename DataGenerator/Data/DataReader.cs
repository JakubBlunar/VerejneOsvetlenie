using System.Collections.Generic;
using System.IO;

namespace DataGenerator.Data
{
    public class DataReader
    {
        public const string Names = "Names.txt";
        public const string Cities = "Cities.txt";
        public const string Surna = "Surnames.txt";
        public const string Rcs = "rcs.txt";
        public const string Ulice = "ulice.txt";
        public const string LoreIps = "LorIps.txt";
        public const string Znacky = "znacky.txt";

        public static List<string> Read(string paFileName)
        {
            List<string> list;
            try
            {   // Open the text file using a stream reader.
                using (var sr = new StreamReader("../../Data/" + paFileName))
                {
                    var noStr = int.Parse(sr.ReadLine());
                    list = new List<string>(noStr);

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                return list;
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public static List<string> ReadNonOptimalized(string paFileName)
        {
            List<string> list;
            try
            {   // Open the text file using a stream reader.
                using (var sr = new StreamReader("../../Data/" + paFileName))
                {
                    list = new List<string>();

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                return list;
            }
            catch
            {
                // ignored
            }
            return null;
        }
    }
}
