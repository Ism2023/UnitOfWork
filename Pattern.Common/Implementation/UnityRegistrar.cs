using Microsoft.Practices.Unity;
using Pattern.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Common.Implementation
{
    public class UnityRegistrar : IUnityRegistrar
    {
        public void Bootstrap()
        {

        }

        public void Configure(IUnityContainer container)
        {
            container.RegisterType<ISystemSettings, SystemSettings>();
        }
    }
}
