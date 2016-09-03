using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.POCOEntities
{
    public class ProductPOCO : EntityPOCO
    {

        [Display(Name = "ProductCode", ResourceType = typeof(Resources.Resources))]
        public string ProductCode { get; set; }
        [DisplayName("Nombre")]
        public string ShortDescription { get; set; }
        [DisplayName("Descripción")]  
        public string LongDescription { get; set; }
        [DisplayName("Activo")]
        public bool? Active { get; set; }
        [DisplayName("Disponible")]
        public bool? AvailableForSale { get; set; }
        [DisplayName("Precio de venta")]
        public decimal SalePrice { get; set; }
        [DisplayName("Costo")]
        public decimal Cost { get; set; }
        [DisplayName("Stock")]
        public int Stock { get; set; }
        [DisplayName("Proveedor")]
        public Nullable<int> ProviderId { get; set; }
        [UIHint("GridForeignKey")]
        [DisplayName("Tipo")]
        public int? ProductFamilyId { get; set; }

        //public ProductFamilyPOCO ProductFamily { get; set; }
        //public  ProviderPOCO Provider { get; set; }

    }
}
