using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class ProductFamilyManager : ManagerBase<ProductFamilyPOCO,ProductFamily>
    {
        public override List<ProductFamilyPOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            Mapper.CreateMap<ProductFamily, ProductFamilyPOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }

        //public override bool Update(ProductPOCO poco, int id)
        //{
        //    try
        //    {
        //        using (var unitOfWork = new UnitOfWork<Product>())
        //        {
        //            var entity = unitOfWork.Repository.GetById(id);
        //            Mapper.Map(poco.ProductFamily, entity.ProductFamily);
        //            Mapper.Map(poco, entity);
        //            unitOfWork.Save();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
