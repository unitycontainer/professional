using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Container.Interfaces
{
    [TestClass]
    public class ProInterfacesTests : UnityInterfacesTests
    {
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();
            Container.WithProfessionalExtension();
        }

        [TestMethod]
        public override async Task RegisterAsync_Array()
        {
            // Act
            await Container.RegisterAsync(Registrations);

            // Validate
            Assert.AreEqual(5998, Container.Registrations.ToArray().Length);
            Assert.AreEqual(0, count);
            Assert.AreEqual(0, called);
            Assert.IsNull(sender);
        }

        [TestMethod]
        public async Task RegisterAsync_Array_Event()
        {
            // Arrange
            Container.AddExtension(ExtensionMethod);

            // Act
            await Container.RegisterAsync(Registrations);

            // Validate
            Assert.AreEqual(5998, Container.Registrations.ToArray().Length);
            Assert.AreEqual(Registrations.Length, count);
            Assert.AreEqual(1, called);
            Assert.AreSame(Container, sender);
        }

        [TestMethod]
        public override async Task RegisterAsync_Memory()
        {
            // Arrange
            var rent = ArrayPool<RegistrationDescriptor>.Shared.Rent(Registrations.Length);
            Array.Copy(Registrations, rent, Registrations.Length);
            ReadOnlyMemory<RegistrationDescriptor> memory = new ReadOnlyMemory<RegistrationDescriptor>(rent);

            // Act
            await Container.RegisterAsync(memory.Slice(0, Registrations.Length));
            ArrayPool<RegistrationDescriptor>.Shared.Return(rent);

            // Validate
            Assert.AreEqual(5998, Container.Registrations.ToArray().Length);
            Assert.AreEqual(0, count);
            Assert.AreEqual(0, called);
            Assert.IsNull(sender);
        }

        [TestMethod]
        public async Task RegisterAsync_Memory_Event()
        {
            // Arrange
            var rent = ArrayPool<RegistrationDescriptor>.Shared.Rent(Registrations.Length);
            Array.Copy(Registrations, rent, Registrations.Length);
            ReadOnlyMemory<RegistrationDescriptor> memory = new ReadOnlyMemory<RegistrationDescriptor>(rent);
            Container.AddExtension(ExtensionMethod);

            // Act
            await Container.RegisterAsync(memory.Slice(0, Registrations.Length));
            ArrayPool<RegistrationDescriptor>.Shared.Return(rent);

            // Validate
            Assert.AreEqual(5998, Container.Registrations.ToArray().Length);
            Assert.AreEqual(Registrations.Length, count);
            Assert.AreEqual(1, called);
            Assert.AreSame(Container, sender);
        }
    }
}
