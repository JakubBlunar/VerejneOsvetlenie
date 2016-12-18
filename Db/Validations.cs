using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class Validations
    {
        public static bool ValidRC(string rodneCislo)
        {
            if (rodneCislo.Length != 10)
                return false;

            //try { int.Parse(rodneCislo); }
            //catch (FormatException) { return false; }

            try
            {
                string date =
                      rodneCislo.Substring(4, 2) + "."
                    + int.Parse(rodneCislo.Substring(2, 2)) % 50 + "."
                    + rodneCislo.Substring(0, 2);
                DateTime.Parse(date);
            }
            catch (FormatException) { return false; }

            return true;
        }
    }
}
