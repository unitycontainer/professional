using System;
using Unity.Container;
using Unity.Extension;

namespace Unity
{
    public class Professional : UnityContainerExtension
    {
        protected override void Initialize() => 
            Setup(Context ?? throw new InvalidOperationException());

        public static void Setup(ExtensionContext context)
        {
            context.Container._scope = new ContainerScopeAsync((ContainerScope)context.Container._scope);
        }
    }
}
