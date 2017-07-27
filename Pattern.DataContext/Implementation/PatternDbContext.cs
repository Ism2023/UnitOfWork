using Pattern.Common.Contract;
using Pattern.DataContext.Contract;
using Pattern.DataContext.Entities.Base;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.DataContext.Implementation
{
    public class PatternDbContext : ContextBase, IDbContext
    {
        public PatternDbContext(ISystemSettings settings)
            : base(settings.GetConnectionString("PatternConnectionStringName")) { }

        Database IDbContext.Database
        {
            get
            {
                return base.Database;
            }
        }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public DbEntityEntry ContextEntry(object o)
        {
            return Entry(o);
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    var entityName = result.Entry.Entity.GetType().FullName;

                    foreach (var e in result.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("{0}: {1} => {2}", entityName, e.PropertyName, e.ErrorMessage));
                    }
                }

                throw new ArgumentException(sb.ToString());
            }
        }
        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    var entityName = result.Entry.Entity.GetType().FullName;

                    foreach (var e in result.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("{0}: {1} => {2}", entityName, e.PropertyName, e.ErrorMessage));
                    }
                }

                throw new ArgumentException(sb.ToString());
            }
        }

        public DbChangeTracker GetChangeTracker()
        {
            return ChangeTracker;
        }
    }
}
