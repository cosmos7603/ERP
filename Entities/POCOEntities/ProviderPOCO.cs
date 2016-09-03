using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.POCOEntities
{
    public class ProviderPOCO : EntityPOCO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Display(Name = "Province", ResourceType = typeof(Resources.Resources))]
        public string Province { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        public string City { get; set; }
        public string ZipCode { get; set; }
        [Display(Name = "AlternativeTelephone", ResourceType = typeof(Resources.Resources))]
        public string Telephone2 { get; set; }
        [Display(Name = "Telephone", ResourceType = typeof(Resources.Resources))]
        public string Telephone1 { get; set; }
        [Display(Name = "Provider", ResourceType = typeof(Resources.Resources))]
        public string ComercialName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Observations", ResourceType = typeof(Resources.Resources))]
        public string Observations { get; set; }
        [Display(Name = "CUIT", ResourceType = typeof(Resources.Resources))]
        public string CUIT { get; set; }
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resources))]
        public string FirstName { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
        public string LastName { get; set; }
    }

}
