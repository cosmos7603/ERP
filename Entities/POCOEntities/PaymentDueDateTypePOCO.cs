using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class PaymentDueDateTypePOCO : EntityPOCO
    {

        public int Id { get; set; }
        public string Description { get; set; }

        //public  ICollection<ClientPOCO> Client { get; set; }
        //public  ICollection<ClientOrderPOCO> ClientOrder { get; set; }
    }
}
