using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Internal;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using DAL.Repository.Abstract;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class ProductManager : ManagerBase<ProductPOCO, Product>
    {
        public override List<ProductPOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            //Mapper.CreateMap<ProductFamily, ProductFamilyPOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }

        public override bool Update(ProductPOCO poco, int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<Product>())
                {

                    //var repositoryProductFamily = new RepositoryBase<ProductFamily>(unitOfWork.Repository.GetDbContext());
                    var product = unitOfWork.Repository.GetById(id);
                    Mapper.Map(poco, product);
                    unitOfWork.Save();
                    //if (poco.ProductFamily != null)
                    //{
                    //    var productFamily = repositoryProductFamily.GetById(poco.ProductFamily.Id);
                    //    product.ProductFamily = productFamily;
                    //}


                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool Add(ProductPOCO poco)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<Product>())
                {

                    var entity = new Product();
                    entity = Mapper.Map<ProductPOCO, Product>(poco);
                    unitOfWork.Repository.Add(entity);
                    unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
