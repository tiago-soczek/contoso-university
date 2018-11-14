using Contoso.University.Model.AccessControl;
using Contoso.University.Model.Courses;
using Microsoft.EntityFrameworkCore;

namespace Contoso.University.Infra.Shared
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }
    }
}
