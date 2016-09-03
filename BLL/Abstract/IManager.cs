using System.Collections.Generic;
using BLL.Grid;
using Entities.POCOEntities;

namespace BLL.Abstract
{
    public interface IManager<TPoco> where TPoco : EntityPOCO 
    {
        IEnumerable<TPoco> GetAll();
        bool Add(TPoco article);
        bool Update(TPoco article, int id);
        bool Delete(int id);
        List<TPoco> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount);
        TPoco GetById(int id);
        List<TPoco> GetBy(List<Filter> filters);
    }
}
