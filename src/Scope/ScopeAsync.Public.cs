using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Container
{
    public partial class ContainerScopeAsync : Scope
    {
        ///<inheritdoc/>
        public override IEnumerable<ContainerRegistration> Registrations => Enumerable.Empty<ContainerRegistration>();

        public override int Contracts => 0;

        public override int Names => 0;

        public override Scope CreateChildScope()
            => new ContainerScopeAsync(this);

        public override bool IsRegistered(Type type)
        {
            throw new NotImplementedException();
        }

        public override bool IsRegistered(Type type, string name)
        {
            throw new NotImplementedException();
        }

        public override void Register(in RegistrationData data)
        {
            throw new NotImplementedException();
        }
    }
}
