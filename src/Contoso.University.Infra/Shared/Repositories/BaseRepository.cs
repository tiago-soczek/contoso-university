using System.Threading.Tasks;
using Contoso.University.Model.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Zek.Model;

namespace Contoso.University.Infra.Shared.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext db;
        private readonly DbSet<T> dbSet;

        public BaseRepository(DataContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public Task Insert(T entity)
        {
            dbSet.Update(entity);

            return db.SaveChangesAsync();
        }
    }
}
