﻿using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstract;
using BLL.Grid;
using DAL;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class PaymentDueDateTypeManager : ManagerBase<PaymentDueDateTypePOCO, PaymentDueDateType>
    {
        public override List<PaymentDueDateTypePOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            Mapper.CreateMap<PaymentDueDateType, PaymentDueDateTypePOCO>();
            return base.GetPaged(filters, sort, page, rows, out totalCount);
        }

    }
}
