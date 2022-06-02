using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Product : ICustomer
    {
        public double Price { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public int Quantity { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }

        public string BuyProduct()
        {
            return "";
        }


        //Цена при оплате картой. Перегрузка методов.
        public double PriceWithCard(double j)
        {
            return Math.Truncate(j);
        }
        public int PriceWithCard(int j)
        {
            return j;
        }
    }
}