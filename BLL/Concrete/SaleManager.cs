using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using DAL.Repository.Abstract;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class SaleManager : ManagerBase<SalePOCO, Sale>
    {
        public override bool Add(SalePOCO poco)
        {
            try
            {

                var filter = new Filter { Comparison = ComparisonType.Equal, Property = typeof(SaleState).GetProperty("Description"), Value = "A cobrar" };
                var filters = new List<Filter> {filter};

                var saleStateManager = ManagerFactory.GetInstance().GetManagerFor<SaleStatePOCO>();
                var states = saleStateManager.GetBy(filters);
                var state = states[0];
                poco.SaleStateId = state.Id;
                base.Add(poco);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
