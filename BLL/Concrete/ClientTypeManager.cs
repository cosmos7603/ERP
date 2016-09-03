using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class ClientTypeManager : ManagerBase<ClientTypePOCO, ClientType>
    {
        public override List<ClientTypePOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            Mapper.CreateMap<ClientType, ClientTypePOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }
    }
}
