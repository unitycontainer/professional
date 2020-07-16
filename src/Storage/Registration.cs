using System;
using System.Diagnostics;

namespace Unity.Storage
{
    [DebuggerDisplay("Identity = { Identity }, Manager = {Manager}", Name = "{ (Contract.Type?.Name ?? string.Empty),nq }")]
    public struct Registration
    {
        public readonly uint Hash;
        public readonly int Identity;
        public readonly Contract Contract;
        public RegistrationManager Manager;

        public Registration(uint hash, Type type, RegistrationManager manager)
        {
            Hash = hash;
            Contract = new Contract(type);
            Manager = manager;
            Identity = 0;
        }

        public Registration(uint hash, Type type, string name, int identity, RegistrationManager manager)
        {
            Hash = hash;
            Contract = new Contract(type, name);
            Manager = manager;
            Identity = identity;
        }
    }
}
