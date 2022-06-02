using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryShop;

namespace ShopProgram
{
    class MyEventArgs : EventArgs
    {
        public char ch;
    }

    class KeyEvent
    {
        // Создадим событие, используя обобщенный делегат
        public event EventHandler<MyEventArgs> KeyDown;

        public void OnKeyDown(char ch)
        {
            MyEventArgs c = new MyEventArgs();

            if (KeyDown != null)
            {
                c.ch = ch;
                KeyDown(this, c);
            }
        }
    }

    internal class Program
    {
        
        private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Программа отменена, все данные сохранены.");
        }
        static void Main(string[] args)
        {

            Console.CancelKeyPress += Console_CancelKeyPress;

            //МАГАЗИН
            var shop = new Shop();
            string path = @"..\Shop.csv";
            var lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var splits = lines[i + 1].Split(';');
                shop.Balance = Convert.ToDouble(splits[0]);  
                //Console.WriteLine(shop.Balance);
            }



            //Считываем список пользователей
            int countusers = 0;
            path = @"..\user.csv";
            var users = new Users();
            users.LoadUsers(path);


            //Входим в Аккаунт
            string login;
            string password;
            Console.WriteLine();
            Console.Write("Bведите ваш логин:");
            login = Convert.ToString(Console.ReadLine());
            Console.Write("Bведите ваш пароль:");
            password = Convert.ToString(Console.ReadLine());
            var customer = users.TryLogin(login, password);
            if (customer == null)
            {
                Console.WriteLine("Такого пользователя нет.");
            }
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine(customer.Login);
            Console.WriteLine(customer.Balance);


            //создаём списки товаров
            int clothescount = 0;
            int shoescount = 0;
            int accessoriescount = 0;


            Console.WriteLine("Одежда:");
            path = @"..\odezhda.csv";
            var clothes = new Clothes<string>();
            clothes.LoadClothes(path);

            
            Console.WriteLine("\n\n\nОбувь:");
            path = @"..\obuv.csv";
            var shoes = new Shoes<string>();
            shoes.LoadShoes(path);

            
            Console.WriteLine("\n\n\nАксессуары:");
            path = @"..\axessuary.csv";
            var accessories = new Accessories<string>();
            accessories.LoadAccess(path);

            
            //Подготовка переменных для меню.
            var sellclothes = new Clothes<string>[clothes.Count()];
            var sellshoes = new Shoes<string>[shoes.Count()];
            var sellaccessories = new Accessories<string>[accessories.Count()];
            int numclothes = 0;
            int numshoes = 0;
            int numaccessories = 0;
            int k = 4;

            //Меню закупки.
            while (k != 0)
            {
                Console.CancelKeyPress += users.Console_CancelKeyPress;
                Console.CancelKeyPress += shop.Console_CancelKeyPress;
                Console.CancelKeyPress += clothes.Console_CancelKeyPress;
                Console.CancelKeyPress += shoes.Console_CancelKeyPress;
                Console.CancelKeyPress += accessories.Console_CancelKeyPress;
                Console.WriteLine("\n\n\nВ каждой категории можно выбрать только 10 товаров.");
                Console.WriteLine("1)Выбор Одежды");
                Console.WriteLine("2)Выбор Обуви");
                Console.WriteLine("3)Выбор Аксессуров");
                Console.WriteLine("4)Показать корзину");
                Console.WriteLine("0)Сохранить и выйти");
                k = Convert.ToInt32(Console.ReadLine());


                //Открытие вкладки с одеждой
                if (k == 1)
                {
                    if (numclothes < 10)
                    {
                        Console.WriteLine("\nВведите номер товара");
                        int nomer = Convert.ToInt32(Console.ReadLine());
                        int index = 0;
                        if (nomer < 11)
                        {
                            int povtor = 0;
                            var sellclothe = clothes.Try(nomer);
                            for (int i = 0; i < sellclothes.Length; i++)
                            {
                                if (sellclothes[i] != null)
                                {
                                    if (sellclothes[i].Id == sellclothe.Id)
                                    {
                                        povtor = 1;
                                        index = i;
                                    }
                                }
                            }
                            if (povtor == 1)
                            {
                                sellclothes[index].Quantity = sellclothes[index].Quantity + 1;
                            }
                            else
                            {
                                sellclothes[numclothes] = sellclothe;
                                sellclothes[numclothes].Quantity = 1;
                                numclothes++;
                            }
                            povtor = 0;
                            Console.WriteLine("\nВыберите категорию товара");
                        }
                        else
                        {
                            Console.WriteLine("\nТакого товара нет.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nВы выбрали максимальное количество товара");
                    }
                }


                //Открытие вкладыки с Обувью.
                if (k == 2)
                {
                    if (numshoes < 10)
                    {
                        Console.WriteLine("\nВведите номер товара");
                        int nomer = Convert.ToInt32(Console.ReadLine());
                        int index = 0;
                        if (nomer < 11)
                        {
                            int povtor = 0;
                            var sellshoe = shoes.Try(nomer);
                            for (int i = 0; i < sellshoes.Length; i++)
                            {
                                if (sellshoes[i] != null)
                                {
                                    if (sellshoes[i].Id == sellshoe.Id)
                                    {
                                        povtor = 1;
                                        index = i;
                                    }
                                }
                            }
                            if (povtor == 1)
                            {
                                sellshoes[index].Quantity = sellshoes[index].Quantity + 1;
                            }
                            else
                            {
                                sellshoes[numclothes] = sellshoe;
                                sellshoes[numclothes].Quantity = 1;
                                numshoes++;
                            }
                            povtor = 0;
                            Console.WriteLine("\nВыберите категорию товара");
                        }
                        else
                        {
                            Console.WriteLine("\nТакого товара нет.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nВы выбрали максимальное количество товара");
                    }
                }



                //Открытие Вкладки с Аксессуарами.
                if (k == 3)
                {
                    if (numaccessories < 10)
                    {
                        Console.WriteLine("\nВведите номер товара");
                        int nomer = Convert.ToInt32(Console.ReadLine());
                        int index = 0;
                        if (nomer < 11)
                        {
                            int povtor = 0;
                            var sellaccessorie = accessories.Try(nomer);
                            for (int i = 0; i < sellaccessories.Length; i++)
                            {
                                if (sellaccessories[i] != null)
                                {
                                    if (sellaccessories[i].Id == sellaccessorie.Id)
                                    {
                                        povtor = 1;
                                        index = i;
                                    }
                                }
                            }
                            if (povtor == 1)
                            {
                                sellaccessories[index].Quantity = sellaccessories[index].Quantity + 1;
                            }
                            else
                            {
                                sellaccessories[numclothes] = sellaccessorie;
                                sellaccessories[numclothes].Quantity = 1;
                                numaccessories++;
                            }
                            povtor = 0;
                            Console.WriteLine("\nВыберите категорию товара");
                        }
                        else
                        {
                            Console.WriteLine("\nТакого товара нет.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nВы выбрали максимальное количество товара");
                    }
                }



                //Посмотреть купленное.
                if (k == 4)
                {
                    Console.WriteLine("\n одежда:");
                    for (int i = 0; i < sellclothes.Length; i++)
                    {
                        if (sellclothes[i] != null)
                            Console.WriteLine(sellclothes[i].ToString());
                    }


                    Console.WriteLine("\n Обувь:");
                    for (int i = 0; i < sellshoes.Length; i++)
                    {
                        if (sellshoes[i] != null)
                            Console.WriteLine(sellshoes[i].ToString());
                    }


                    Console.WriteLine("\n аксессуары:");
                    for (int i = 0; i < sellaccessories.Length; i++)
                    {
                        if (sellaccessories[i] != null)
                            Console.WriteLine(sellaccessories[i].ToString());
                    }
                    double fullprice = 0;
                    int dostypveshi = 0;


                    //Проверка на полную стоимость.
                    for (int i = 0; i < sellshoes.Length; i++)
                    {
                        if (sellshoes[i] != null)
                        {
                            fullprice += sellshoes[i].Price;
                        }
                    }
                    for (int i = 0; i < sellclothes.Length; i++)
                    {
                        if (sellclothes[i] != null)
                        {
                            fullprice += sellclothes[i].Price;
                        }
                    }
                    for (int i = 0; i < sellaccessories.Length; i++)
                    {
                        if (sellaccessories[i] != null)
                        {
                            fullprice += sellaccessories[i].Price;
                        }
                    }
                    customer.Notify += Shop_Notify;
                    customer.Sale1000(fullprice);

                    //Проверка на наличие вещей.
                    for (int i = 0; i < sellclothes.Length; i++)
                    {
                        if (sellclothes[i] != null)
                        {
                            int sellnomer = sellclothes[i].Quantity;
                            int id = sellclothes[i].Id;
                            dostypveshi -= clothes.Dostup(id, sellnomer);
                        }
                    }
                    for (int i = 0; i < sellaccessories.Length; i++)
                    {
                        if (sellaccessories[i] != null)
                        {
                            int sellnomer = sellaccessories[i].Quantity;
                            int id = sellaccessories[i].Id;
                            dostypveshi -= accessories.Dostup(id, sellnomer);
                        }
                    }
                    for (int i = 0; i < sellshoes.Length; i++)
                    {
                        if (sellshoes[i] != null)
                        {
                            int sellnomer = sellshoes[i].Quantity;
                            int id = sellshoes[i].Id;
                            dostypveshi -= shoes.Dostup(id, sellnomer);
                        }
                    }


                    Console.WriteLine("\n\nЖелаете приобрести товары?");
                    Console.WriteLine("y - yes; n - no");
                    string vybor;
                    vybor = Convert.ToString(Console.ReadLine());
                    if ((vybor == "y") & (customer.Balance >= fullprice) & (dostypveshi > -10))
                    {
                        //приобретение товаров
                        int chek = 2;
                        for (int i = 0; i < sellclothes.Length; i++)
                        {
                            if (sellclothes[i] != null)
                            {
                                int sellnomer = sellclothes[i].Quantity;
                                int id = sellclothes[i].Id;
                                chek += clothes.Pokupka(id, sellnomer);
                            }
                        }
                        for (int i = 0; i < sellaccessories.Length; i++)
                        {
                            if (sellaccessories[i] != null)
                            {
                                int sellnomer = sellaccessories[i].Quantity;
                                int id = sellaccessories[i].Id;
                                chek += shoes.Pokupka(id, sellnomer);
                            }
                        }
                        for (int i = 0; i < sellshoes.Length; i++)
                        {
                            if (sellshoes[i] != null)
                            {
                                int sellnomer = sellshoes[i].Quantity;
                                int id = sellshoes[i].Id;
                                chek += accessories.Pokupka(id, sellnomer);
                            }
                        }

                        if (chek > 0)
                        {
                            customer.Balance -= fullprice;
                            shop.Balance += fullprice;
                            Console.WriteLine("Товары куплены");
                        }



                        //ОЦЕНКА
                        Console.WriteLine("Желаете оставить оценку?");
                        string notify = Console.ReadLine();
                        if (notify == "y")
                        {
                            Console.WriteLine("Напишите первую букву категории товара\n" + "A - Accessories\n" + "C - Clothes\n" + "S - shoes\n" + "E - exit\n");
                            KeyEvent @event = new KeyEvent();
                            @event.KeyDown += (sender, e) =>
                            {
                                switch (e.ch)
                                {
                                    case 'a':
                                        {
                                            accessories.Feedback();
                                            break;
                                        }
                                    case 'c':
                                        {
                                            clothes.Feedback();
                                            break;
                                        }
                                    case 's':
                                        {
                                            shoes.Feedback();
                                            break;
                                        }
                                    case 'e':
                                        {
                                            Console.Beep();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("\nТакая команда не найдена!");
                                            break;
                                        }
                                }
                            };
                            char ch;
                            do
                            {
                                Console.Write("Введите команду: ");
                                ConsoleKeyInfo key;
                                key = Console.ReadKey();
                                ch = key.KeyChar;
                                @event.OnKeyDown(key.KeyChar);
                            }
                            while (ch != 'E');
                        }



                        Console.WriteLine("\nCписок одежды:\n");
                        clothes.Vyvod();


                        Console.WriteLine("\nCписок обуви:\n");
                        shoes.Vyvod();


                        Console.WriteLine("\nCписок аксессуаров:\n");
                        accessories.Vyvod();


                        for (int i = 0; i < shoes.Count(); i++)
                        {
                            sellaccessories[i] = null;
                            sellclothes[i] = null;
                            sellshoes[i] = null;
                        }
                        dostypveshi = 0;
                        fullprice = 0;
                        chek = 0;
                    }
                    else if (customer.Balance < fullprice)
                    {
                        Console.WriteLine("У вас недостаточно средств");
                        for (int i = 0; i < shoes.Count(); i++)
                        {
                            sellaccessories[i] = null;
                            sellclothes[i] = null;
                            sellshoes[i] = null;
                        }
                        dostypveshi = 0;
                        fullprice = 0;
                    }
                    else if (dostypveshi < -100)
                    {
                        Console.WriteLine("Некоторых вещей нет на складе");
                        for (int i = 0; i < shoes.Count(); i++)
                        {
                            sellaccessories[i] = null;
                            sellclothes[i] = null;
                            sellshoes[i] = null;
                        }
                        dostypveshi = 0;
                        fullprice = 0;
                    }
                }
                if (k == 5)
                {
                    Console.WriteLine("Баланс магазина: " + shop.Balance);
                    Console.WriteLine("Баланс пользователя: " + customer.Balance);
                }
                if (k == 6)
                {
                    Console.WriteLine("Блокнотов получено: " + customer.Bloknot);
                }
                if (k == 0)
                {
                    shop.SaveShop();
                    users.SaveUsers();
                    clothes.SaveClothes();
                    shoes.SaveShoes();
                    accessories.SaveAccess();
                }
            }
        }

        private static void Shop_Notify(double sum)
        {
            if (sum > 1000)
            Console.WriteLine("1 блокнот при покупке от тысячи");
        }
    }
}
