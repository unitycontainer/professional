﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using Unity.BuiltIn;
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

            Scope = new ProfessionalScope((ContainerScope)Scope);
        }

        [ClassInitialize]
        public static void InitializeAsyncClass(TestContext context)
        {
            InitializeClass(context);
        }

        [TestMethod]
        public override void AddMemoryTest()
        {
            // Arrange
            ReadOnlyMemory<RegistrationDescriptor> memory = Registrations;

            // Act
            Scope.Add(memory);

            // Validate
            Assert.AreEqual(100, Scope.Names);
            Assert.AreEqual(5995, Scope.Version);
            Assert.AreEqual(5995, Scope.Contracts);
            Assert.AreEqual(5995, Scope.ToArray().Length);
        }
    }
}
