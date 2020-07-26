using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;

namespace Container.Extending
{
    [TestClass]
    public class LoadExtensionTests
    {
        [TestMethod]
        public void AddExtensionTest()
        {
            var container = new UnityContainer();

            container.AddExtension(new Professional());
        }

        [TestMethod]
        public void AddGenericTest()
        {
            var container = new UnityContainer();

            container.AddExtension<Professional>();
        }

        [TestMethod]
        public void AddTest()
        {
            var container = new UnityContainer { new Professional() };

            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void AddTypeTest()
        {
            var container = new UnityContainer { typeof(Professional) };

            Assert.IsNotNull(container);
        }
    }
}
