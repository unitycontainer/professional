using System;
using System.Threading;
using Unity.Storage;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        protected int Add(in Contract contract, RegistrationManager manager)
        {
            var hash = (uint)contract.HashCode;
            var meta = _registryMeta;
            var target = hash % meta.Length;
            var index = meta[target].Position;

            // Check for existing
            while (index > 0)
            {
                ref var candidate = ref _registryData[index];

                if (candidate._contract.Type == contract.Type && ReferenceEquals(
                    candidate._contract.Name, contract.Name))
                {
                    // Found existing
                    _registryLock.EnterReadLock();
                    _registryData[index] = new ContainerRegistration(in candidate._contract, manager);
                    _registryLock.ExitReadLock();

                    Interlocked.Increment(ref _version);
                    return 0;
                }

                index = meta[index].Next;
            }

            // Add new registration
            index = Interlocked.Increment(ref _registryCount);

            // Lock as required
            if (index < _registryMeta.MaxIndex())
            {
                _registryLock.EnterReadLock();

                // Add new registration to the registry
                _registryData[index] = new ContainerRegistration(in contract, manager);

                // Update metadata if changed while waited
                if (meta.Length != _registryMeta.Length)
                {
                    meta = _registryMeta;
                    target = hash % meta.Length;
                }
                _registryLock.ExitReadLock();
            }
            else
            {
                _registryLock.EnterWriteLock();

                // Check again and expand if still short on capacity
                if (index >= _registryMeta.MaxIndex()) ExpandRegistry(index);

                // Add new registration to the registry
                _registryData[index] = new ContainerRegistration(in contract, manager);

                // Update metadata pointers
                meta = _registryMeta;
                target = hash % meta.Length;

                _registryLock.ExitWriteLock();
            }

            ref var bucket = ref meta[target];
            ref var entry = ref meta[index];

            // Update metadata
            do { entry.Next = bucket.Position; }
            while (entry.Next != Interlocked.CompareExchange(ref bucket.Position, index, entry.Next));

            // Increment version
            Interlocked.Increment(ref _version);

            return index;
        }


        protected virtual void ExpandRegistry(int required)
        {
            var size = Prime.GetNext((int)(required * ReLoadFactor));

            // Create new metadata
            var registryMeta = new Metadata[size];
            registryMeta.Setup(LoadFactor);

            // Resize registrations buffer
            Array.Resize(ref _registryData, registryMeta.GetCapacity());

            // Rebuild buckets
            for (var current = START_INDEX; current <= _registryCount; current++)
            {
                var target = (uint)_registryData[current]._contract.HashCode % size;
                ref var bucket = ref registryMeta[target];

                registryMeta[current].Next = bucket.Position;
                bucket.Position = current;
            }

            // Update metadata
            _registryMeta = registryMeta;
        }
    }
}
