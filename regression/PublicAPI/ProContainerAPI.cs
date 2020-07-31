using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Container.Interfaces
{
    [TestClass]
    public partial class ProContainerAPI : UnityContainerAPI
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.WithProfessionalExtension();
        }
    }
}
