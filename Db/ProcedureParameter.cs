using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Db
{
    [ImplementPropertyChanged]
    public class ProcedureParameter
    {
        public string NazovParametra { get; set; }
        public string DbNazovTypu { get; set; }
        public object HodnotaParametra { get; set; }

        public ProcedureParameter(string paNamzovParametra, string paDbNazovTypu, object paHodnotaParametra)
        {
            this.NazovParametra = paNamzovParametra;
            this.DbNazovTypu = paDbNazovTypu;
            this.HodnotaParametra = paHodnotaParametra;
        }
    }
}
