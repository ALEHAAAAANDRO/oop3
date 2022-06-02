using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Clothes<T> : Product //обобщение (универсальный шаблон) с универсальным параметром Т
    {
        public T Material { get; set; } //использование универсального параметра Т
        public bool Image { get; set; }
        public Clothes()
        {}
        public Clothes (int id, double price, int size, string color, string brand, string country, T material, bool image)
        {
            Id = id;
            Price = price;
            Size = size;
            Color = color;
            Brand = brand;
            Country = country;
            Material = material;
            Image = image;
        }
        int count;
        public int Count()
        {
            return count;
        }

        public List<Clothes<string>> Clothe { get; set; } = new List<Clothes<string>>();

        public Clothes<string> Try(int id)
        {
            Clothes<string> customer = new();
            for (int j = 0; j < Clothe.Count(); j++)
            {
                if (Clothe[j].Id == id)
                {
                    customer = Clothe[j];
                }
            }
            return customer;
        }

        public int Dostup(int id, int qu)
        {
            int dostup = 0;
            for (int j = 0; j < Clothe.Count(); j++)
            {
                if (Clothe[j].Id == id)
                {
                    if (Clothe[j].Quantity < qu)
                        dostup = 100000;
                }
            }
            return dostup;
        }

        public int Pokupka(int id, int qu)
        {
            int chek = 0;
            for (int i = 0; i < Clothe.Count(); i++)
            {
                if (Clothe[i].Id == id)
                {
                    if (Clothe[i].Quantity < qu)
                    {
                        Console.WriteLine("Нет товаров.");
                        chek -= 30;
                    }
                    else
                    {
                        Clothe[i].Quantity -= qu;
                        chek += 1;
                    }
                }
            }
            return chek;
        }

        public void Vyvod()
        {
            for (int i =0; i < Clothe.Count(); i++)
            {
                Console.WriteLine(Clothe[i].ToString());
            }
        }

        public void LoadClothes(string path)
        {
            var lines = File.ReadAllLines(path);
            count = lines.Length - 1;
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var splits = lines[i + 1].Split(';');
                var clothe = new Clothes<string>();
                clothe.Id = i + 1;
                clothe.Price = Convert.ToDouble(splits[1]);
                clothe.Size = Convert.ToInt32(splits[2]);
                clothe.Color = splits[3];
                clothe.Brand = splits[4];
                clothe.Country = splits[5];
                clothe.Material = splits[6];
                clothe.Image = Convert.ToBoolean(splits[7]);
                clothe.Quantity = Convert.ToInt32(splits[8]);
                Clothe.Add(clothe);
                Console.WriteLine(clothe.ToString());
            }
        }

        public void SaveClothes()
        {
            string path = @"..\odezhda.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\odezhda.csv");
                    sw.WriteLine("Id; Price; Size; Color; Brand; Country; Material; Image; Quantity");
                    for (int i = 0; i < Clothe.Count; i++)
                    {
                        sw.WriteLine($"{Clothe[i].Id};{Clothe[i].Price};{Clothe[i].Size};{Clothe[i].Color};{Clothe[i].Brand};{Clothe[i].Country};{Clothe[i].Material};{Clothe[i].Image};{Clothe[i].Quantity}");
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
            string line = $"{Id} {Price} {Size} {Color} {Brand} {Country} {Material} {Image}";
            Console.WriteLine("Вы купили: " + line);
            k = line;
            return k;
        }
        public override string ToString()
        {
            return $"{Id} {Price} {Size} {Color} {Brand} {Country} {Material} {Image} {Quantity}";
        }
        public string ToExcel()
        {
            return $"{Id};{Price};{Size};{Color};{Brand};{Country};{Material};{Image};{Quantity}";
        }

        public void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            string path = @"..\odezhda.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\odezhda.csv");
                    sw.WriteLine("Id; Price; Size; Color; Brand; Country; Material; Image; Quantity");
                    for (int i = 0; i < Clothe.Count; i++)
                    {
                        sw.WriteLine($"{Clothe[i].Id};{Clothe[i].Price};{Clothe[i].Size};{Clothe[i].Color};{Clothe[i].Brand};{Clothe[i].Country};{Clothe[i].Material};{Clothe[i].Image};{Clothe[i].Quantity}");
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
            Clothe[id - 1].Mark = mark;
            Clothe[id - 1].Comment = comment;
            string path = @"..\Otzyv.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\Otzyv.csv", true);
                    sw.WriteLine($"{Clothe[id - 1].Id};{Clothe[id - 1].Brand};{Clothe[id - 1].Mark};{Clothe[id - 1].Comment}");
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
