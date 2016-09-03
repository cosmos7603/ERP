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
    public class ClientManager : ManagerBase<ClientPOCO, Client>
    {
        //public override List<ClientPOCO> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        //{
        //    return base.GetPaged(filters, sort, page, rows, out totalCount);
        //}

        public override bool Update(ClientPOCO poco, int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<Client>())
                {
                    var client = unitOfWork.Repository.GetById(id);

                    Mapper.Map(poco, client);



                    //client.Active = poco.Active;
                    //client.Address1 = poco.Address1;
                    //client.Address2 = poco.Address2;
                    //client.CientTypeId = poco.ClientTypeId;
                    //client.PaymentDay = poco.PaymentDay;
                    //client.PaymentDay2 = poco.PaymentDay2;
                    //client.Country = poco.Country;
                    //client.ComercialAgentId = poco.ComercialAgentId;
                    //client.Province = poco.Province;
                    //client.City = poco.City;
                    //client.ZipCode = poco.ZipCode;
                    //client.Telephone1 = poco.Telephone1;
                    //client.Thelephone2 = poco.Thelephone2;
                    //client.ChargeOverCost = poco.ChargeOverCost;
                    //client.Discount = poco.Discount;
                    //client.ClientCode = poco.ClientCode;
                    //client.CorporateName = poco.CorporateName;
                    //client.ComercialName = poco.ComercialName;
                    //client.Email = poco.Email;
                    //client.Language = poco.Language;
                    //client.WebSite = poco.WebSite;
                    //client.Observations = poco.Observations;
                    //client.BirthDate = poco.BirthDate;
                    //client.SectorId = poco.Sector?.Id;
                    //client.ChargeMethodId = poco.ChargeMethod?.Id;
                    //client.CientTypeId = poco.ClientType?.Id;
                    //client.PaymentDay = poco.PaymentDay;
                    //client.PaymentDueDateTypeId = poco.PaymentDueDateType.Id;
                    //client.ComercialAgentId = poco.ComercialAgent?.Id;
                    unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override bool Add(ClientPOCO poco)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<Client>())
                {


                    var entity = new Client();
                    entity = Mapper.Map<ClientPOCO, Client>(poco);

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

        //public override  IEnumerable<ClientPOCO> GetAll()
        //{
        //    try
        //    {
        //        List<Client> listEntities;
        //        List<ClientPOCO> ListPOCO = new List<ClientPOCO>();
        //        using (var unitOfWork = new UnitOfWork<Client>())
        //        {
        //            listEntities = unitOfWork.Repository.GetAll().ToList();
        //            foreach (var client in listEntities)
        //            {

        //                ListPOCO.Add(new ClientPOCO
        //                             {
        //                                ClientCode = client.ClientCode,
        //                                Active = client.Active,
        //                                Address1 = client.Address1,
        //                                Address2 = client.Address2,
        //                                BirthDate = client.BirthDate,
        //                                ChargeMethod = new ChargeMethodPOCO
        //                                {
        //                                    Id = client.ChargeMethod.Id,
        //                                    Description = client.ChargeMethod.Description

        //                                },
        //                                ChargeOverCost = client.ChargeOverCost,




        //                });
        //            }

        //            //Mapper.CreateMap<Client, ClientPOCO>();
        //            //ListPOCO = Mapper.Map<List<Client>, List<ClientPOCO>>(listEntities);
        //        }
        //        return ListPOCO;
        //    }


        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

    }
}
