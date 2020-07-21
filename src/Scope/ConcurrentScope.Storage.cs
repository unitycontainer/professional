using System;
using System.Diagnostics;
using System.Threading;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        [DebuggerDisplay("Buffer = { Buffer.Length }, Position = {Position}")]
        public struct RegistrySection
        {
            public Registry[] Buffer;
            public int Tail;
            public int Length => Buffer.Length;


            public int GetSlot() => Interlocked.Increment(ref Tail);

            public RegistrySection(int size)
            {
                Buffer = new Registry[size];
                Tail = 0;
            }
        }

        [DebuggerDisplay("Length = { Length }, Position = {Position}, Metadata = { Metadata.Length }")]
        public struct MetadataSection
        {
            public int Length;
            public int Tail;
            public object SyncRoot;
            public Metadata[] Metadata;

            public int GetSlot() => Interlocked.Increment(ref Tail);

            public MetadataSection(int size)
            {
                Length = size;
                Tail = 0;
                SyncRoot = new object();
                Metadata = new Metadata[Length];
            }
        }

        /// <summary>
        /// Internal metadata structure for hash sets and lists
        /// </summary>
        [DebuggerDisplay("Buffer = { Buffer }, Position = { Position }, Next = { Next }")]
        public struct Metadata
        {
            public int Next;
            public int Buffer;
            public int Position;
        }

        [DebuggerDisplay("Identity = { Identity }, Manager = {Manager}", Name = "{ (Contract.Type?.Name ?? string.Empty),nq }")]
        public struct Registry
        {
            public readonly uint Hash;
            public readonly int Identity;
            public readonly Contract Contract;
            public RegistrationManager Manager;

            public Registry(uint hash, Type type, RegistrationManager manager)
            {
                Hash = hash;
                Contract = new Contract(type);
                Manager = manager;
                Identity = 0;
            }

            public Registry(uint hash, Type type, string name, int identity, RegistrationManager manager)
            {
                Hash = hash;
                Contract = new Contract(type, name);
                Manager = manager;
                Identity = identity;
            }
        }

        [DebuggerDisplay("{ Name }")]
        public struct Identity
        {
            public readonly uint Hash;
            public readonly string? Name;
            public int[] References;

            public Identity(uint hash, string? name, int size)
            {
                Hash = hash;
                Name = name;
                References = new int[size];
            }
        }

    }

    public static class ContainerScopeAsyncExtensions
    { 
    
    }
}
