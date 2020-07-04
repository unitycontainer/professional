using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Container.Interfaces
{
    [TestClass]
    public class IUnityContainerAsyncTests : IUnityContainerTests
    {
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();
            Container.AddExtension<Professional>();
        }

    }
}
