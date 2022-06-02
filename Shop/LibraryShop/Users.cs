using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Users
    {
        public double Balance { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }  
        public string Password { get; set; }
        public int Bloknot { get; set; }
        public string ToExcel()
        {
            return $"{Login};{Password};{Name};{Balance}";
        }


        public delegate void Sale(double sum);
        public event Sale? Notify;

        public void Sale1000(double sum)
        {
            Notify?.Invoke(sum);
            if (sum > 1000)
                Bloknot++;
        }


        public Users()
        { }

        public void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            string path = @"..\user.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\user.csv");
                    sw.WriteLine("Login; Password; Name; Balance");
                    for (int i = 0; i < User.Count; i++)
                    {
                        sw.WriteLine($"{User[i].Login};{User[i].Password};{User[i].Name};{User[i].Balance}");
                    }
                    sw.Close();
                }
                finally
                {
                    Console.WriteLine("\n");
                }
            }
        }

        public void LoadUsers(string path)
        {
            var lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var splits = lines[i + 1].Split(';');
                var user = new Users();
                user.Login = splits[0];
                user.Password = splits[1];
                user.Name = splits[2];
                user.Balance = Convert.ToDouble(splits[3]);
                User.Add(user);
                Console.WriteLine(user.Login + " " + user.Password + " " + user.Name + " " + user.Balance);
            }
        }

        public Users TryLogin(string login, string pass)
        {
            var customer = User.FirstOrDefault(x => x.Login == login & x.Password == pass);
            return customer;
        }

        public List<Users> User { get; set; } = new List<Users>();

        public void SaveUsers()
        {
            string path = @"..\user.csv";
            var fileInf1 = new FileInfo(path);
            if (fileInf1.Exists)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(@"..\user.csv");
                    sw.WriteLine("Login; Password; Name; Balance");
                    for (int i = 0; i < User.Count; i++)
                    {
                        sw.WriteLine($"{User[i].Login};{User[i].Password};{User[i].Name};{User[i].Balance}");
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
    } 
}
