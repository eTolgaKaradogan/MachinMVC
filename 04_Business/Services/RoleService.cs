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
    public class RoleService : IService<Role, RoleModel>
    {
        private readonly RepositoryBase<Role> roleRepository;

        public RoleService(RepositoryBase<Role> roleRepositoryprm)
        {
            roleRepository = roleRepositoryprm;
        }

        public void Add(RoleModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public RoleModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RoleModel> GetQuery(Expression<Func<RoleModel, bool>> predicate = null)
        {
            var sorgu=roleRepository.GetEntityQuery().Select(a => new RoleModel()
            {
                Id = a.Id,
                Name = a.Name
            });
            if (predicate!=null)
            {
                sorgu = sorgu.Where(predicate);
            }
            return sorgu;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(RoleModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
