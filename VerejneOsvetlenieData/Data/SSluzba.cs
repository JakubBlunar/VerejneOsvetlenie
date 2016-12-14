using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_SLUZBA", DisplayName = "", TableKey = "id_sluzby")]
    public class SSluzba : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = null)]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "DATUM", DisplayName = "dátum")]
        public string Datum { get; set; }
        [SqlClass(ColumnName = "POPIS")]
        public string Popis { get; set; }
        [SqlClass(ColumnName = "TRVANIE")]
        public string Trvanie { get; set; }

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
            throw new System.NotImplementedException();
        }
    }
}