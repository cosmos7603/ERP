using System;
using System.Collections.Generic;

namespace Entities.POCOEntities
{
    public class ProfessionalPOCO : EntityPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public  ICollection<SpecialityPOCO> Speciality { get; set; }
    }
}