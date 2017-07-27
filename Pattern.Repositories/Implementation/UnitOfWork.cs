using Pattern.Common.Contract;
using Pattern.DataContext.Contract;
using Pattern.Repositories.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContext _context;
        private readonly IUserProfile _userProfile;

        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(IDbContext context, IUserProfile userProfile)
        {
            _context = context;
            _userProfile = userProfile;
        }

        public void Save()
        {
            var changeTracker = _context.GetChangeTracker();

            var _added = changeTracker.Entries<IAuditedEntity>()
                                         .Where(p => p.State == EntityState.Added)
                                         .Select(p => p.Entity);

            foreach (IAuditedEntity e in _added)
            {
                e.Created = DateTime.UtcNow;
                e.CreatedBy = _userProfile.UserName;
                e.CreatedById = _userProfile.UserId;

                e.LastUpdated = DateTime.UtcNow;
                e.LastUpdatedBy = _userProfile.UserName;
                e.LastUpdatedById = _userProfile.UserId;
            }

            var _modified = changeTracker.Entries<IAuditedEntity>()
                  .Where(p => p.State == EntityState.Modified)
                  .Select(p => p.Entity);

            foreach (IAuditedEntity e in _modified)
            {
                e.LastUpdated = DateTime.UtcNow;
                e.LastUpdatedBy = _userProfile.UserName;
                e.LastUpdatedById = _userProfile.UserId;
                e.ModificationNumber = e.ModificationNumber + 1;
            }
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            try
            {
                var changeTracker = _context.GetChangeTracker();

                var _added = changeTracker.Entries<IAuditedEntity>()
                                             .Where(p => p.State == EntityState.Added)
                                             .Select(p => p.Entity);

                foreach (IAuditedEntity e in _added)
                {
                    e.Created = DateTime.UtcNow;
                    e.CreatedBy = _userProfile.UserName;
                    e.CreatedById = _userProfile.UserId;

                    e.LastUpdated = DateTime.UtcNow;
                    e.LastUpdatedBy = _userProfile.UserName;
                    e.LastUpdatedById = _userProfile.UserId;
                }


                var _modified = changeTracker.Entries<IAuditedEntity>()
                      .Where(p => p.State == EntityState.Modified)
                      .Select(p => p.Entity);

                foreach (IAuditedEntity e in _modified)
                {
                    e.LastUpdated = DateTime.UtcNow;
                    e.LastUpdatedBy = _userProfile.UserName;
                    e.LastUpdatedById = _userProfile.UserId;
                    e.ModificationNumber = e.ModificationNumber + 1;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveUnauditedAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }
    }
}
