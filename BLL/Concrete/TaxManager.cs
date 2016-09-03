using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class TaxManager : ManagerBase<TaxPOCO, Tax>
    {
        public override List<TaxPOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            Mapper.CreateMap<Tax, TaxPOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }

        public override bool Add(TaxPOCO poco)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<Tax>())
                {

                    // Client entity = Mapper.Map<ClientPOCO, Client>(poco);
                    var entity = new Tax
                    {
                        Description = poco.Description
                    };
                    

                    unitOfWork.Repository.Add(entity);
                    unitOfWork.Save();
                    poco.Id = entity.Id;
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
