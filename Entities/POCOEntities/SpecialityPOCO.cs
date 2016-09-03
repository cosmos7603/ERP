using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class SpecialityPOCO : EntityPOCO
    {

        public string Description { get; set; }

        public ICollection<ProfessionalPOCO> Professional { get; set; }
    }
}
