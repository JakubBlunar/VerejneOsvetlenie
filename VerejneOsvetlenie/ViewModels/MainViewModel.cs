﻿using System;
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
                new PomenovanyVystup("Testovací výstup 1", new VystupSelect("select * from s_technik", "rodné číslo", "meno", "priezvisko")),
                new PomenovanyVystup("Testovací výstup 2", new VystupProcedura("reklamacia_technika", false, new [] {"rodné číslo", "meno", "priezvisko", "trvanie"}))
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
