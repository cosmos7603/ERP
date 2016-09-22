using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Promotion : AuditableEntity
    {
        [Key]
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public string Prefix { get; set; }
        public int Vendorid { get; set; }
        public DateTime Validfrom { get; set; }
        public DateTime Validto { get; set; }
        public string Promotiondesc { get; set; }
        public string Instructions { get; set; }
        public string Ratecode { get; set; }
        public string Cagroupnumber { get; set; }
        public string Usgroupnumber { get; set; }
        public string Voyagenumber { get; set; }
        public string Promotionimportcode { get; set; }
        public int? DataFileId { get; set; }
        public string Sailingcode { get; set; }
        public string Esename { get; set; }
        public string Eselink { get; set; }
        public string Eselocation { get; set; }
        public string Eseduration { get; set; }
        public string Eseadditionalinfo { get; set; }
        public DateTime? Esedate { get; set; }
        public int? Importid { get; set; }
        public bool Apipublish { get; set; }
        public bool Apiincludesearch { get; set; }
        public string Apicalloutname { get; set; }
        public string Apihighlightedoffer { get; set; }
        public string Apiimageurl1 { get; set; }
        public string Apiimageurl2 { get; set; }
        public int? ApiFileid { get; set; }
        public string ApiSeotitle { get; set; }
        public string ApiSeodescription { get; set; }
        public string ApiSeokeywords { get; set; }
        public string ApiPublishamenities { get; set; }
        public string Apipublishdisclaimer { get; set; }
        public bool ApiFeature { get; set; }
        public string ApiOffertitle { get; set; }
        public string ApiPublishDescription { get; set; }
        public string PromotionDetails { get; set; }
        public string Consumerurlid { get; set; }
        public int StoreId { get; set; }
        public string ApiimageUrl3 { get; set; }
        public bool Overridesailings { get; set; }
        public string Overridesailingsdaterange { get; set; }
        public string Overridesailingsships { get; set; }
        public string Overridesailingsduration { get; set; }
        public string Overridesailingsembarkation { get; set; }
        public string LafDisplayCatCode { get; set; }
        public decimal? LafOverrideAmt { get; set; }
        public int? PpoOfferId { get; set; }
        public int? PpoPriceId { get; set; }
        public DateTime? Ppoimportdate { get; set; }
        public DateTime? Reviewdate { get; set; }
        public string PromotionCode { get; set; }
    }
}
