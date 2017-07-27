using Microsoft.Practices.Unity;

namespace Pattern.Common.Contract
{
    public interface IUnityRegistrar
    {
        void Configure(IUnityContainer container);
        void Bootstrap();
    }
}
