using System.Data.Entity;
using AppCore.DataAccess.Repositories.Bases;

namespace AppCore.DataAccess.Repositories
{
    // Servisler tarafından kullanılan, RepositoryBase'den türeyen ve RepositoryBase içinde DB context'in atanmasını sağlayan class
    public class Repository<TEntity> : RepositoryBase<TEntity> where TEntity : ObligatoryProperities, new()
    {
        public Repository(DbContext dbParameter) : base(dbParameter)
        {
            
        }
    }
}
