using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Container
{
    public partial class ConcurrentScope : Scope
    {
        ///<inheritdoc/>
        public override IEnumerable<ContainerRegistration> Registrations => Enumerable.Empty<ContainerRegistration>();

        public override int Contracts => 0;

        public override int Names => 0;

        public override Scope CreateChildScope()
            => new ConcurrentScope(this);

        public override bool Contains(Type type)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(Type type, string name)
        {
            throw new NotImplementedException();
        }

        public override void Add(in RegistrationData data)
        {
            throw new NotImplementedException();
        }
    }
}
