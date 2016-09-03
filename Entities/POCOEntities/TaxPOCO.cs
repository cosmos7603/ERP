using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class TaxPOCO : EntityPOCO
    {
        public int Id { get; set; }
        public string Description { get; set; }

     //   public  ICollection<ProviderPOCO> Provider { get; set; }
    }

}
