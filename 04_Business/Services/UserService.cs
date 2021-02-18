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
   public class UserService : IService<User, UserModel>
    {
        private readonly RepositoryBase<User> userRepository;
        public UserService(RepositoryBase<User> userRepositoryasd)
        {
            userRepository = userRepositoryasd;
        }
        public void Add(UserModel model, bool saveChanges = true)
        {
            User entity = new User()
            {
                Id = model.Id,
                Name = model.Name,
                Password = model.Password,
                RoleId = model.RoleId,
                FactoryId=model.FactoryId,
                Mail=model.Mail     
            };
            
            userRepository.AddEntity(entity);
            if (saveChanges)
            {
                userRepository.SaveChanges();
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            userRepository.DeleteEntity(id);
            if (saveChanges)
            {
                userRepository.SaveChanges();
            }
        }

        public UserModel GetById(int id)
        {
            var a = GetQuery(q => q.Id == id);
            var r = a.FirstOrDefault();
            return r;
        }

        public IQueryable<UserModel> GetQuery(Expression<Func<UserModel, bool>> predicate = null)
        {
            var sorgu = userRepository.GetEntityQuery("Roles").Select(a => new UserModel()
            {
                Id = a.Id,
                Name = a.Name,
                Password = a.Password,
                RoleId = a.Role.Id,
                FactoryId=a.Factory.Id,
                Mail=a.Mail
            });
            if (predicate != null)
            {
                sorgu = sorgu.Where(predicate);
            }
            return sorgu;
        }

        public int SaveChanges()
        {
            try
            {
                return (userRepository.SaveChanges());
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void Update(UserModel model, bool saveChanges = true)
        {
            var gelenentity = userRepository.GetEntityById(model.Id);
            gelenentity.Name = model.Name;
            gelenentity.Password = model.Password;
            gelenentity.RoleId = model.RoleId;
            gelenentity.FactoryId = model.FactoryId;
            gelenentity.Mail = model.Mail;
            userRepository.UpdateEntity(gelenentity);
            if (saveChanges)
            {
                userRepository.SaveChanges();
            }
        }
    }
}
