
namespace Unity.Benchmarks.Pro
{
    public class ContainerBasicsPro : UnityContainerAPI
    {
        public override void GlobalSetup()
        {
            base.GlobalSetup();
            Container.AddExtension(Professional.Setup);
        }
    }
}
