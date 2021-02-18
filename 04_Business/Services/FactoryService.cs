using _02_DataEntities.Entities;
using _04_Business.Models;
using AppCore.Business.Bases;
using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _04_Business.Services
{
    public class FactoryService : IService<Factory, FactoryModel>
    {
        private readonly RepositoryBase<Factory> factoryRepository;

        public FactoryService(RepositoryBase<Factory> factoryRepositoryparameter)
        {
            factoryRepository = factoryRepositoryparameter;
        }
        public void Add(FactoryModel model, bool saveChanges = true)
        {
            try
            {
                Factory entity = new Factory()
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                factoryRepository.AddEntity(entity);
                if (saveChanges)
                {
                    factoryRepository.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            factoryRepository.DeleteEntity(id);
            if (saveChanges)
            {
                factoryRepository.SaveChanges();
            }
        }

        public FactoryModel GetById(int id)
        {
            var sorgu = GetQuery(a => a.Id == id);
            FactoryModel sonuc = sorgu.FirstOrDefault();
            return (sonuc);
        }

        public IQueryable<FactoryModel> GetQuery(Expression<Func<FactoryModel, bool>> predicate = null)
        {
            try
            {
                var sorgu = factoryRepository.GetEntityQuery().Select(q => new FactoryModel()
                {
                    Id = q.Id,
                    Name = q.Name
                });
                if (predicate != null)
                {
                    sorgu = sorgu.Where(predicate);
                }
                return (sorgu);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return factoryRepository.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Update(FactoryModel model, bool saveChanges = true)
        {
            var databasedengelenfactory = factoryRepository.GetEntityById(model.Id);
            databasedengelenfactory.Name = model.Name;
            factoryRepository.UpdateEntity(databasedengelenfactory);
            if (saveChanges)
            {
                factoryRepository.SaveChanges();
            }
        }
    }
}
