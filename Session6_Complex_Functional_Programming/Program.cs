
using Session6_Complex_Functional_Programming.Data;
using Session6_Complex_Functional_Programming.Features;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Session6_Complex_Functional_Programming
{
    internal static class Program
    {
        #region InvoicePath-Compose
            public static Func<Order, Freight> InvoicePathCompose(Configuration configuration, InvoicePath invoicePath)
            {
                var InvoiceImplentaion = invoicePath.InvoiceImplentaions.FirstOrDefault(invoice => invoice.invoiceStratgy == configuration.InvoiceStratgy).invoiceImplention;
                var ShipppingImplentaion = invoicePath.ShippingImplentaions.FirstOrDefault(invoice => invoice.shippingStratgy == configuration.ShippingStratgy).shippingImplentaion;
                var FreightImplentaion = invoicePath.FreightImplentaions.FirstOrDefault(invoice => invoice.freightStratgy == configuration.FreightStratgy).frightImplentaion;
                return InvoiceImplentaion.Compose(ShipppingImplentaion).Compose(FreightImplentaion);
            }
            public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> First, Func<T2, T3> Second)
            {
                return Input => Second(First(Input));
            }
        #endregion

        #region AvaiablityPath-Compose
        public static Func<Order, ShippingDate> AvaiablityPathCompose(Configuration configuration, AvaiablityPath avaiablityPath)
        {
            var AvaaibliatyImplentaion = avaiablityPath.AvaiablityImplentaions.FirstOrDefault(avaiablity=>avaiablity.avaiablityStratgy==AvaiablityStratgy.PreOrder).aviablityImplentaion;
            var ShippingDateImplentaion = avaiablityPath.ShippingDateImplentaions.FirstOrDefault(shipping => shipping.shippingDateStratgy == ShippingDateStratgy.Scheduled).shippingDateImplentaion;
            return AvaaibliatyImplentaion.Compose(ShippingDateImplentaion);
        }

        #endregion

        #region AdjustCost
            public static Func<Order, double> AdjustCost(Configuration configuration, InvoicePath invoicePath, AvaiablityPath avaiablityPath)
            {
                return (order) => CalcAdjustedCostofOrder(order, InvoicePathCompose(configuration, invoicePath), AvaiablityPathCompose(configuration, avaiablityPath));
            }
            public static double CalcAdjustedCostofOrder(Order order, Func<Order, Freight> InvoicePathFunc, Func<Order, ShippingDate> AvaiablityPathFunc)
            {
                //excute the invoicefunc 
                var fright = InvoicePathFunc(order);
                var shippingDate = AvaiablityPathFunc(order);

                var totalCost = shippingDate.ShipDate.DayOfWeek.ToString() == "Friday" ? fright.Cost + 100 : fright.Cost + 200;
                return totalCost;
            } 
        #endregion

        static void Main(string[] args)
        {
            #region Business                
            //* Order Create Two Paths
            //    * First Path Order --> InVoice --> Shipping --> Transformation Cost
            //    * Second Path Order -->Inventory (Availiablity)-->ShipDate
            //    * Take First and Second  Path --> Freight + ShippingDate  --> Get Finial Cost.
            // in TO Get Invoice we have different implentaions why?? because can exist a customer has differrent invoice to other customer...
            // not invoice has differnt implentaion only but each step
            #endregion


            InvoicePath invoicePath = new InvoicePath();
            AvaiablityPath avaiablityPath = new AvaiablityPath();
            Configuration configuration = new Configuration() { AvaiablityStratgy = AvaiablityStratgy.PreOrder, FreightStratgy = FreightStratgy.VolumeBased, InvoiceStratgy = InvoiceStratgy.Standard, ShippingDateStratgy = ShippingDateStratgy.Scheduled, ShippingStratgy = ShippingStratgy.Standard }; Customer customer_1 = new Customer() { Id = 1, CustomerGroup = CustomerGroup.Default, Name = "Sondos" };

            Customer customer = new Customer() { Id = 1, CustomerGroup = CustomerGroup.Default, Name = "Sondos" };
            Order order_1 = new Order() { Id = 1, Customer = customer, TotalPrice = 140 };

            var totalPrice= AdjustCost(configuration,invoicePath,avaiablityPath)(order_1);

            Console.WriteLine(totalPrice);
        }
    }
} 
