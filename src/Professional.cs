using System;
using Unity.Container;
using Unity.Extension;

namespace Unity
{
    public class Professional : UnityContainerExtension
    {
        #region Constants

        private const string ERROR_REGISTRATIONS =
            "Professional extension must be installed before anything is registered with the container";

        #endregion


        protected override void Initialize() => 
            Setup(Context ?? throw new InvalidOperationException());

        public static void Setup(ExtensionContext context)
        {
            var scope = context.Container._scope;

            if (UnityContainer.BuiltInContracts < scope.Version) 
                throw new InvalidOperationException(ERROR_REGISTRATIONS);

            context.Container._scope = new ContainerScopeAsync(scope);
        }
    }
}
