using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Overview
{
    public class Account
    {
        public string Name { get; set; }
        public int Sum { get; set; }

        public List<string> Tags { get; set; } = new();

        public delegate void AccountHandler(string message);
        public event AccountHandler? Notify;

        public event AccountHandler? TakeNotify;
        public event AccountHandler? PutNotify;


        AccountHandler? notify;
        public event AccountHandler? NotifyWithManage
        {
            add
            {
                notify += value;
                Console.WriteLine($"{value.Method.Name} добавлен");
            }
            remove
            {
                notify -= value;
                Console.WriteLine($"{value.Method.Name} удален");
            }
        }


        public Account(string name, int sum)
        {
            Name = name;
            Sum = sum;
        }
        public void Put(int sum)
        {
            Sum += sum;
            Notify?.Invoke($"На счет поступило: {sum}");
            PutNotify?.Invoke($"На счет поступило: {sum}");

            notify?.Invoke($"На счет поступило: {sum}");
            //Console.WriteLine($"На счет поступило: {sum}");
        }

        public void Take(int sum)
        {
            if (Sum >= sum)
            {
                Sum -= sum;
                Notify?.Invoke($"Со счета снято: {sum}");
                TakeNotify?.Invoke($"Со счета снято: {sum}");

                notify?.Invoke($"Со счета снято: {sum}");
                //Console.WriteLine($"Со счета снято: {sum}");
            }
            else
            {
                Notify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {sum}");
                TakeNotify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {sum}");

                notify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {sum}");
                //Console.WriteLine($"Недостаточно денег на счете. Текущий баланс: {sum}");
            }
        }
        //public void ShopReaction(object sender, AccountEventArgs e)
        //{
        //    var shop = (Shop)sender;
        //    if (Tags.Contains(e.TypeProduct))
        //    {
        //        Console.WriteLine(Name + ": " + shop.Name + ": " + e.Message);
        //    }
        //}
    }
}