using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class Vysledok
    {
        public bool JeChyba { get; private set; }
        public string Popis { get; set; }

        public Vysledok()
        {
            JeChyba = false;
            Popis = "";
        }
        public void NastavChybu(string pPopis)
        {
            Popis = pPopis;
            JeChyba = true;
        }
    }
}
