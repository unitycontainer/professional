using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Public.API
{
    [TestClass]
    public partial class ProContainerAPI : IUnityContainer_Extensions
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.WithProfessionalExtension();
        }
    }
}
