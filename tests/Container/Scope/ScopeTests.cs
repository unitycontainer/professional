using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Container;

namespace Container.Scope
{
    [TestClass]
    public class ScopeAsyncTests : ScopeTests
    {
        protected override UnityContainer GetContainer() => new UnityContainer { new Professional() };
    }
}
