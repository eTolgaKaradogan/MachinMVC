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
    public class HatirlaticiService : IService<Hatırlatıcı, HatırlatıcıModel>
    {
        private readonly RepositoryBase<Hatırlatıcı> hatRepository;

        public HatirlaticiService(RepositoryBase<Hatırlatıcı> hatRepositoryparameter)
        {
            hatRepository = hatRepositoryparameter;
        }

        public void Add(HatırlatıcıModel model, bool saveChanges = true)
        {
            try
            {
                var entity = new Hatırlatıcı()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Details = model.Details,
                    DateTime = model.DateTime,
                    MachineId = model.MachineId,
                    To = model.To,
                    From = model.From
                };
                hatRepository.AddEntity(entity);
                if (saveChanges)
                {
                    hatRepository.SaveChanges();
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            try
            {
                hatRepository.DeleteEntity(id);
                if (saveChanges)
                {
                    hatRepository.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public HatırlatıcıModel GetById(int id)
        {
            var model = GetQuery(q => q.Id == id).FirstOrDefault();
            return model;
        }

        public IQueryable<HatırlatıcıModel> GetQuery(Expression<Func<HatırlatıcıModel, bool>> predicate = null)
        {
            try
            {
                var sorgu = hatRepository.GetEntityQuery("Machines").Select(q => new HatırlatıcıModel()
                {
                    Id = q.Id,
                    Name = q.Name,
                    Details = q.Details,
                    DateTime = q.DateTime,
                    MachineId = q.Machine.Id,
                    To=q.To,
                    From=q.From
                });
                if (predicate != null)
                {
                    sorgu = sorgu.Where(predicate);
                }
                return sorgu;
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
                return hatRepository.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void Update(HatırlatıcıModel model, bool saveChanges = true)
        {
            try
            {
                var entity = hatRepository.GetEntityById(model.Id);
                entity.Name = model.Name;
                entity.Details = model.Details;
                entity.DateTime = model.DateTime;
                entity.MachineId = model.MachineId;
                entity.To = model.To;
                entity.From = model.From;
                hatRepository.UpdateEntity(entity);
                if (saveChanges)
                {
                    hatRepository.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
