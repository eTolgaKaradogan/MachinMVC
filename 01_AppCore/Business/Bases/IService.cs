using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AppCore.Business.Bases
{
    // Projelerde kullanılan tüm servislerin türetildiği base interface
    public interface IService<TEntity, TModel> where TEntity : ObligatoryProperities, new() where TModel : ObligatoryProperities, new()
    {
        // Repository üzerinden opsiyonel parametre olarak gönderilen lambda expression'a göre (predicate) sonuç query'sini döner
        IQueryable<TModel> GetQuery(Expression<Func<TModel, bool>> predicate = null);

        // Repository üzerinden primary key'e göre (id) tek bir sonuç döner
        TModel GetById(int id);

        // Parametre olarak gönderilen kaydı repository'e ekler ve saveChanges true ise unit of work üzerinden veritabanına kaydetme işlemini gerçekleştirir
        void Add(TModel model, bool saveChanges = true);

        // Parametre olarak gönderilen kaydı repository'de günceller ve saveChanges true ise unit of work üzerinden veritabanına kaydetme işlemini gerçekleştirir
        void Update(TModel model, bool saveChanges = true);

        // Parametre olarak gönderilen primary key'e göre (id) repository'den silme işlemi yapar ve saveChanges true ise unit of work üzerinden veritabanına kaydetme işlemini gerçekleştirir
        void Delete(int id, bool saveChanges = true);

        // Repository üzerinde yapılan toplu değişikliklerin veritabanına unit of work ile tek seferde kaydedilmesini sağlar
        int SaveChanges();
    }
}
