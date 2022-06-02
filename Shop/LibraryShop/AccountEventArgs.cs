using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryShop
{
    internal class AccountEventArgs : EventArgs
    {
        
        public string Message { get; set; }
        public string TypeProduct { get; set; }

        public AccountEventArgs(string message, string typeProduct)
        {
            Message = message;
            TypeProduct = typeProduct;
        }
    }
}

