using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class AddressPOCO : EntityPOCO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }

        public ClientPOCO Client { get; set; }
        public ProviderPOCO Provider { get; set; }
    }
}
