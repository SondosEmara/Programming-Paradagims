using Session6_Complex_Functional_Programming.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session6_Complex_Functional_Programming.Features
{
    public class AvaiablityPath
    {
        public List<(AvaiablityStratgy avaiablityStratgy, Func<Order, Avaiablity> aviablityImplentaion)> AvaiablityImplentaions { get; set; } = new();
        public List<(ShippingDateStratgy shippingDateStratgy, Func<Avaiablity, ShippingDate> shippingDateImplentaion)> ShippingDateImplentaions { get; set; } = new();

        public AvaiablityPath()
        {
            AvaiablityImplentaions = new()
            {
                (AvaiablityStratgy.PreOrder,PreOrderAvaiablity)
            };

            ShippingDateImplentaions = new()
            {
                 (ShippingDateStratgy.Scheduled,ScheduledShipping)
            };
        }

        public Avaiablity PreOrderAvaiablity(Order order)
        {
            return new Avaiablity() { AvaiblaityTime = order.CreatedDate.AddDays(3) };
        }

        public ShippingDate ScheduledShipping(Avaiablity avaiablity)
        {
            return new ShippingDate() { ShipDate = avaiablity.AvaiblaityTime.AddDays(1) };
        }
    }
}
