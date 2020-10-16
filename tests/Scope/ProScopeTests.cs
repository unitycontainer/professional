using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
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

        [TestMethod]
        public void AddMemoryTest()
        {
            // Arrange
            ReadOnlyMemory<RegistrationDescriptor> memory = Registrations;

            // Act
            Scope.AddAsync(memory);

            // Validate
            Assert.AreEqual(5995, Scope.Version);
            Assert.AreEqual(5995, Scope.Count);
            Assert.AreEqual(5995, Scope.ToArray().Length);
        }
    }
}
