using System;
using System.Diagnostics;
using Unity.Storage;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        #region Implementation

        protected ref readonly NameInfo GetNameInfo(string name)
        {
            var hash = (uint)name!.GetHashCode();
            var meta = _namesMeta;
            var count  = _namesCount;
            var target = hash % meta.Length;
            ref var bucket = ref meta[target];
            var index  = bucket.Position;

            // Optimistic search
            while (index > 0)
            {
                ref var candidate = ref _namesData[index];
                if (candidate.Hash == hash && candidate.Name == name)
                    return ref candidate;

                index = meta[index].Next;
            }

            lock (_namesLock)
            {
                // Search again if buffer has changed
                if (_namesCount != count)
                {
                    if (meta.Length != _namesMeta.Length)
                    {
                        meta   = _namesMeta;
                        target = hash % meta.Length;
                    }
                        
                    index = meta[target].Position;

                    while (index > 0)
                    {
                        ref var candidate = ref _namesData[index];
                        if (candidate.Hash == hash && candidate.Name == name)
                            return ref candidate;

                        index = meta[index].Next;
                    }
                }

                // Expand if required
                if (++_namesCount >= _namesMeta.MaxIndex())
                {
                    var size = Prime.Numbers[++_namesPrime];

                    meta = new Metadata[size];
                    meta.Setup(LoadFactor);

                    Array.Resize(ref _namesData, meta.GetCapacity());

                    // Rebuild buckets
                    for (var current = START_INDEX; current < _namesCount; current++)
                    {
                        target = _namesData[current].Hash % size;
                        bucket = ref meta[target];
                        meta[current].Next = bucket.Position;
                        bucket.Position = current;
                    }

                    target = hash % meta.Length;
                    _namesMeta = meta;
                }

                ref var entry = ref _namesData[_namesCount];
                entry = new NameInfo(hash, name);
                bucket = ref meta[target];

                meta[_namesCount].Next = bucket.Position;
                bucket.Position = _namesCount;

                return ref entry;
            }
        }

        #endregion


        #region Nested Types

        [DebuggerDisplay("{ Name }")]
        public struct NameInfo
        {
            public readonly uint Hash;
            public readonly string? Name;
            public int[] References;

            public NameInfo(uint hash, string? name)
            {
                Hash = hash;
                Name = name;
                References = new int[5];
            }
        }

        #endregion
    }
}
