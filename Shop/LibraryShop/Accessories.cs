using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Accessories<T> : Product //обобщение (универсальный шаблон) с универсальным параметром Т
    {
        public T Material { get; set; } //использование универсального параметра Т
        public bool Gems { get; set; }
        public Accessories()
        { }
        public Accessories(int id, double price, int size, string color, string brand, string country, T material, bool gems)
        {
            Id = id;
            Price = price;
            Size = size;
            Color = color;
            Brand = brand;
            Country = country;
            Material = material;
            Gems = gems;
        }

        int count;
        public int Count()
        {
            return count;
        }

        public List<Accessories<string>> Accessorie { get; set; } = new List<Accessories<string>>();

        public int Dostup(int id, int qu)
        {
            int dostup = 0;
            for (int j = 0; j < Accessorie.Count(); j++)
            {
                if (Accessorie[j].Id == id)
                {
                    if (Accessorie[j].Quantity < qu)
                        dostup = 100000;
                }
            }
            return dostup;
        }

        public int Pokupka(int id, int qu)
        {
            int chek = 0;
            for (int i = 0; i < Accessorie.Count(); i++)
            {
                if (Accessorie[i].Id == id)
                {
                    if (Accessorie[i].Quantity < qu)
                    {
                        Console.WriteLine("Нет товаров.");
                        chek -= 30;
                    }
                    else
                    {
                        Accessorie[i].Quantity -= qu;
                        chek += 1;
                    }
                }
            }
            return chek;
        }

        public void Vyvod()
        {
            for (int i = 0; i < Accessorie.Count(); i++)
            {
                Console.WriteLine(Accessorie[i].ToString());
            }
        }



        public Accessories<string> Try(int id)
        {
            Accessories<string> customer = new();
            for (int j = 0; j < Accessorie.Count(); j++)
            {
                if (Accessorie[j].Id == id)
                {
                    customer = Accessorie[j];
                }
            }
            return customer;
        }

        public void LoadAccess(string path)
        {
            var lines = File.ReadAllLines(path);
            count = lines.Length - 1;
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var splits = lines[i + 1].Split(';');
                var clothe = new Accessories<string>();
                clothe.Id = i + 1;
                clothe.Price = Convert.ToDouble(splits[1]);
                clothe.Size = Convert.ToInt32(splits[2]);
                clothe.Color = splits[3];
                clothe.Brand = splits[4];
                clothe.Country = splits[5];
                clothe.Material = splits[6];
                clothe.Gems = Convert.ToBoolean(splits[7]);
                clothe.Quantity = Convert.ToInt32(splits[8]);
                Accessorie.Add(clothe);
                Console.WriteLine(clothe.ToString());
            }
        }

        public void SaveAccess()
        {
            string path = @"..\axessuary.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\axessuary.csv");
                    sw.WriteLine("Id; Price; Size; Color; Brand; Country; Material; Gems; Quantity");
                    for (int i = 0; i < Accessorie.Count; i++)
                    {
                        sw.WriteLine($"{Accessorie[i].Id};{Accessorie[i].Price};{Accessorie[i].Size};{Accessorie[i].Color};{Accessorie[i].Brand};{Accessorie[i].Country};{Accessorie[i].Material};{Accessorie[i].Gems};{Accessorie[i].Quantity}");
                    }
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

        public string BuyProduct(string k)
        {
            string line = $"{Id} {Price} {Size} {Color} {Brand} {Country} {Material} {Gems}";
            Console.WriteLine("Вы купили: " + line);
            k = line;
            return k;
        }
        public override string ToString()
        {
            return $"{Id} {Price} {Size} {Color} {Brand} {Country} {Material} {Gems} {Quantity}";
        }
        public string ToExcel()
        {
            return $"{Id};{Price};{Size};{Color};{Brand};{Country};{Material};{Gems};{Quantity}";
        }

        public void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            string path = @"..\axessuary.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\axessuary.csv");
                    sw.WriteLine("Id; Price; Size; Color; Brand; Country; Material; Gems; Quantity");
                    for (int i = 0; i < Accessorie.Count; i++)
                    {
                        sw.WriteLine($"{Accessorie[i].Id};{Accessorie[i].Price};{Accessorie[i].Size};{Accessorie[i].Color};{Accessorie[i].Brand};{Accessorie[i].Country};{Accessorie[i].Material};{Accessorie[i].Gems};{Accessorie[i].Quantity}");
                    }
                    sw.Close();
                }
                finally
                {
                    Console.WriteLine("\n");
                }
            }
        }

        public void Feedback()
        {
            Console.WriteLine("\nНапишите Id товара");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Поставьте оценку от 1 до 10");
            int mark = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Напишите отзыв");
            string comment = Console.ReadLine();
            Accessorie[id - 1].Mark = mark;
            Accessorie[id - 1].Comment = comment;
            string path = @"..\Otzyv.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\Otzyv.csv", true);
                    sw.WriteLine($"{Accessorie[id - 1].Id};{Accessorie[id - 1].Brand};{Accessorie[id - 1].Mark};{Accessorie[id - 1].Comment}");
                    sw.Close();
                }
                finally
                {
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
