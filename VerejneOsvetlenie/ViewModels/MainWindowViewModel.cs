using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VerejneOsvetlenieData.Data;

namespace VerejneOsvetlenie.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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

        public void NacitajTechnika()
        {
            Technik = new STechnik();
            Technik.SelectPodlaId("8612056244");

            Stlp = new SStlp();
            Stlp.SelectPodlaId(0);

            var servis = new SServis();
            servis.SelectPodlaId(1131);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
