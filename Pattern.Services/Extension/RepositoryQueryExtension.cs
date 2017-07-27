using AutoMapper;
using Pattern.Repositories.Implementation;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Pattern.Services.Extension
{
    public static class RepositoryQueryExtension
    {
        public static IEnumerable<Model> AsEnumerable<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            var results = query.Get().AsEnumerable<Entity>();

            var models = new List<Model>();

            foreach (var result in results)
            {
                models.Add(Mapper.Map<Model>(result));
            }

            return models;
        }

        public static List<Model> ToList<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<List<Model>>(query.Get().ToList());
        }
        public static async Task<List<Model>> ToListAsync<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            var results = await query.Get().ToListAsync();

            var models = new List<Model>();

            foreach (var result in results)
            {
                models.Add(Mapper.Map<Model>(result));
            }

            return models;
        }
        public static async Task<List<Model>> ToListNoTrackingAsync<Entity, Model>(this RepositoryQuery<Entity> query)
           where Model : class
           where Entity : class
        {
            var results = await query.Get().AsNoTracking().ToListAsync();

            var models = new List<Model>();

            foreach (var result in results)
            {
                models.Add(Mapper.Map<Model>(result));
            }

            return models;
        }

        public static Model FirstOrDefault<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(query.Get().FirstOrDefault());
        }
        public static Model FirstOrDefaultNoTracking<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(query.Get().AsNoTracking().FirstOrDefault());
        }
        public static async Task<Model> FirstOrDefaultAsync<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(await query.Get().FirstOrDefaultAsync());
        }
        public static async Task<Model> FirstOrDefaultNoTrackingAsync<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(await query.Get().AsNoTracking().FirstOrDefaultAsync());
        }

        public static Model Single<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(query.Get().Single());
        }
        public static async Task<Model> SingleAsync<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(await query.Get().SingleAsync());
        }
        public static async Task<Model> SingleOrDefaultAsync<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(await query.Get().SingleOrDefaultAsync());
        }
        public static async Task<Model> SingleOrDefaultNoTrackingAsync<Entity, Model>(this RepositoryQuery<Entity> query)
            where Model : class
            where Entity : class
        {
            return Mapper.Map<Model>(await query.Get().AsNoTracking().SingleOrDefaultAsync());
        }
    }
}
