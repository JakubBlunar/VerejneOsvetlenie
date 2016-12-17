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

        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "Technik", Length = 10)]
        public string RodneCislo { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public DateTime Datum { get; set; }

        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis", Length = 500)]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

        [SqlClass(ColumnName = "STAV", DisplayName = "Stav")]
        public char Stav { get; set; }

        [SqlClass(ColumnName = "SVIETIVOST", DisplayName = "Svietivost")]
        public int Svietivost { get; set; }

        public SKontrolaLampy()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            return UseDbMethod(Databaza.UpdateKontrolyLampy(IdSluzby, RodneCislo, IdLampy, Popis, Stav,
                    Trvanie, Datum, Svietivost));
        }

        public override bool Insert()
        {
            return UseDbMethod(Databaza.VlozKontroluLampy(RodneCislo, IdLampy, Popis, Stav,
                    Trvanie, Datum, Svietivost));
        }

        public override bool Drop()
        {
            return UseDbMethod(Databaza.ZmazSluzbu(IdSluzby));
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            string s = "select id_lampy, rodne_cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy hh24:mi'), nvl(popis,''), trvanie, stav, nvl(svietivost, 0) from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) where id_sluzby = " + paIdEntity;
            var select = new VystupSelect(s,
                "id_lampy", "rodne_cislo", "id_sluzby", "datum", "popis", "trvanie", "stav", "svietivost");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                IdLampy = int.Parse(row[0].ToString());
                RodneCislo = row[1].ToString();
                IdSluzby = int.Parse(row[2].ToString());
                Datum = DateTime.Parse(row[3].ToString());
                Popis = row[4].ToString();
                Trvanie = int.Parse(row[5].ToString());
                Stav = row[6].ToString()[0];//.ToCharArray()[0];
                Svietivost = int.Parse(row[7].ToString());
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
