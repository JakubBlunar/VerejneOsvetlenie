using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;
using VerejneOsvetlenieData.Data.Tables;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "", DisplayName = "Servis stlpu", TableKey = "ID_SLUZBY")]
    public class SServisStlpu:SqlEntita
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = "Číslo stlpu")]
        public int Cislo { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public string Datum { get; set; }


        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis")]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

        [SqlClass(ColumnName = "Cena", DisplayName = "Stav")]
        public int Cena { get; set; }

        public override bool Update()
        {
            throw new NotImplementedException();
        }

        public override bool Insert()
        {
            throw new NotImplementedException();
        }

        public override bool Drop()
        {
            throw new NotImplementedException();
        }

        public override IVystup GetSelectOnTableData()
        {
            string s = "select cislo, id_sluzby, datum, nvl(popis,''), trvanie, cena from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_servis using (id_sluzby)";
            var select = new VystupSelect(s,
                "cislo stlpu", "id_sluzby", "datum", "popis", "trvanie", "cena");
            return select;

        }
    }
}
