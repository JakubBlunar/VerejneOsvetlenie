using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_SERVIS", DisplayName = "Servis", TableKey = "id_sluzby")]
    public class SServis : SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public string IdSluzby { get; set; }
        [SqlClass(ColumnName = "CENA")]
        public string Cena { get; set; }

        [SqlClass(ColumnName = "id_sluzby", IsReference = true)]
        public SSluzba SSluzba { get; set; }

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