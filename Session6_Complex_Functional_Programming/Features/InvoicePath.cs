using Session6_Complex_Functional_Programming.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session6_Complex_Functional_Programming.Features
{
    public class InvoicePath
    {
        public List<(InvoiceStratgy invoiceStratgy, Func<Order, Invoice> invoiceImplention)> InvoiceImplentaions { get; set; } = new();
        public List<(ShippingStratgy shippingStratgy, Func<Invoice, Shipping> shippingImplentaion)> ShippingImplentaions { get; set; } = new();
        public List<(FreightStratgy freightStratgy, Func<Shipping, Freight> frightImplentaion)> FreightImplentaions { get; set; } = new();

        public InvoicePath()
        {
                InvoiceImplentaions = new()
                {
                    (InvoiceStratgy.Standard,InVoiceStartgyStandard),
                    (InvoiceStratgy.Discounted,InVoiceStartgyDiscounted)
                };

                ShippingImplentaions = new()
                {
                    (ShippingStratgy.Standard,ShippingStandard),
                    (ShippingStratgy.Express,ShippingExpress)
                };

                FreightImplentaions = new()
                {
                    (FreightStratgy.VolumeBased,FreightVolumnBased)
                };
        }

        public Freight FreightVolumnBased(Shipping shipping)
        {

            Freight freight =new Freight() {Cost= shipping.Cost*0.25 };
            return freight;
        }
       

        public Shipping ShippingStandard(Invoice invoice)
        {
            Shipping shipping = new Shipping() {ShipperId=1,Cost= invoice.Amount*0.3};
            return shipping;
        }
        public Shipping ShippingExpress(Invoice invoice)
        {
            Shipping shipping = new Shipping() { ShipperId = 1, Cost = invoice.Amount * 0.25 };

            return shipping;
        }

        public Invoice InVoiceStartgyStandard(Order order)
        {
            Invoice invoic = new Invoice() { Amount = order.TotalPrice };
            return invoic;
        }

        public Invoice InVoiceStartgyDiscounted(Order order)
        {
            Invoice invoic = new Invoice() { Amount = order.TotalPrice, isDisount = true };
            return invoic;
        }
    }
}
