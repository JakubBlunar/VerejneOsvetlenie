﻿using System;
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
    [SqlClass(TableName = "", DisplayName = "Kontrola Lampy", TableKey = "ID_SLUZBY")]
    public class SKontrolaLampy:SqlEntita
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = "Id Lampy")]
        public int IdLampy { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public string Datum { get; set; }


        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis")]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

        [SqlClass(ColumnName = "STAV", DisplayName = "Stav")]
        public string Stav { get; set; }

        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivost")]
        public int Svietivost { get; set; }

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
            string s = "select id_lampy, id_sluzby, datum, nvl(popis,''), trvanie, stav, svietivost from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby)";
            var select = new VystupSelect(s,
                "cislo", "id_sluzby", "datum", "popis", "trvanie", "stav","svietivost");
            return select;

        }
    }
}
