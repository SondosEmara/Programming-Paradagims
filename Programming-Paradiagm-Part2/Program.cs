using Programming_Paradiagm_Part2.DataBase;
using System.Security.Cryptography.X509Certificates;
using static Programming_Paradiagm_Part2.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Programming_Paradiagm_Part2
{
    internal class Program
    {

        #region Impretive Programming
            public static class ProcCalculation
            {
                public static double GetOrderDiscount(Order order)
                {
                    var Customer_Group = order.Customer.CustomerGroup;
                    var discount = 0.0;


                    //Customer group “default”  --> total shopping cart amount between $100 and $300 and items >5-->  20% discount
                    //Customer group “default”  --> total shopping cart amount between $100 and $300 and items =>2 <=5-->  5% discount
                    //Customer group “default”  --> total shopping cart amount between 800 -->  25% discount
                    //Customer group “Wholesaler”  --> total shopping cart amount between 500, 800 -->  15% discount
                    //Any Customer group  when Buy From Product Id = "1" --> get 10 % discount.

                    if (Customer_Group == CustomerGroup.Default)
                    {
                        if (order.TotalPrice >= 100 && order.TotalPrice <= 300)
                        {
                            if (order.Items.Count > 5) discount += 0.2;
                            else if (order.Items.Count >= 2 && order.Items.Count <= 5) discount += 0.05;
                        }

                        else if (order.TotalPrice >= 800 && order.TotalPrice <= 15000)
                        {
                            discount += 0.25;
                        }
                    }
                    if (Customer_Group == CustomerGroup.Wholesaler && order.TotalPrice >= 500 && order.TotalPrice <= 800)
                    {
                        discount += 0.15;
                    }

                    foreach (var product in order.Items)
                    {
                        if (product.Id == 1)
                            discount += 0.1;

                    }
                    return discount;
                }
                public static (int orderId, double totalPrice) ApplyDiscount(Order order)
                {
                    return (order.Id, order.TotalPrice - order.TotalPrice * GetOrderDiscount(order));
                }
            }
        #endregion

        #region Functional Programming
           public static class OrderDiscount
           {
                public static Order GetOrderWithDiscount(Order order)
                {
                     //Looping Over Functions
                     var totalDiscount=OrderDiscountRules.Where(rule=>rule.QuailfierCondition(order)).Select(rule=>rule.GetDiscount(order)).Sum();
                     order.TotalPrice=order.TotalPrice-order.TotalPrice*totalDiscount;
                     return order;
                }

                public static List<(Func<Order, bool> QuailfierCondition, Func<Order, double> GetDiscount)> OrderDiscountRules = new()
                {
                        (isAQuailifed,GetADiscount),//(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items >5-->     (Calculator) 20% discount
                        (isBQuailifed,GetBDiscount),//(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items =>2 <=5-->  (Calculator) 5% discount
                        (isCQuailifed,GetCDiscount),//(Qualifier)Customer group “default”  --> total shopping cart amount between 800 -->  (Calculator)25% discount
                        (isDQuailifed,GetDDiscount),//(Qualifier)Customer group “Wholesaler”  --> total shopping cart amount between 500, 800 --> (Calculator) 15% discount
                        (isEQuailifed,GetEDiscount),//(Qualifier)Any Customer group  when Buy From Product Id = "1" -->(Calculator) get 10 % discount.
                };


                private static bool isAQuailifed(Order order)
                {
                    //(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items >5-->     (Calculator) 20% discount
                    return (order.Customer.CustomerGroup == CustomerGroup.Default) && (order.TotalPrice >= 100 && order.TotalPrice <= 300) && (order.Items.Count > 5);
                }

                private static double GetADiscount(Order order)
                {
                    //(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items >5-->     (Calculator) 20% discount
                    return  0.2;
                }


                private static bool isBQuailifed(Order order)
                {
                    //(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items =>2 <=5-->  (Calculator) 5% discount
                    return (order.Customer.CustomerGroup == CustomerGroup.Default) && (order.TotalPrice >= 100 && order.TotalPrice <= 300) && (order.Items.Count >=2&& order.Items.Count<=5);
                }

                private static double GetBDiscount(Order order)
                {
                    //(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items =>2 <=5-->  (Calculator) 5% discount
                    return 0.05;
                }

                private static bool isCQuailifed(Order order)
                {
                    //(Qualifier)Customer group “default”  --> total shopping cart amount between 800 ,-->  (Calculator)25% discount
                    return (order.Customer.CustomerGroup == CustomerGroup.Default) && (order.TotalPrice >= 800 && order.TotalPrice <= 15000);
                }

                private static double GetCDiscount(Order order)
                {
                    //(Qualifier)Customer group “default”  --> total shopping cart amount between 800 -->  (Calculator)25% discount
                    return 0.25;
                }
                private static bool isDQuailifed(Order order)
                {
                    //(Qualifier)Customer group “Wholesaler”  --> total shopping cart amount between 500, 800 --> (Calculator) 15% discount
                    return (order.Customer.CustomerGroup == CustomerGroup.Wholesaler) && (order.TotalPrice >= 500 && order.TotalPrice <= 800);
                }

                private static double GetDDiscount(Order order)
                {
                    //(Qualifier)Customer group “Wholesaler”  --> total shopping cart amount between 500, 800 --> (Calculator) 15% discount
                    return 0.15;
                }

                private static bool isEQuailifed(Order order)
                {
                    //(Qualifier)Any Customer group  when Buy From Product Id = "1" -->(Calculator) get 10 % discount.
                    return order.Items.Any(pro=>pro.Id == 1);
                }

                private static double GetEDiscount(Order order)
                {
                    //(Qualifier)Any Customer group  when Buy From Product Id = "1" -->(Calculator) get 10 % discount.
                    return  0.1;
                }
           }
        #endregion


        static void Main(string[] args)
        {


            #region Data
                    #region Customers
                        Customer customer_1 = new Customer() { Id = 1, CustomerGroup = CustomerGroup.Default, Name = "Sondos" };
                        Customer customer_2 = new Customer() { Id = 2, CustomerGroup = CustomerGroup.Default, Name = "Sarah" };
                        Customer customer_3 = new Customer() { Id = 3, CustomerGroup = CustomerGroup.Default, Name = "Salma" };
                        Customer customer_4 = new Customer() { Id = 4, CustomerGroup = CustomerGroup.Wholesaler, Name = "Sherouk" };
                        Customer customer_5 = new Customer() { Id = 5, CustomerGroup = CustomerGroup.Wholesaler, Name = "Hoda" };
                        Customer customer_6 = new Customer() { Id = 6, CustomerGroup = CustomerGroup.Wholesaler, Name = "Fayza" };
                    #endregion

                    #region Products
                        Product product_1 = new Product() { Id = 1, UnitPrice = 20 };
                        Product product_2 = new Product() { Id = 2, UnitPrice = 120 };
                        Product product_3 = new Product() { Id = 3, UnitPrice = 2000 };
                    #endregion

                    #region Order
                        Order order_1 = new Order() { Id = 1, Customer=customer_1, TotalPrice= 140,Items = new List<Product>() { product_1, product_2 } };
                        Order order_2 = new Order() { Id = 2, Customer=customer_2, TotalPrice = 140, Items = new List<Product>() { product_1, product_2 } };
                        Order order_3 = new Order() { Id = 3, Customer = customer_3, TotalPrice = 2020, Items = new List<Product>() { product_1, product_3 } };
                        Order order_4 = new Order() { Id = 4, Customer = customer_4, TotalPrice = 2000, Items = new List<Product>() { product_3 } };
                        Order order_5 = new Order() { Id = 5, Customer = customer_5, TotalPrice = 2140,Items = new List<Product>() { product_1, product_2, product_3 } };
                        Order order_6 = new Order() { Id = 6, Customer = customer_6, TotalPrice = 2120,Items = new List<Product>() { product_2, product_3 } };
                        List<Order> Orders = new List<Order>() { order_1, order_2, order_3, order_4, order_5, order_6 };
            #endregion
            #endregion

            #region Bussiness

                //Customer group “default”  --> total shopping cart amount between $100 and $300 and items >5-->  20% discount
                //Customer group “default”  --> total shopping cart amount between $100 and $300 and items =>2 <=5-->  5% discount
                //Customer group “default”  --> total shopping cart amount between 800 -->  25% discount
                //Customer group “Wholesaler”  --> total shopping cart amount between 500, 800 -->  15% discount
                //Any Customer group  when Buy From Product Id = "1" --> get 10 % discount.

            #endregion

            #region Imperative Programming
                #region Procedural Programming
                    foreach (Order order in Orders)
                    {
                       var tuple = ProcCalculation.ApplyDiscount(order);
                       Console.WriteLine($"({tuple.orderId},{tuple.totalPrice})");
                    }
            #endregion
            #endregion

            #region Declartive Programming
                #region Functional Programming
                    Orders.Select(order=> OrderDiscount.GetOrderWithDiscount(order)).ToList().ForEach(order => Console.WriteLine($"({order.Id},{order.TotalPrice})"));
                #endregion
            #endregion
        }
    }
    
}
