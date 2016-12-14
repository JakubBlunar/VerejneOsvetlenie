using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_OBSLUHA_STLPU", DisplayName = "Obsluha stĺpu", TableKey = "ID_SLUZBY")]
    public class SObsluhaStlpu: SqlEntita
    {
        [SqlClass(ColumnName = "ID_SLUZBY", DisplayName = null)]
        public int IdSluzby { get; set; }
        [SqlClass(ColumnName = "ID_SLUZBY", IsReference = true)]
        public SSluzba Sluzba { get; set; }

        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public int Cislo { get; set; }
        [SqlClass(ColumnName = "CISLO", IsReference = true)]
        public SStlp Stlp { get; set; }

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
