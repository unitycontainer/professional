using System;
using System.Threading.Tasks;

namespace Unity.Container
{
    public partial class ContainerScopeAsync : ContainerScope
    {
        public override Task RegisterAsync(Type type, RegistrationManager manager)
        {
            throw new NotImplementedException();
        }

        public override Task RegisterAsync(Type[] types, RegistrationManager manager)
        {
            throw new NotImplementedException();
        }

        public override Task RegisterAsync(Type type, string name, RegistrationManager manager)
        {
            throw new NotImplementedException();
        }

        public override Task RegisterAsync(Type[] types, string name, RegistrationManager manager)
        {
            throw new NotImplementedException();
        }
    }
}
