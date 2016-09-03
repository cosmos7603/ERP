using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class ComercialAgentPOCO : EntityPOCO
    {

        public int Id { get; set; }
        public string ComercialAgentCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string StartDate { get; set; }
        public string Observations { get; set; }
        public string ComisionAmount { get; set; }
        public Nullable<int> ComisionTypeId { get; set; }


        public ICollection<ClientPOCO> Clients { get; set; }
        public ComisionTypePOCO ComisionType { get; set; }
    }
}
