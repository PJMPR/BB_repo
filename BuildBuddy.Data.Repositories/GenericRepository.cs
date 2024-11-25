using BuildBuddy.Data.Abstractions;

namespace BuildBuddy.Data.Repositories
{
    internal class GenericRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IHaveId<TId>
    {
        private readonly BuildBuddyDbContext ctx;

        public GenericRepository(BuildBuddyDbContext ctx)
        {
            this.ctx = ctx;
            Entities = ctx.Set<TEntity>();
        }

        public IQueryable<TEntity> Entities { get; }

        public void Add(TEntity entity)
        {
            ctx.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            ctx.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return ctx.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            ctx.Update(entity);
        }

    }
}
