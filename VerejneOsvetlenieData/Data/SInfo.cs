using System.Drawing;
using System.IO;
using PropertyChanged;
using VerejneOsvetlenieData.Data.Interfaces;
using Db;

namespace VerejneOsvetlenieData.Data
{
    [ImplementPropertyChanged]
    [SqlClass(TableName = "S_INFO", DisplayName = "info o ståpe", TableKey = "ID")]
    public class SInfo : SqlEntita
    {
        [SqlClass(ColumnName = "ID", DisplayName = null)]
        public int Id { get; set; }

        [SqlClass(ColumnName = "CISLO", DisplayName = null)]
        public int Cislo { get; set; }
        //[SqlClass(ColumnName = "CISLO", IsReference = true)]
        //public SStlp Stlp { get; set; }

        [SqlClass(ColumnName = "DATA", DisplayName = "obrázok", IsBitmapImage = true)]
        public byte[] Data { get; set; }

        [SqlClass(ColumnName = "TYP")]
        public char Typ { get; set; }

        public object Obrazok { get; set; }

        //public MemoryStream Stream => Data != null ? new MemoryStream(Data) : new MemoryStream();

        //public Image Obrazok { get; private set; }

        //~SInformacie()
        //{
        //    Data?.Dispose();
        //}

        public override bool Update()
        {
            return useDbMethod(Databaza.UpdateInfoStlpu(Id, Cislo, Typ, Data));
        }

        public override bool Insert()
        {
            return useDbMethod(Databaza.VlozInfoStlpu(Cislo, Typ, Data));
        }

        public override bool Drop()
        {
            return useDbMethod(Databaza.ZmazInfoOStlpe(Id));
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public byte[] ImageToByteArray(Bitmap imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}