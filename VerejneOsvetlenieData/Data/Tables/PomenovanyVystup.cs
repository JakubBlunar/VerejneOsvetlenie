using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace VerejneOsvetlenieData.Data.Tables
{
    [ImplementPropertyChanged]
    public class PomenovanyVystup : Select
    {
        public string Nazov { get; set; }

        public PomenovanyVystup(string paNazov, string paSelectString, params string[] paCollumnNames) : base(paSelectString, paCollumnNames)
        {
            Nazov = paNazov;
        }

        public PomenovanyVystup(string paSelectString, params string[] paCollumnNames) : base(paSelectString, paCollumnNames)
        {
            Nazov = string.Empty;
        }

        public override string ToString()
        {
            return Nazov;
        }
    }
}
