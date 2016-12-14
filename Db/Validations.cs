using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class Validations
    {
        public static bool ValidRC(string rodneCislo) {
            if (rodneCislo.Length != 10)
                return false;

            try { int.Parse(rodneCislo); }
            catch (FormatException) { return false; }

            string date = 
                  rodneCislo.Substring(4, 2) + "."
                + rodneCislo.Substring(2, 2) + "." 
                + rodneCislo.Substring(0, 2);
            try { DateTime.Parse(date); }
            catch (FormatException) { return false; }

            return true;    
        }
    }
}
