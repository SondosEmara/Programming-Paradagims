using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Paradiagm_Part2.DataBase
{
    #region DataBase
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public CustomerGroup CustomerGroup { get; set; }
    }

    public enum CustomerGroup
    {
        Default,
        Wholesaler
    }


    public class Product
    {
        public int Id { get; set; }

        public decimal UnitPrice { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public double TotalPrice { get; set; }
        public List<Product> Items { get; set; } = new List<Product>();
    }

    #endregion

}
