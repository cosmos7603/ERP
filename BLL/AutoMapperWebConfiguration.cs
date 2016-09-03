using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using DAL;
using Entities.POCOEntities;

namespace BLL
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            //AutoMapper
            Mapper.CreateMap<ProductPOCO, Product>().IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<Product, ProductPOCO>().IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ProductFamilyPOCO, ProductFamily>()
                .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ProductFamily, ProductFamilyPOCO>()
               .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<Provider, ProviderPOCO>()
               .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ProviderPOCO, Provider>()
               .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

              //Mapper.CreateMap<ClientPOCO, Client>()
              //.IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<Client, ClientPOCO>()
              .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ClientTypePOCO, ClientType>()
              .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ClientType, ClientTypePOCO>()
            .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            //Mapper.CreateMap<SectorPOCO, Sector>()
            //  .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            //Mapper.CreateMap<Sector, SectorPOCO>()
            //  .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));


            Mapper.CreateMap<ChargeMethodPOCO, ChargeMethod>()
              .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ChargeMethod, ChargeMethodPOCO>()
              .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<ComercialAgentPOCO, ComercialAgent>()
              .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<TaxPOCO, Tax>()
             .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<Tax, TaxPOCO>()
            .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<PaymentDueDateType, PaymentDueDateTypePOCO>()
            .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));


            Mapper.CreateMap<PaymentDueDateTypePOCO, PaymentDueDateType>()
            .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<SalePOCO, Sale>()
           .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<Sale, SalePOCO>()
          .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<SaleCategoryPOCO, SaleCategory>()
      .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<SaleCategory, SaleCategoryPOCO>()
      .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));


            Mapper.CreateMap<SaleStatePOCO, SaleState>()
      .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<SaleState, SaleStatePOCO>()
      .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));


            Mapper.CreateMap<BillType, BillTypePOCO>()
      .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<BillTypePOCO, BillType>()
    .IgnoreAllNonExisting().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));



        }

        public static U MapValidValues<U, T>(T source, U destination)
        {
            // Go through all fields of source, if a value is not null, overwrite value on destination field.
            foreach (var propertyName in source.GetType().GetProperties().Where(p => !p.PropertyType.IsGenericType).Select(p => p.Name))
            {
                var value = source.GetType().GetProperty(propertyName).GetValue(source, null);
                if (value != null && (value.GetType() != typeof(DateTime) || (value.GetType() == typeof(DateTime) && (DateTime)value != DateTime.MinValue)))
                {
                    destination.GetType().GetProperty(propertyName).SetValue(destination, value, null);
                }
            }

            return destination;
        }

    }

    public static class IMappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
                                                                  && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}
