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
        [SqlClass(ColumnName = "CISLO", DisplayName = "Cislo stlpu")]
        public int Cislo { get; set; }

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

            string s = "select cislo, id_sluzby, datum, nvl(popis,''), trvanie, stav from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) where id_sluzby = "+ paIdEntity;
            var select = new VystupSelect(s,
                "cislo", "id_sluzby", "datum", "popis", "trvanie", "stav");
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
                    Stav = enumerator.Current[3].ToString();
                    return true;
                }
            }
            return false;
        }

        public override IVystup GetSelectOnTableData()
        {
            string s = "select cislo, id_sluzby, datum, nvl(popis,''), trvanie, stav from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby)";
            var select = new VystupSelect(s,
                "cislo","id_sluzby","datum","popis","trvanie","stav" );
            return select;

        }

    }
}
