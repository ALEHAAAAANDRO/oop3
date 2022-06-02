using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Shoes<T> : Product //обобщение (универсальный шаблон) с универсальным параметром Т
    {
        public T UnderMaterial { get; set; } //использование универсального параметра Т
        public T UpMaterial { get; set; }
        public bool Heel { get; set; }
        public Shoes()
        { }
        public Shoes(int id, double price, int size, string color, string brand, string country, T undermaterial, T upmaterial, bool heel)
        {
            Id = id;
            Price = price;
            Size = size;
            Color = color;
            Brand = brand;
            Country = country;
            UnderMaterial = undermaterial;
            UpMaterial = upmaterial;
            Heel = heel;
        }

        int count;
        public int Count()
        {
            return count;
        }

        public List<Shoes<string>> Shoe { get; set; } = new List<Shoes<string>>();

        public int Dostup(int id, int qu)
        {
            int dostup = 0;
            for (int j = 0; j < Shoe.Count(); j++)
            {
                if (Shoe[j].Id == id)
                {
                    if (Shoe[j].Quantity < qu)
                        dostup = 100000;
                }
            }
            return dostup;
        }

        public int Pokupka(int id, int qu)
        {
            int chek = 0;
            for (int i = 0; i < Shoe.Count(); i++)
            {
                if (Shoe[i].Id == id)
                {
                    if (Shoe[i].Quantity < qu)
                    {
                        Console.WriteLine("Нет товаров.");
                        chek -= 30;
                    }
                    else
                    {
                        Shoe[i].Quantity -= qu;
                        chek += 1;
                    }
                }
            }
            return chek;
        }

        public void Vyvod()
        {
            for (int i = 0; i < Shoe.Count(); i++)
            {
                Console.WriteLine(Shoe[i].ToString());
            }
        }


        public Shoes<string> Try(int id)
        {
            Shoes<string> customer = new();
            for (int j = 0; j < Shoe.Count(); j++)
            {
                if (Shoe[j].Id == id)
                {
                    customer = Shoe[j];
                }
            }
            return customer;
        }

        public void LoadShoes(string path)
        {
            var lines = File.ReadAllLines(path);
            count = lines.Length - 1;
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var splits = lines[i + 1].Split(';');
                var clothe = new Shoes<string>();
                clothe.Id = i + 1;
                clothe.Price = Convert.ToDouble(splits[1]);
                clothe.Size = Convert.ToInt32(splits[2]);
                clothe.Color = splits[3];
                clothe.Brand = splits[4];
                clothe.Country = splits[5];
                clothe.UpMaterial = splits[6];
                clothe.UnderMaterial = splits[7];
                clothe.Heel = Convert.ToBoolean(splits[8]);
                clothe.Quantity = Convert.ToInt32(splits[9]);
                Shoe.Add(clothe);
                Console.WriteLine(clothe.ToString());
            }
        }

        public void SaveShoes()
        {
            string path = @"..\obuv.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\obuv.csv");
                    sw.WriteLine("Id; Price; Size; Color; Brand; Country; UpMaterial; UnderMaterial; Heel; Quntity");
                    for (int i = 0; i < Shoe.Count; i++)
                    {
                        sw.WriteLine($"{Shoe[i].Id};{Shoe[i].Price};{Shoe[i].Size};{Shoe[i].Color};{Shoe[i].Brand};{Shoe[i].Country};{Shoe[i].UpMaterial};{Shoe[i].UnderMaterial};{Shoe[i].Heel};{Shoe[i].Quantity}");
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
            string line = $"{Id} {Price} {Size} {Color} {Brand} {Country} {UnderMaterial} {UpMaterial} {Heel}";
            Console.WriteLine("Вы купили: " + line);
            k = line;
            return k;
        }
        public override string ToString()
        {
            return $"{Id} {Price} {Size} {Color} {Brand} {Country} {UnderMaterial} {UpMaterial} {Heel} {Quantity}";
        }
        public string ToExcel()
        {
            return $"{Id};{Price};{Size};{Color};{Brand};{Country};{UnderMaterial};{UpMaterial};{Heel};{Quantity}";
        }

        public void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            string path = @"..\obuv.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\obuv.csv");
                    sw.WriteLine("Id; Price; Size; Color; Brand; Country; UpMaterial; UnderMaterial; Heel; Quntity");
                    for (int i = 0; i < Shoe.Count; i++)
                    {
                        sw.WriteLine($"{Shoe[i].Id};{Shoe[i].Price};{Shoe[i].Size};{Shoe[i].Color};{Shoe[i].Brand};{Shoe[i].Country};{Shoe[i].UpMaterial};{Shoe[i].UnderMaterial};{Shoe[i].Heel};{Shoe[i].Quantity}");
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
            Shoe[id - 1].Mark = mark;
            Shoe[id - 1].Comment = comment;
            string path = @"..\Otzyv.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\Otzyv.csv", true);
                    sw.WriteLine($"{Shoe[id - 1].Id};{Shoe[id - 1].Brand};{Shoe[id - 1].Mark};{Shoe[id - 1].Comment}");
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
