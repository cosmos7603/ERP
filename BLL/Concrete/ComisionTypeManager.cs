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
    public class ComisionTypeManager : ManagerBase<ComisionTypePOCO, ComisionType>
    {
        //public override List<ComisionTypePOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        //{
        //    return base.GetPaged(filters, sort, page, rows, out totalCount);
        //}

        public override bool Update(ComisionTypePOCO poco, int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<ComisionType>())
                {
                    var client = unitOfWork.Repository.GetById(id);
                    Mapper.Map(poco, client);
                    unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public override bool Add(ComisionTypePOCO poco)
        //{
        //    try
        //    {
        //        using (var unitOfWork = new UnitOfWork<ComisionType>())
        //        {

        //           // ComisionType entity = Mapper.Map<ComisionTypePOCO, ComisionType>(poco);
        //            var entity = new ComisionType
        //                         {
        //                             Active = poco.Active,
        //                             ComisionTypeCode = poco.ComisionTypeCode,
        //                             CorporateName = poco.CorporateName,
        //                             ComercialName = poco.ComercialName,
        //                             Email = poco.Email,
        //                             Language = poco.Language,
        //                             WebSite = poco.WebSite,
        //                             Observations = poco.Observations,
        //                             BirthDate = poco.BirthDate,
        //                             SectorId = poco.Sector.Id,
        //                             ChargeMethodId = poco.ChargeMethod.Id,
        //                             CientTypeId = poco.ComisionTypeType.Id,
        //                             PaymentDay = poco.PaymentDay,
        //                             PaymentDueDateTypeId = poco.PaymentDueDateType.Id,
                                     
        //                         };

        //            unitOfWork.Repository.Add(entity);
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
