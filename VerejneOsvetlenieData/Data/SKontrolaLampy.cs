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
    [SqlClass(TableName = "", DisplayName = "Kontrola Lampy", TableKey = "ID_SLUZBY")]
    public class SKontrolaLampy:SqlEntita
    {
        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = "Id Lampy", ReadOnly = true)]
        public int IdLampy { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public DateTime Datum { get; set; }


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

        public override bool SelectPodlaId(object paIdEntity)
        {
            string s = "select id_lampy, id_sluzby, to_char(datum, 'dd.mm.yyyy hh24:mi'), nvl(popis,''), trvanie, stav, nvl(svietivost, 0) from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) where id_sluzby = " + paIdEntity;
            var select = new VystupSelect(s,
                "cislo", "id_sluzby", "datum", "popis", "trvanie", "stav", "svietivost");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                IdLampy = int.Parse(row[0].ToString());
                IdSluzby = int.Parse(row[1].ToString());
                Datum = DateTime.Parse(row[2].ToString());
                Popis = row[3].ToString();
                Trvanie = int.Parse(row[4].ToString());
                Stav = row[5].ToString();
                Svietivost = int.Parse(row[6].ToString());
                return true;
            }
            return false;
        }

        public override IVystup GetSelectOnTableData()
        {
            string s = "select id_lampy, id_sluzby, to_char(datum, 'dd.mm.yyyy'), nvl(popis,''), trvanie, stav, nvl(svietivost, 0) from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby)  order by id_sluzby desc";
            var select = new VystupSelect(s,
                "cislo", "id_sluzby", "datum", "popis", "trvanie", "stav","svietivost");
            select.KlucovyStlpec = "ID_SLUZBY";
            return select;

        }
    }
}
