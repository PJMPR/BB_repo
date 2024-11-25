using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBuddy.Data.Abstractions
{
    public interface IRepository<TEntity, TId> where TEntity : class, IHaveId<TId>
    {
        IQueryable<TEntity> Entities { get; }
        Task SaveChangesAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
