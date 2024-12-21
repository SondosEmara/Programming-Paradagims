using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session6_Complex_Functional_Programming.Data
{
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
        public DateTime CreatedDate { get; set; }
        public List<Product> Items { get; set; } = new List<Product>();
    }


    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public bool isDisount {  get; set; }
    }

    public class Shipping
    {
        public int ShipperId { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Cost { get; set; }
    }

    public class Freight
    {
        public double Cost { get; set; }
    }

    public class Avaiablity
    {
        public DateTime AvaiblaityTime { get; set; }
    }

    public class ShippingDate
    {
        public DateTime ShipDate { get; set; }

    }

  
    public enum InvoiceStratgy
    {
        Standard,
        Discounted
    }
    public enum ShippingStratgy
    {
        Standard,
        Express,
    }
    public enum FreightStratgy
    {
        WeightBased,
        VolumeBased
    }

    public enum AvaiablityStratgy
    {
        AlwaysAvailable,
        StockBased,
        PreOrder

    }

    public enum ShippingDateStratgy
    {
        SameDay,
        Scheduled,
    }


}
