using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace VerejneOsvetlenieData.Data.Tables
{
    [ImplementPropertyChanged]
    public class PomenovanyVystup
    {
        public string Nazov { get; set; }
        public IVystup Vystup { get; set; }

        public PomenovanyVystup(string paNazov, IVystup paVystup)
        {
            Nazov = paNazov;
            Vystup = paVystup;
        }

        public override string ToString()
        {
            return Nazov;
        }
    }
}
