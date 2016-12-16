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
    [SqlClass(TableName = "", DisplayName = "Kontrola stlpu", TableKey = "ID_SLUZBY")]
    public class SKontrolaStlpu:SqlEntita
    {
        [SqlClass(ColumnName = "CISLO", DisplayName = "Cislo stlpu", ReadOnly = true)]
        public int Cislo { get; set; }

        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = null, Length = 10)]
        public string RodneCislo { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public string Datum { get; set; }

        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis", Length = 500)]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

        [SqlClass(ColumnName = "STAV", DisplayName = "Stav")]
        public char Stav { get; set; }

        public override bool Update()
        {
            return !Databaza.UpdateKontrolyStlpu(IdSluzby, RodneCislo, Cislo, Popis, Stav,
                    Trvanie, DateTime.Parse(Datum)).JeChyba;
        }

        public override bool Insert()
        {
            return !Databaza.VlozKontroluStlpu(RodneCislo, Cislo, Popis, Stav,
                    Trvanie, DateTime.Parse(Datum)).JeChyba;
        }

        public override bool Drop()
        {
            return !Databaza.ZmazSluzbu(IdSluzby).JeChyba;
        }

        public override bool SelectPodlaId(object paIdEntity)
        {

            var s = "select cislo, rodne_cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy hh24:mi'), nvl(popis,''), trvanie, stav from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) where id_sluzby = " + paIdEntity;
            var select = new VystupSelect(s,
                "cislo", "rodne_cislo", "id_sluzby", "datum", "popis", "trvanie", "stav");
            select.SpustiVystup();


            using (var enumerator = select.Rows.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    Cislo = int.Parse(enumerator.Current[0].ToString());
                    IdSluzby = int.Parse(enumerator.Current[1].ToString());
                    Datum = enumerator.Current[2].ToString();
                    Popis = enumerator.Current[3].ToString();
                    Trvanie = int.Parse(enumerator.Current[4].ToString());
                    Stav = enumerator.Current[5].ToString().ToArray()[0];
                    return true;
                }
            }
            return false;
        }

        public override IVystup GetSelectOnTableData()
        {
            var s = "select cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy'), nvl(popis,''), trvanie, stav from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) order by id_sluzby desc";
            var select = new VystupSelect(s,
                "cislo","id_sluzby","datum","popis","trvanie","stav" );
            select.KlucovyStlpec = "ID_SLUZBY";
            return select;

        }

    }
}
