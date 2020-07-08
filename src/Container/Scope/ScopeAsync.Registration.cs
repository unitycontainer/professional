using System.Threading;

namespace Unity.Container
{
    public partial class ContainerScopeAsync : ContainerScope
    {
        public override void RegisterAnonymous(ref RegistrationData data, CancellationToken token)
        {
            base.RegisterAnonymous(ref data, token);
        }

        public override void RegisterContracts(ref RegistrationData data, CancellationToken token)
        {
            base.RegisterContracts(ref data, token);
        }
    }
}
