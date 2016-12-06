using System;
using System.Drawing;
using System.IO;
using System.Text;
using Db;


namespace Aplikacia
{
    class Program
    {
        static void Main(string[] args)
        {
            Databaza db = new Databaza();
            /*
            FileInfo fi = new FileInfo("D:stlp2.jpg");
            FileStream fileStream = fi.OpenRead();
            Image img = new Bitmap(fileStream);
            
            Vysledok res = db.UpdateInfoStlpu(5,0 ,'I', ImageToByteArray(img));
            if (res.JeChyba)
                Console.WriteLine("error");


            Console.WriteLine(res.Popis);
            

            int c = 0;
            DataTable ds = db.GetAllInfo();
            foreach (DataRow row in ds.Rows)
            {
                if (row["typ"].ToString() == "I")
                {
                    Image i = ByteArrayToImage(row["data"] as byte[]);
                    i.Save("D:img" + c + ".jpeg", ImageFormat.Jpeg);
                    c++;
                }
            
            }*/
            /*
            Dictionary<int, Image> qrCodes = new Dictionary<int, Image>();
            Dictionary<int, Image> pictures = new Dictionary<int, Image>();
            for (int i = 0; i < 699; i++)
            {
                FileInfo fi = new FileInfo("D:qr/"+i+".jpg");
                FileStream fileStream = fi.OpenRead();
                Image img = new Bitmap(fileStream);
                qrCodes.Add(i, img);
            }

            for (int i = 0; i < 50; i++)
            {
                FileInfo fi = new FileInfo("D:stlpy/" + i + ".jpg");
                FileStream fileStream = fi.OpenRead();
                Image img = new Bitmap(fileStream);
                pictures.Add(i, img);
            }

            Random rand = new Random();

            for (int i = 0; i < 9775; i++)
            { 
                Image img = pictures[rand.Next(0, pictures.Count)];
                Vysledok res = db.VlozInfoStlpu(i,'I',ImageToByteArray(img));

                img = qrCodes[rand.Next(0, qrCodes.Count)];
                res = db.VlozInfoStlpu(i, 'Q', ImageToByteArray(img));
                           
                Console.WriteLine(i);
            }
            */
            /*
            using (var s = new FileStream("D:import.sql", FileMode.Open))
            {
                    
            }*/

            StringBuilder b = db.Test();
            Console.WriteLine(b.ToString());
            Console.WriteLine("Done");

            var vysledok = new Vysledok();
            var resultProcedurWithoutResult = db.RunProcedureWithOutput("TEST_KOD", null, new ProcedureParameter("parameter1", "integer", 1));
            foreach (var row in resultProcedurWithoutResult)
            {
                Console.WriteLine(row[0]);
            }

            vysledok = new Vysledok();
            var resultProcedureSuccess = db.RunProcedureWithOutput("TEST_KOD", vysledok, new ProcedureParameter("parameter1", "integer", 0));
            foreach (var row in resultProcedureSuccess)
            {
                Console.WriteLine(row[0]);
            }
            Console.WriteLine($"vysledok je chyba:{vysledok.JeChyba}, sprava: {vysledok.Popis}");

            vysledok = new Vysledok();
            var resultProcedureError = db.RunProcedureWithOutput("TEST_KOD", vysledok, new ProcedureParameter("parameter1", "integer", 1));
            foreach (var row in resultProcedureError)
            {
                Console.WriteLine(row[0]);
            }
            Console.WriteLine($"vysledok je chyba:{vysledok.JeChyba}, sprava: {vysledok.Popis}");

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }



    }

}




