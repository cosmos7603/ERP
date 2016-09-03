using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class ComisionTypePOCO : EntityPOCO
    {

        public int Id { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<ComercialAgentPOCO> ComercialAgents { get; set; }

    }
}
