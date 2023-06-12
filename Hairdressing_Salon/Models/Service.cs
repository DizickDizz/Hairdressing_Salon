using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Models
{
    public class Service
    {
        public string Type { get; }
        public int Price { get; }
        public Service(string type, int price)
        {
            Type = type;
            Price = price;
        }


    }
}
