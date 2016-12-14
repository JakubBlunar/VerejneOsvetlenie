using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using VerejneOsvetlenieData.Data;

namespace VerejneOsvetlenie.ViewModels
{
    [ImplementPropertyChanged]
    public class SpravaZaznamovViewModel
    {
        public ObservableCollection<ZaznamInfo> ZaznamyNaSpravu { get; set; }


    }

    public class ZaznamInfo
    {
        public string NazovZaznamu { get; set; }
        public Type TypZaznamu { get; set; }
    }
}
