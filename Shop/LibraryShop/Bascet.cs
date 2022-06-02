using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public class Bascet : Product
    {
        public double FullPrice()
        {
            double price = 0;
            
                price += Price;
            
            return price;
        }
    }
}
