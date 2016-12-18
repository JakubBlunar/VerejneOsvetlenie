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
        [SqlClass(ColumnName = "CISLO", DisplayName = "Číslo stlpu",ReadOnly = true)]
        public int Cislo { get; set; }

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

        [SqlClass(ColumnName = "Cena", DisplayName = "Cena")]
        public int Cena { get; set; }

        public SServisStlpu()
        {
            DeleteEnabled = true;
        }

        public override bool Update()
        {
            return UseDbMethod(Databaza.UpdateServisuStlpu(IdSluzby, RodneCislo, Cislo, Popis, Trvanie, Datum, Cena));
        }

        public override bool Insert()
        {
            return UseDbMethod(Databaza.VlozServisStlpu(RodneCislo, Cislo, Popis, Trvanie, Datum, Cena));
        }

        public override bool Drop()
        {
            return UseDbMethod(Databaza.ZmazSluzbu(IdSluzby));
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            string s = "select cislo, rodne_cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy hh24:mi'), nvl(popis,''), trvanie, cena from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_servis using (id_sluzby) where id_sluzby = " + paIdEntity;
            var select = new VystupSelect(s,
                "cislo", "rodne_cislo", "id_sluzby", "datum", "popis", "trvanie", "cena");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                Cislo = int.Parse(row[0].ToString());
                RodneCislo = row[1].ToString();
                IdSluzby = int.Parse(row[2].ToString());
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
            string s = "select cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy'), nvl(popis,''), trvanie, cena from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_servis using (id_sluzby)  order by id_sluzby desc";
            var select = new VystupSelect(s,
                "cislo stlpu", "id_sluzby", "datum", "popis", "trvanie", "cena");
            select.KlucovyStlpec = "ID_SLUZBY";
            return select;

        }
    }
}
