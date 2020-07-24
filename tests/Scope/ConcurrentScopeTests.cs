using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Container;

namespace Container.Scope
{
    [TestClass]
    public partial class ConcurrentScopeTests : ScopeTests
    {
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();

            Scope = new ConcurrentScope(Scope);
        }

        [ClassInitialize]
        public static void InitializeAsyncClass(TestContext context)
        {
            InitializeClass(context);
        }
    }
}
