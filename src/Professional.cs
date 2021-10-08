using System;
using Unity.BuiltIn;
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
            //context.Container._scope = new ProfessionalScope((ContainerScope)context.Container._scope);
        }
    }

    public static class ProfessionalExtensions
    {
        public static void WithProfessionalExtension(this UnityContainer container)
        {
            (container ?? throw new ArgumentNullException(nameof(container)))
                .AddExtension(Professional.Setup);
        }

        public static void WithProfessionalExtension(this IUnityContainer container)
        {
            (container ?? throw new ArgumentNullException(nameof(container)))
                .AddExtension(Professional.Setup);
        }

        public static void WithProfessional(this UnityContainer container)
        {
            (container ?? throw new ArgumentNullException(nameof(container)))
                .AddExtension(Professional.Setup);
        }

        public static void WithProfessional(this IUnityContainer container)
        {
            (container ?? throw new ArgumentNullException(nameof(container)))
                .AddExtension(Professional.Setup);
        }
    }
}
