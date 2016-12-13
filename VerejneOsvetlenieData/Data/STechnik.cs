using VerejneOsvetlenieData.Data.Interfaces;

namespace VerejneOsvetlenieData.Data
{
    [SqlClass(TableName = "S_TECHNIK", DisplayName = "Technik", TableKeyContraint = "rodne_cislo = {0}")]
    public class STechnik : SqlEntita
    {
        [SqlClass(ColumnName = "RODNE_CISLO", DisplayName = "")]
        public string RodneCislo { get; set; }
        [SqlClass(ColumnName = "MENO")]
        public string Meno { get; set; }
        [SqlClass(ColumnName = "PRIEZVISKO")]
        public string Priezvisko { get; set; }

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