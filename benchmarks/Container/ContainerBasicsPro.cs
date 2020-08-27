
namespace Unity.Benchmarks.Pro
{
    public class ContainerBasicsPro : UnityContainerAPI
    {
        public override void GlobalSetup()
        {
            base.GlobalSetup();
            ((UnityContainer)Container).AddExtension(Professional.Setup);
        }
    }
}
