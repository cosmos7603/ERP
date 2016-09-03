using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.POCOEntities
{
    public class BillTypePOCO : EntityPOCO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}