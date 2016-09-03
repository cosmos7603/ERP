using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Grid;
using BLL.Helpers;
using DAL;
using Entities.POCOEntities;

namespace BLL.Abstract
{
    public abstract class ManagerBase<TPoco,TEntity> : IManager<TPoco>
        where TPoco : EntityPOCO
        where TEntity : class 
    {

        //protected ManagerBase()
        //{
        //    AutoMapperWebConfiguration.Configure();

        //}
        //protected ManagerBase()
        //{
        //    AutoMapperWebConfiguration.Configure();

        //}

        public virtual IEnumerable<TPoco> GetAll()
        {
            try
            {
                List<TEntity> listEntities;
                List<TPoco> ListPOCO;
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {
                    listEntities = unitOfWork.Repository.GetAll().ToList();
                    //Mapper.CreateMap<TEntity,TPoco>();
                    ListPOCO = Mapper.Map<List<TEntity>, List<TPoco>>(listEntities);
                }
                return ListPOCO;
            }


            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual bool Add(TPoco poco)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {
                    
                    TEntity entity = Mapper.Map<TPoco, TEntity>(poco);
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

        public virtual bool Update(TPoco poco, int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {
                    var entity = unitOfWork.Repository.GetById(id);
                    Mapper.Map(poco, entity);
                    unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool Delete(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {

                    TEntity entity = unitOfWork.Repository.GetById(id);
                    unitOfWork.Repository.Delete(entity);
                    unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<TPoco> GetBy(List<Filter> filters)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {
                    

                    var query = unitOfWork.Repository.GetAll()
                        .AsQueryable()
                        .Where(filters);

                    var resultList = query.ToList();

                    Mapper.CreateMap<TEntity, TPoco>();
                    return Mapper.Map<List<TEntity>, List<TPoco>>(resultList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<TPoco> GetPaged(List<Filter> filters, SortOptions sort, int page, int rows, out int totalCount)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {
                    totalCount = unitOfWork.Repository.GetAll().AsQueryable().Where(filters).Count();

                    var query = unitOfWork.Repository.GetAll()
                        .AsQueryable()
                        .Where(filters);
                    if (sort != null)
                    {
                        query = query.OrderBy(sort);
                    }

                    var resultList = query.Skip((page - 1) * rows).Take(rows).ToList();

                    Mapper.CreateMap<TEntity,TPoco>();
                    return Mapper.Map<List<TEntity>, List<TPoco>>(resultList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPoco GetById(int id)
        {
            try
            {
                var poco = (TPoco) Activator.CreateInstance(typeof(TPoco));
                using (var unitOfWork = new UnitOfWork<TEntity>())
                {
                    TEntity entity = unitOfWork.Repository.GetById(id);
                    Mapper.CreateMap<TPoco,TEntity>();
                     poco = Mapper.Map<TEntity, TPoco>(entity);
                    Mapper.AssertConfigurationIsValid();

                }
                return poco;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }


}
