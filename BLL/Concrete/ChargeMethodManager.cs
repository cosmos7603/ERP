using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class ChargeMethodManager : ManagerBase<ChargeMethodPOCO, ChargeMethod>
    {
        public override List<ChargeMethodPOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            Mapper.CreateMap<ChargeMethod, ChargeMethodPOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }

    }
}
