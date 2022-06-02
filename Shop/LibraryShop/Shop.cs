using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Shop
    {
        public double Balance { get; set; }


        public void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            string path = @"..\Shop.csv";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\Shop.csv");
                    sw.WriteLine("Balance");
                    sw.WriteLine($"{Balance}");
                    sw.Close();
                }
                finally
                {
                    Console.WriteLine("\n");
                }
            }
        }

        public void SaveShop()
        {
            string path = @"..\Shop.csv";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\Shop.csv");
                    sw.WriteLine("Balance");
                    sw.WriteLine($"{Balance}");
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n" + e.Message);
                }
                finally
                {
                    Console.WriteLine("\n");
                }
            }

        }
    }
}
