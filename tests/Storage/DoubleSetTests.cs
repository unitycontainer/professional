using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Unity.Storage;

namespace Storage.Tests
{
    [TestClass]
    public class DoubleSetTests
    {
        #region Fields

        int HashMask = unchecked((int)(uint.MaxValue >> 1));
        protected const int SizeTypes = 1000;
        protected const int SizeNames = 100;
        protected static Type[] TestTypes = Assembly.GetAssembly(typeof(int))
                                                    .DefinedTypes
                                                    .Where(t => t != typeof(IServiceProvider))
                                                    .Distinct()
                                                    .Take(SizeTypes)
                                                    .ToArray();
        string[] TestNames = TestTypes.Take(SizeNames)
                                      .Select(t => t.Name)
                                      .ToArray();
        #endregion


        [TestMethod]
        public void Baseline()
        {
            // Arrange
            var set = new DoubleSet<Type>();

            // Validate
            Assert.IsTrue(set.Add(null));
            Assert.IsTrue(set.Add(typeof(DoubleSetTests)));
            Assert.IsFalse(set.Add(typeof(DoubleSetTests)));
        }

        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var set = new DoubleSet<Type>();

            // Validate
            foreach (var type in TestTypes) Assert.IsTrue(set.Add(type));
        }

        [TestMethod]
        public void SameHashCodeTest()
        {
            // Arrange
            var set = new DoubleSet<SameHashCode>();
            var instance = new SameHashCode();

            // Validate
            Assert.IsTrue(set.Add(instance));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsTrue(set.Add(new SameHashCode { Code = instance.Code | (-1 ^ HashMask) } ));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsTrue(set.Add(new SameHashCode()));
            Assert.IsFalse(set.Add(instance));
            Assert.IsFalse(set.Add(instance));
            Assert.IsFalse(set.Add(instance));
            Assert.IsFalse(set.Add(instance));
        }

        [TestMethod]
        public void AddSameHashCodeTest()
        {
            // Arrange
            var set = new DoubleSet<object>();
            var instance = new object();
            var code = instance.GetHashCode();
            var wrong = code | (-1 ^ HashMask);

            // Validate
            Assert.IsTrue(set.Add(code, instance));
            Assert.IsTrue(set.Add(wrong, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsTrue(set.Add(code, new object()));
            Assert.IsFalse(set.Add(code, instance));
            Assert.IsFalse(set.Add(code, instance));
            Assert.IsFalse(set.Add(code, instance));
        }

        [DebuggerDisplay("{Name}")]
        public class SameHashCode
        {
            public string Name { get; } = Guid.NewGuid().ToString();
            public int Code { get; set; } = 753951;

            public override int GetHashCode() => Code;

            public override bool Equals(object obj)
            {
                if (obj is SameHashCode other && Name == other.Name) 
                    return true;
                
                return false;
            }
        }
    }
}
