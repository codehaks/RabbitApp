using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.Models
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Count { get; set; }
        public int Amount { get; set; }
        public DateTime TimeCreated { get; set; }

    }
}
