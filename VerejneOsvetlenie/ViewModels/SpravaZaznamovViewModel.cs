using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using VerejneOsvetlenieData.Data;
using VerejneOsvetlenieData.Data.Interfaces;
using VerejneOsvetlenieData.Data.Tables;

namespace VerejneOsvetlenie.ViewModels
{
    [ImplementPropertyChanged]
    public class SpravaZaznamovViewModel
    {
        public ObservableCollection<ZaznamInfo> ZaznamyNaSpravu { get; set; }

        public ZaznamInfo AktualnyTypZaznamu { get; set; }
        public IVystup AktualnyVystup { get; set; }
        public SqlEntita AktualnaEntita { get; set; }

        public SpravaZaznamovViewModel()
        {
            this.InitTypy();
        }

        private void InitTypy()
        {
            ZaznamyNaSpravu = new ObservableCollection<ZaznamInfo>
            {
                new ZaznamInfo() { NazovZaznamu = "Technici",  TypZaznamu = typeof(STechnik)},
                new ZaznamInfo() { NazovZaznamu = "Typy lámp",  TypZaznamu = typeof(SLampa)},
                new ZaznamInfo() { NazovZaznamu = "Stĺpy",  TypZaznamu = typeof(SStlp)},
                new ZaznamInfo() { NazovZaznamu = "Kontroly stĺpov",  TypZaznamu = typeof(SKontrolaStlpu)},
                new ZaznamInfo() { NazovZaznamu = "Kontroly lámp",  TypZaznamu = typeof(SKontrolaLampy)},
                new ZaznamInfo() { NazovZaznamu = "Servis stĺpov",  TypZaznamu = typeof(SServisStlpu)},
                new ZaznamInfo() { NazovZaznamu = "Servis lámp",  TypZaznamu = typeof(SServisLampy)}
            };
        }



    }

    public class ZaznamInfo
    {
        public string NazovZaznamu { get; set; }
        public Type TypZaznamu { get; set; }

        public SqlEntita DajInstanciu()
        {
            return System.Activator.CreateInstance(TypZaznamu) as SqlEntita;
        }
    }
}
