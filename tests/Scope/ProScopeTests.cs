using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.BuiltIn;
using Unity.Container;

namespace Container.Scope
{
    [TestClass]
    public partial class ProScopeTests : ScopeTests
    {
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();

            Scope = new ProfessionalScope((ContainerScope)Scope);
        }

        [ClassInitialize]
        public static void InitializeAsyncClass(TestContext context)
        {
            InitializeClass(context);
        }
    }
}
