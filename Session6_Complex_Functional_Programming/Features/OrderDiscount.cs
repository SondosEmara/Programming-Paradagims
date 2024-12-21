using Session6_Complex_Functional_Programming.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session6_Complex_Functional_Programming.Features
{



    public static class OrderDiscount
    {
        public static Order GetOrderWithDiscount(Order order)
        {
            //Looping Over Functions
            var totalDiscount = OrderDiscountRules.Where(rule => rule.QuailfierCondition(order)).Select(rule => rule.GetDiscount(order)).Sum();
            order.TotalPrice = order.TotalPrice - order.TotalPrice * totalDiscount;
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
            return 0.2;
        }


        private static bool isBQuailifed(Order order)
        {
            //(Qualifier)Customer group “default”  --> total shopping cart amount between $100 and $300 and items =>2 <=5-->  (Calculator) 5% discount
            return (order.Customer.CustomerGroup == CustomerGroup.Default) && (order.TotalPrice >= 100 && order.TotalPrice <= 300) && (order.Items.Count >= 2 && order.Items.Count <= 5);
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
            return order.Items.Any(pro => pro.Id == 1);
        }

        private static double GetEDiscount(Order order)
        {
            //(Qualifier)Any Customer group  when Buy From Product Id = "1" -->(Calculator) get 10 % discount.
            return 0.1;
        }
    }

}
