using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    public interface ICustomer
    {
        public string BuyProduct();
        public double PriceWithCard(double j);
        public int PriceWithCard(int j);

    }
}
