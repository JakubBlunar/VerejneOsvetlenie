using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using VerejneOsvetlenieData.Data;
using VerejneOsvetlenieData.Data.Tables;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using Db;

namespace VerejneOsvetlenie.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PomenovanyVystup> Vystupy { get; set; }

        private STechnik _mTechnik;
        public STechnik Technik
        {
            get { return _mTechnik; }
            set
            {
                if (_mTechnik != value)
                {
                    _mTechnik = value;
                    OnPropertyChanged(nameof(Technik));
                }
            }
        }

        private SStlp _mStlp;
        public SStlp Stlp
        {
            get { return _mStlp; }
            set
            {
                if (_mStlp != value)
                {
                    _mStlp = value;
                    OnPropertyChanged(nameof(Stlp));
                }
            }
        }

        public VystupSelect VystupSelectTest { get; set; }

        public void NacitajTechnika()
        {
            //Technik = new STechnik();
            //Technik.SelectPodlaId("8612056244");

            //Stlp = new SStlp();
            //Stlp.SelectPodlaId(0);

            //var servis = new SServis();
            //servis.SelectPodlaId(1131);


            InitVystupy();
        }

        private void InitVystupy()
        {

            Vystupy = new ObservableCollection<PomenovanyVystup>
            {
                /*new PomenovanyVystup("Testovací výstup 1", new VystupSelect("select * from s_technik", "rodné číslo", "meno", "priezvisko")),  
                new PomenovanyVystup("Testovací výstup s parametrami", 
                new VystupProcedura("vypis_chybnych_ulic", 
                false, 
                new [] {"id_ulice", "názov", "počet", "poradie"}, 
                new ProcedureParameter("pocet", "number", 500),
                new ProcedureParameter("pa_odkedy", "date", DateTime.Now.AddYears(-10).ToString("yyyy-MM-dd")),
                new ProcedureParameter("pa_dokedy", "date", DateTime.Now.ToString("yyyy-MM-dd"))))*/
            };

            DateTime datumOd = DateTime.ParseExact("01.01.2000 13:26", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime datumDo = DateTime.Now;

            Vystupy.Add(new PomenovanyVystup("Výpis prvých desať technikov, podľa počtu servisných zásahov za definované obdobie.",
                new VystupProcedura("najlepsi_technici", false, new[] { "Rodné číslo", "Počet zásahov", "Dense Rank" },
                new ProcedureParameter("datum_od", "date", datumOd.ToString("dd.MM.yyyy")),
                new ProcedureParameter("datum_do", "date", datumDo.ToString("dd.MM.yyyy")),
                new ProcedureParameter("pocet", "number", 10)
            )));

            var vystup4 = ResourceVystupy.vystup_4.Replace("\r\n", " ").Replace(";", "");
            Vystupy.Add(new PomenovanyVystup("Výpis ulíc, na ktorých je viac ako 15% nesvietiacich lámp.",
                new VystupSelect(vystup4, "Id Ulice", "Názov", "Mesto")));

            var vystup7 = ResourceVystupy.vystup_7.Replace("\r\n", " ").Replace(";", "");
            Vystupy.Add(new PomenovanyVystup("Výpis 10% stĺpov s celkovou najnižšou svietivosťou lámp.",
                new VystupSelect(vystup7, "Číslo Stĺpu", "Svietivost")));

            var vystupD4 = ResourceVystupy.vystup_D2.Replace("\r\n", " ").Replace(";", "");
            Vystupy.Add(new PomenovanyVystup("Výpis typu lampy, ktorá sa najviac kazí.",
                new VystupSelect(vystupD4, "Id Typu", "Svietivosť", "Typ")));

            var vystupD5 = ResourceVystupy.vystup_D5.Replace("\r\n", " ").Replace(";", "");
            Vystupy.Add(new PomenovanyVystup("Výpis stĺpov, ktorých všetky fotografie sú staršie ako rok.",
                new VystupSelect(vystupD5, "Číslo Stĺpu", "Ulica", "Poradie")));

            Vystupy.Add(new PomenovanyVystup("Výpis stĺpov, na ktorých sme menili dopravnú značku minulý rok viac ako 2x.",
                new VystupProcedura("menene_znacky_na_stlpe", false, new[] { "Číslo Stĺpu" },
                new ProcedureParameter("PA_POC_ROKOV_DOZADU", "number", 1), new ProcedureParameter("PA_POC_MENENI", "number", 2))));

            Vystupy.Add(new PomenovanyVystup("Výpis servisných informácií na konkrétnej ulici pre každý stĺp.",
                new VystupProcedura("xml_servisy_stlpov", false, new[] { "Riadok" },
                new ProcedureParameter("pa_id_ulice", "number", 1)
            )));

            Vystupy.Add(new PomenovanyVystup("Výpis ulíc, na ktorých sú viac ako 2 stĺpy vedľa seba, ktoré nesvietia.",
               new VystupProcedura("nesvietiace_lampy_v_rade", false, new[] { "Id_Ulice", "Názov ulice", "Mesto" })));

            Vystupy.Add(new PomenovanyVystup("Opravy stlpov od do s granularitou",
             new VystupProcedura("OPRAVY_STLPOV", true, new[] { "Dátum", "Počet" },
              new ProcedureParameter("pa_granularity", "varchar2", "year"),
              new ProcedureParameter("paOd", "date", datumOd.ToString("dd.MM.yyyy")),
               new ProcedureParameter("paDo", "date", datumDo.ToString("dd.MM.yyyy"))
             )));

            Vystupy.Add(new PomenovanyVystup("Opravy stlpov zadaneho typu od do s granularitou",
             new VystupProcedura("OPRAVY_STLPOV_TYPU", true, new[] { "Dátum", "Počet" },
                new ProcedureParameter("pa_typ", "char", 'C'),
                new ProcedureParameter("pa_granularity", "varchar2", "year"),
                new ProcedureParameter("paOd", "date", datumOd.ToString("dd.MM.yyyy")),
               new ProcedureParameter("paDo", "date", datumDo.ToString("dd.MM.yyyy"))
             )));

            Vystupy.Add(new PomenovanyVystup("Opravy stlpov zadanej ulice od do s granularitou",
            new VystupProcedura("OPRAVY_STLPOV_ULICE", true, new[] { "Dátum", "Počet" },
               new ProcedureParameter("pa_id_ulice", "number", 1),
               new ProcedureParameter("pa_granularity", "varchar2", "year"),
               new ProcedureParameter("paOd", "date", datumOd.ToString("dd.MM.yyyy")),
              new ProcedureParameter("paDo", "date", datumDo.ToString("dd.MM.yyyy"))
            )));

            Vystupy.Add(new PomenovanyVystup("Opravy stlpov zadaného veku, od do s granularitou",
            new VystupProcedura("OPRAVY_STLPOV_VEKU", true, new[] { "Dátum", "Počet" },
               new ProcedureParameter("pa_vek", "number", 1),
               new ProcedureParameter("pa_granularity", "varchar2", "year"),
               new ProcedureParameter("paOd", "date", datumOd.ToString("dd.MM.yyyy")),
              new ProcedureParameter("paDo", "date", datumDo.ToString("dd.MM.yyyy"))
            )));

            Vystupy.Add(new PomenovanyVystup("Štatistické údaje výdrže stĺpa",
            new VystupProcedura("STAT_VYDRZE_STLPA", false, new[] { "Typ štatistiky", "Výsledok(v dňoch)" },
               new ProcedureParameter("pa_cislo", "number", 1),
               new ProcedureParameter("pa_od", "date", datumOd.ToString("dd.MM.yyyy")),
              new ProcedureParameter("pa_do", "date", datumDo.ToString("dd.MM.yyyy"))
            )));

            Vystupy.Add(new PomenovanyVystup("Štatistické údaje výdrže typu stĺpa",
            new VystupProcedura("STAT_VYDRZE_TYPU_STLPA", false, new[] { "Typ štatistiky", "Výsledok(v dňoch)" },
               new ProcedureParameter("pa_typ", "char", 'X'),
               new ProcedureParameter("pa_od", "date", datumOd.ToString("dd.MM.yyyy")),
              new ProcedureParameter("pa_do", "date", datumDo.ToString("dd.MM.yyyy"))
            )));

            Vystupy.Add(new PomenovanyVystup("Štatistické údaje k výdrži stĺpov na ulici",
            new VystupProcedura("STAT_VYDRZE_STLPOV_UL", false, new[] { "Typ štatistiky", "Výsledok(v dňoch)" },
               new ProcedureParameter("pa_id_ulice", "number", '0'),
               new ProcedureParameter("pa_od", "date", datumOd.ToString("dd.MM.yyyy")),
              new ProcedureParameter("pa_do", "date", datumDo.ToString("dd.MM.yyyy"))
            )));

            Vystupy.Add(new PomenovanyVystup("Priemerné výdrže stĺpov",
            new VystupProcedura("PRIEMERNE_VYDRZE_STLPOV", false, new[] { "Typ stĺpa", "Počet svietidiel", "Typ svietidiel", "Počet nájdených", "Priemerná výdrž(v dňoch)" },
               new ProcedureParameter("pa_typ_stlpu", "char", '?'),
               new ProcedureParameter("pa_svetiel", "char", '?'),
               new ProcedureParameter("pa_typ_svetla", "char", '?')
            )));

            Vystupy.Add(new PomenovanyVystup("Výpis osoby, ktorá opravila stĺp, ale do týždňa ho bolo treba opraviť opäť.",
                new VystupProcedura("reklamacia_technika", false, new[] { "rodné číslo", "meno", "priezvisko" })));


            Vystupy.Add(new PomenovanyVystup("Výpis ulíc, kde bol najčastejšie potrebný servisný zásah.",
                new VystupProcedura("vypis_chybnych_ulic", false, new[] { "Id Ulice", "Názov", "Počet", "Poradie" },
                new ProcedureParameter("pa_pocet", "number", 10),
                new ProcedureParameter("datum_od", "date", datumOd.ToString("dd.MM.yyyy")),
                new ProcedureParameter("datum_do", "date", datumDo.ToString("dd.MM.yyyy"))

            )));


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
