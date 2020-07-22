using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Lifetime;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        public override void Add(LifetimeManager manager, params Type[] registerAs)
        {
            throw new NotImplementedException();
        }
    }
}
