using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.POCOEntities
{
	public class SalePOCO : EntityPOCO
	{
		public int Id { get; set; }
		public int ClientId { get; set; }

		public ClientPOCO Client { get; set; }

		public SaleCategoryPOCO SaleCategory { get; set; }


		public SaleStatePOCO SaleState { get; set; }


		[Display(Name = "SaleEmisionDate", ResourceType = typeof(Resources.Resources))]
		public System.DateTime EmisionDate { get; set; }
		public System.DateTime DueDate { get; set; }
		public string BillNumber { get; set; }
		public decimal TotalAmount { get; set; }
		public string AmountToCharge { get; set; }
		public int SaleStateId { get; set; }
		public int SaleCategoryId { get; set; }
		public int BillTypeId { get; set; }
		public string Observations { get; set; }


	}
}