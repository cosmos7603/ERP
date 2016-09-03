using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class SectorManager : ManagerBase<SectorPOCO, Sector>
    {
        public override List<SectorPOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            Mapper.CreateMap<Sector, SectorPOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }

    }
}
