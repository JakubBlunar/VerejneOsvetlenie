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

        public SpravaZaznamovViewModel()
        {
            this.InitTypy();
        }

        private void InitTypy()
        {
            ZaznamyNaSpravu = new ObservableCollection<ZaznamInfo>
            {
                new ZaznamInfo() { NazovZaznamu = "technici",  TypZaznamu = typeof(STechnik)},
                new ZaznamInfo() { NazovZaznamu = "typy lamp",  TypZaznamu = typeof(SLampa)},
                new ZaznamInfo() { NazovZaznamu = "servis",  TypZaznamu = typeof(SServis)},
                new ZaznamInfo() { NazovZaznamu = "kontrola",  TypZaznamu = typeof(SKontrola)}
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
