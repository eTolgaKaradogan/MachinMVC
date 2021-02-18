using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace AppCore.DataAccess.Repositories.Bases
{
    // RepositoryBase class'ının esas oluşturulması gereken proje, diğer tüm projeler tarafından ortak kullanılabilecek bir projedir (Core).
    // Servisler tarafından kullanılan ve EntityFramework üzerinden oluşturulmuş entity'ler üzerinde CRUD işlemleri yapılmasını sağlayan base class 
    // https://www.gokhan-gokalp.com/en/generic-repository-ve-unit-of-work-kullanarak-basic-infrastructure-tasarlamak/
    public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : ObligatoryProperities, new()
    {
        private readonly DbContext db;

        protected RepositoryBase(DbContext dbParameter)
        {
            db = dbParameter;
        }

        // Herhangi bir filtre olmadan tüm entity'leri liste olarak döner
        public virtual List<TEntity> GetEntities()
        {
            try
            {
                return db.Set<TEntity>().ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen lambda expression'a göre entity'leri liste olarak döner 
        public virtual List<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return db.Set<TEntity>().Where(predicate).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Lazy loading yapılmayan durumlarda entity için herhangi bir kayıt döndürmeyen bir LINQ query oluşturur, opsiyonel parametre olarak gönderilen bu entity'ye bağlı entity'leri query'ye ekler ve bu query'i döner
        public virtual IQueryable<TEntity> GetEntityQuery(params string[] entitiesToInclude)
        {
            try
            {
                DbQuery<TEntity> entityQuery = db.Set<TEntity>();
                foreach (string entityToInclude in entitiesToInclude)
                {
                    entityQuery = entityQuery.Include(entityToInclude);
                }
                return entityQuery.AsQueryable();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen lambda expression'a göre entity için kayıt döndürmeyen bir LINQ query oluşturur, opsiyonel parametre olarak gönderilen bu entity'ye bağlı entity'leri query'ye ekler ve bu query'i döner 
        public virtual IQueryable<TEntity> GetEntityQuery(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            try
            {
                DbQuery<TEntity> entityQuery = db.Set<TEntity>();
                foreach (string entityToInclude in entitiesToInclude)
                {
                    entityQuery = entityQuery.Include(entityToInclude);
                }
                return entityQuery.AsQueryable().Where(predicate);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Primary key olan ID'ye göre tek bir entity döner
        public virtual TEntity GetEntityById(int id)
        {
            try
            {
                return db.Set<TEntity>().Find(id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen lambda expression'a göre tek bir entity döner
        public virtual TEntity GetEntity(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return db.Set<TEntity>().SingleOrDefault(predicate);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Parametre olarak gönderilen entity'i DB set'e ekler
        public virtual void AddEntity(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        // Parametre olarak gönderilen entity'i DB set'te güncellemek için DB set'teki entity'nin durumunu değiştirir
        public virtual void UpdateEntity(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        // Primary key olan ID'ye sahip entity'i DB set'ten siler. Eğer entity'de IsDeleted adında bir özellik varsa entity'i silmez, IsDeleted'ı true olarak günceller 
        public virtual void DeleteEntity(int id)
        {
            var entity = db.Set<TEntity>().Find(id);
            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                TEntity _entity = entity;
                _entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);
                UpdateEntity(_entity);
            }
            else
            {
                db.Set<TEntity>().Remove(entity);
            }
        }

        // Parametre olarak gönderilen entity'i DB set'ten siler. Eğer entity'de IsDeleted adında bir özellik varsa entity'i silmez, IsDeleted'ı true olarak günceller 
        public virtual void DeleteEntity(TEntity entity)
        {
            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                TEntity _entity = entity;
                _entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);
                UpdateEntity(_entity);
            }
            else
            {
                db.Set<TEntity>().Remove(entity);
            }
        }

        // Unit of Work için SaveChanges() methodu: Repository üzerindeki değişikliklerin veritabanına tek seferde kaydedildiği method
        public virtual int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            db.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
