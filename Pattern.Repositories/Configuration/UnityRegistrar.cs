using Microsoft.Practices.Unity;
using Pattern.Common.Contract;
using Pattern.DataContext.Contract;
using Pattern.DataContext.Implementation;
using Pattern.Repositories.Contract;
using Pattern.Repositories.Implementation;

namespace Pattern.Repositories.Configuration
{
    public class UnityRegistrar : IUnityRegistrar
    {
        public void Bootstrap()
        {

        }

        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IDbContext, PatternDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
        }
    }
}
