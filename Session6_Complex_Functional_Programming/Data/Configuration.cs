using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session6_Complex_Functional_Programming.Data
{
    public class Configuration
    {
        public AvaiablityStratgy AvaiablityStratgy {  get; set; }
        public ShippingDateStratgy ShippingDateStratgy { get; set; }
        public InvoiceStratgy InvoiceStratgy { get; set; }
        public ShippingStratgy ShippingStratgy { get; set;}
        public FreightStratgy FreightStratgy { get; set;}
    }
}
