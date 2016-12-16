using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data.Legacy
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_SLUZBA", DisplayName = "", TableKey = "ID_SLUZBY")]
    public class SSluzba : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }

        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = null, Length = 10)]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "RODNE_CISLO", IsReference = true)]
        public STechnik Technik { get; set; }

        [SqlClass(ColumnName = "DATUM", DisplayName = "Dátum")]
        public string Datum { get; set; }

        [SqlClass(ColumnName = "POPIS", DisplayName = "Popis", Length = 500)]
        public string Popis { get; set; }

        [SqlClass(ColumnName = "TRVANIE", DisplayName = "Trvanie")]
        public int Trvanie { get; set; }

        public override bool Update()
        {
            throw new System.NotImplementedException();
        }

        public override bool Insert()
        {
            throw new System.NotImplementedException();
        }

        public override bool Drop()
        {
            return Databaza.ZmazSluzbu(IdSluzby).JeChyba;
        }

        public override bool SelectPodlaId(object paIdEntity)
        {
            Technik = new STechnik();
            bool b1 = base.SelectPodlaId(paIdEntity);
            bool b2 = Technik.SelectPodlaId(RodneCislo);
            return b1 && b2;
        }
    }
}