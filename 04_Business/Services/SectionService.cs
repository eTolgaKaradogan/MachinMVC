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
    public class SectionService : IService<Section, SectionModel>
    {
        private readonly RepositoryBase<Section> sectionRepository;

        public SectionService(RepositoryBase<Section> sectionRepositoryParameter)
        {
            sectionRepository = sectionRepositoryParameter;
        }
        public void Add(SectionModel model, bool saveChanges = true)
        {
            try
            {
                var entity = new Section()
                {
                    Id = model.Id,
                    Name = model.Name,
                    FactoryId = model.FactoryId
                };
                if (saveChanges)
                {
                    sectionRepository.SaveChanges();
                }
                sectionRepository.AddEntity(entity);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            try
            {
                sectionRepository.DeleteEntity(id);
                if (saveChanges)
                {
                    sectionRepository.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public SectionModel GetById(int id)
        {
            var entity = GetQuery(a => a.Id == id);
            var sonuc = entity.FirstOrDefault();
            return sonuc;
        }

        public IQueryable<SectionModel> GetQuery(Expression<Func<SectionModel, bool>> predicate = null)
        {
            var sorgu = sectionRepository.GetEntityQuery("Factories").Select(f => new SectionModel()
            {
                Id = f.Id,
                Name = f.Name,
                FactoryId = f.Factory.Id
            });
            if (predicate != null)
            {
                sorgu = sorgu.Where(predicate);
            }
            return sorgu;
        }

        public int SaveChanges()
        {
            return (sectionRepository.SaveChanges());
        }

        public void Update(SectionModel model, bool saveChanges = true)
        {
            var dbdekikarşılığı = sectionRepository.GetEntityById(model.Id);
            dbdekikarşılığı.Name = model.Name;
            dbdekikarşılığı.FactoryId = model.FactoryId;
            sectionRepository.UpdateEntity(dbdekikarşılığı);
        }
    }
}
