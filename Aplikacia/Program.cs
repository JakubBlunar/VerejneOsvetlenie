using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
