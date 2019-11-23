using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDashboard.Core.Common.Repository
{
    public interface IEntityFrameworkRepository<TEntity> : IDisposable
             where TEntity : class
    {
        TEntity Add(TEntity Entity);

        TEntity Update(TEntity Entity);

        void Delete(TEntity Entity);

        TEntity GetById(object KeyValue);

        IQueryable<TEntity> GetAll();

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
