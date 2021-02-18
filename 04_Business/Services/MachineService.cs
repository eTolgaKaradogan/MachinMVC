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
    public class MachineService : IService<Machine, MachineModel>
    {
        private readonly RepositoryBase<Machine> machineRepository;
        public MachineService(RepositoryBase<Machine> machineRepositorytr)
        {
            machineRepository = machineRepositorytr;
        }

        public void Add(MachineModel model, bool saveChanges = true)
        {
            try
            {
                var entity = new Machine()
                {
                    Name = model.Name,
                    SeriNO = model.SeriNO,
                    Model = model.Model,
                    Detail = model.Detail,
                    SectionId = model.SectionId,
                };
                machineRepository.AddEntity(entity);
                if (saveChanges)
                {
                    machineRepository.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            machineRepository.DeleteEntity(id);
            if (saveChanges)
            {
                machineRepository.SaveChanges();
            }
        }

        public MachineModel GetById(int id)
        {
            var sorgu = GetQuery(q => q.Id == id);
            var sonuc = sorgu.FirstOrDefault();
            return sonuc;
        }

        public IQueryable<MachineModel> GetQuery(Expression<Func<MachineModel, bool>> predicate = null)
        {
            var sorgu = machineRepository.GetEntityQuery().Select(q => new MachineModel()
            {
                Id = q.Id,
                Name = q.Name,
                SeriNO = q.SeriNO,
                Model = q.Model,
                Detail = q.Detail,
                SectionId = q.Section.Id
            });
            if (predicate != null)
            {
                sorgu = sorgu.Where(predicate);
            }
            return sorgu;
        }

        public int SaveChanges()
        {
            return (machineRepository.SaveChanges());
        }

        public void Update(MachineModel model, bool saveChanges = true)
        {
            var entity = machineRepository.GetEntityById(model.Id);
            entity.Model = model.Model;
            entity.Name = model.Name;
            entity.SeriNO = model.SeriNO;
            entity.Detail = model.Detail;
            entity.SectionId = model.SectionId;
            machineRepository.UpdateEntity(entity);
            if (saveChanges)
            {
                machineRepository.SaveChanges();
            }
        }
    }
}
