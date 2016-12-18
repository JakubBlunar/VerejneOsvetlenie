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
    [SqlClass(TableName = "", DisplayName = "Servis lampy", TableKey = "ID_SLUZBY")]
    public class SServisLampy : SqlEntita
    {

        [SqlClass(ColumnName = "ID_LAMPY", DisplayName = "Id Lampy", ReadOnly = true)]
        public int IdLampy { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "Technik", Length = 10)]
        public string RodneCislo { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum", IsDate = true)]
        public DateTime Datum { get; set; }

        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis", Length = 500)]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

        [SqlClass(ColumnName = "CENA", DisplayName = "Cena")]
        public int Cena { get; set; }

        public SServisLampy()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            return UseDbMethod(Databaza.UpdateServisuLampy(IdSluzby, RodneCislo, IdLampy, Popis,
                Trvanie, Datum, Cena));
        }

        public override bool Insert()
        {
            return UseDbMethod(Databaza.VlozServisLampy(RodneCislo, IdLampy, Popis,
                   Trvanie, Datum, Cena));
        }

        public override bool Drop()
        {
            return UseDbMethod(Databaza.ZmazSluzbu(IdSluzby));
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            string s = "select id_lampy, id_sluzby, rodne_cislo, to_char(datum, 'dd.mm.yyyy hh24:mi'), nvl(popis,''), trvanie, cena from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_servis using (id_sluzby) where id_sluzby = " + paIdEntity;
            var select = new VystupSelect(s,
                "id_lampy", "id_sluzby", "rodne_cislo", "datum", "popis", "trvanie", "cena");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                IdLampy = int.Parse(row[0].ToString());
                IdSluzby = int.Parse(row[1].ToString());
                RodneCislo = row[2].ToString();
                Datum = DateTime.Parse(row[3].ToString());
                Popis = row[4].ToString();
                Trvanie = int.Parse(row[5].ToString());
                Cena = int.Parse(row[6].ToString());
                return true;
            }
            return false;
        }

        public override IVystup GetSelectOnTableData()
        {
            string s = "select id_lampy, id_sluzby, to_char(datum, 'dd.mm.yyyy'), nvl(popis,''), trvanie, cena from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_servis using (id_sluzby)  order by id_sluzby desc";
            var select = new VystupSelect(s,
                "cislo", "id_sluzby", "datum", "popis", "trvanie", "cena");
            select.KlucovyStlpec = "ID_SLUZBY";
            return select;

        }
    }
}
