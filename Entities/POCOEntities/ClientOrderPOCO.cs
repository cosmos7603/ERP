using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class ClientOrderPOCO : EntityPOCO
    {
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public string OrderCode { get; set; }
        public string Reference { get; set; }
        public int ChargeMethodId { get; set; }
        public int PaymentDueDateTypeId { get; set; }
        public string DeliveryAddress { get; set; }
        public string ComercialAgent { get; set; }

        public  ICollection<ClientPOCO> Client { get; set; }
        public ChargeMethodPOCO ChargeMethod { get; set; }
        public  PaymentDueDateTypePOCO PaymentDueDateType { get; set; }

    }
}

