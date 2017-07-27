using Microsoft.Practices.Unity;
using Pattern.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Services.Configuration
{
    public class UnityRegistrar : IUnityRegistrar
    {
        private static IUnityContainer _container { get; set; }
        public void Bootstrap()
        {
            throw new NotImplementedException();
        }

        public void Configure(IUnityContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
