using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.POCOEntities
{
    public class ClientPOCO : EntityPOCO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }



        public bool Active { get; set; }

        //[Display(Name = "ClientCode", ResourceType = typeof(Resources.Resources))]
        //public int ClientCode { get; set; }
        [Display(Name = "ComercialName", ResourceType = typeof(Resources.Resources))]
        public string ComercialName { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resources))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
        public string LastName { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }
        //[Display(Name = "Language", ResourceType = typeof(Resources.Resources))]
        //public string Language { get; set; }
        //[Display(Name = "WebSite", ResourceType = typeof(Resources.Resources))]
        //public string WebSite { get; set; }
        [Display(Name = "Observations", ResourceType = typeof(Resources.Resources))]
        public string Observations { get; set; }
        //[Display(Name = "BirthDate", ResourceType = typeof(Resources.Resources))]
        //public DateTime? BirthDate { get; set; }
        //[UIHint("GridForeignKey")]
        //[Display(Name = "ChargeMethod", ResourceType = typeof(Resources.Resources))]
        //public int? ChargeMethodId { get; set; }
        [UIHint("GridForeignKey")]
        [Display(Name = "ClientType", ResourceType = typeof(Resources.Resources))]
        public int ClientTypeId { get; set; }
        //[UIHint("GridForeignKey")]
        //[Display(Name = "PaymentDueDataType", ResourceType = typeof(Resources.Resources))]
        //public int PaymentDueDateTypeId { get; set; }
        //[Display(Name = "PaymentDay", ResourceType = typeof(Resources.Resources))]
        //public short? PaymentDay { get; set; }
        //[Display(Name = "PaymentDay2", ResourceType = typeof(Resources.Resources))]
        //public short? PaymentDay2 { get; set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Resources))]
        public string Country { get; set; }
        //[UIHint("GridForeignKey")]
        //[Display(Name = "ComercialAgent", ResourceType = typeof(Resources.Resources))]
        //public int? ComercialAgentId { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string Address1 { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]

        public string Address2 { get; set; }
        [Display(Name = "Province", ResourceType = typeof(Resources.Resources))]
        
        public string Province { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        public string City { get; set; }
        [Display(Name = "ZipCode", ResourceType = typeof(Resources.Resources))]
        public string ZipCode { get; set; }
        [Display(Name = "Telephone", ResourceType = typeof(Resources.Resources))]
        public string Telephone1 { get; set; }
        [Display(Name = "AlternativeTelephone", ResourceType = typeof(Resources.Resources))]
        public string Telephone2 { get; set; }
        //[Display(Name = "ChargeOverCost", ResourceType = typeof(Resources.Resources))]
        //public string ChargeOverCost { get; set; }
        //[Display(Name = "Discount", ResourceType = typeof(Resources.Resources))]
        //public string Discount { get; set; }
        [Display(Name = "CUIT", ResourceType = typeof(Resources.Resources))]
        public string CUIT { get; set; }

        [Display(Name = "DNI", ResourceType = typeof(Resources.Resources))]
        public string DNI { get; set; }


        //[Required]
        //[Display(Name = "CorporateName", ResourceType = typeof(Resources.Resources))]
        //public string CorporateName { get; set; }
        //[UIHint("GridForeignKey")]
        //[Display(Name = "Tax", ResourceType = typeof(Resources.Resources))]
        //public int TaxId { get; set; }


        //public ChargeMethodPOCO ChargeMethod { get; set; }
        //public ClientTypePOCO ClientType { get; set; }
        ////public SectorPOCO Sector { get; set; }
        //public PaymentDueDateTypePOCO PaymentDueDateType { get; set; }
        //public ComercialAgentPOCO ComercialAgent { get; set; }
    }
}