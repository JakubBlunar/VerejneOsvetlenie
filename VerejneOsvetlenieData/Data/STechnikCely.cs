using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;
using VerejneOsvetlenieData.Data.Tables;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    public class STechnikCely:SqlEntita
    {
        public new Databaza Databaza { get; set; }
        public STechnik Technik { get; set; }

        public LinkedList<SKontrolaLampy> KontrolyLamp { get; set; }
        public LinkedList<SKontrolaStlpu> KontrolyStlpov{ get; set; }
        public LinkedList<SServisLampy> ServisLamp { get; set; }
        public LinkedList<SServisStlpu> ServisStlpov { get; set; }

        public STechnikCely(STechnik technik)
        {
            Databaza = new Databaza();
            Technik = technik;
            DeleteEnabled = true;
            KontrolyLamp = new LinkedList<SKontrolaLampy>();
            KontrolyStlpov = new LinkedList<SKontrolaStlpu>();
            ServisLamp = new LinkedList<SServisLampy>();
            ServisStlpov = new LinkedList<SServisStlpu>();
        }

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
            string s = "select id_lampy, rodne_cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy'), nvl(popis,''), trvanie, stav, nvl(svietivost, 0) from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) where rodne_cislo = " + paIdEntity + " order by datum";
            var select = new VystupSelect(s,
                "id_lampy", "rodne_cislo", "id_sluzby", "datum", "popis", "trvanie", "stav", "svietivost");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                KontrolyLamp.AddFirst(new SKontrolaLampy()
                {
                    IdLampy = int.Parse(row[0].ToString()),
                    RodneCislo = row[1].ToString(),
                    IdSluzby = int.Parse(row[2].ToString()),
                    Datum = DateTime.Parse(row[3].ToString()),
                    Popis = row[4].ToString(),
                    Trvanie = int.Parse(row[5].ToString()),
                    Stav = row[6].ToString()[0],
                    Svietivost = int.Parse(row[7].ToString())
                });
            }

            s = "select cislo, rodne_cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy'), nvl(popis,''), trvanie, stav from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_kontrola using (id_sluzby) where rodne_cislo = " + paIdEntity + " order by datum";
            select = new VystupSelect(s,
                "cislo", "rodne_cislo", "id_sluzby", "datum", "popis", "trvanie", "stav");
            select.SpustiVystup();
            foreach (var row in select.Rows)
            {
                KontrolyStlpov.AddFirst(new SKontrolaStlpu()
                {
                    Cislo = int.Parse(row[0].ToString()),
                    RodneCislo = row[1].ToString(),
                    IdSluzby = int.Parse(row[2].ToString()),
                    Datum = DateTime.Parse(row[3].ToString()),
                    Popis = row[4].ToString(),
                    Trvanie = int.Parse(row[5].ToString()),
                    Stav = row[6].ToString()[0]
                });
            }

            s = "select id_lampy, id_sluzby, rodne_cislo, to_char(datum, 'dd.MM.yyyy'), nvl(popis,''), trvanie, cena from s_obsluha_lampy join s_sluzba using (id_sluzby) join s_servis using (id_sluzby) where rodne_cislo = " + paIdEntity + " order by datum";
            select = new VystupSelect(s,
                "id_lampy", "id_sluzby", "rodne_cislo", "datum", "popis", "trvanie", "cena");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                ServisLamp.AddFirst(new SServisLampy()
                {
                    IdLampy = int.Parse(row[0].ToString()),
                    IdSluzby = int.Parse(row[1].ToString()),
                    RodneCislo = row[2].ToString(),
                    Datum = DateTime.Parse(row[3].ToString()),
                    Popis = row[4].ToString(),
                    Trvanie = int.Parse(row[5].ToString()),
                    Cena = int.Parse(row[6].ToString())
                });
            }

            s = "select cislo, rodne_cislo, id_sluzby, to_char(datum, 'dd.mm.yyyy hh24:mi'), nvl(popis,''), trvanie, cena from s_obsluha_stlpu join s_sluzba using (id_sluzby) join s_servis using (id_sluzby) where rodne_cislo = " + paIdEntity + " order by datum";
            select = new VystupSelect(s,
                "cislo", "rodne_cislo", "id_sluzby", "datum", "popis", "trvanie", "cena");
            select.SpustiVystup();

            foreach (var row in select.Rows)
            {
                ServisStlpov.AddFirst(new SServisStlpu()
                {
                    Cislo = int.Parse(row[0].ToString()),
                    RodneCislo = row[1].ToString(),
                    IdSluzby = int.Parse(row[2].ToString()),
                    Datum = DateTime.Parse(row[3].ToString()),
                    Popis = row[4].ToString(),
                    Trvanie = int.Parse(row[5].ToString()),
                    Cena = int.Parse(row[6].ToString())
                });
            }
            return true;
        }
    }
}
