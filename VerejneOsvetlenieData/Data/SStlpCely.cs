using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_STLP", DisplayName = "Informácie o stĺpe", TableKey = "CISLO")]
    public class SStlpCely
    {
        [SqlClass(ColumnName = "CISLO")]
        public int Cislo { get; set; }


        public int IdUlice { get; set; }

        public int Vyska { get; set; }

        public int Poradie { get; set; }

        public DateTime DatumInstalacie { get; set; }

        public char Typ { get; set; }

        public LinkedList<Doplnok> Doplnky { get; set; }
    }
}
