using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Scope;

namespace Container.Scope
{
    [TestClass]
    public class ScopeAsyncTests : ScopeTests
    {
        [TestInitialize]
        public override void InitializeTest() => Scope = new ContainerScopeAsync(Container);
    }
}
