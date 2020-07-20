using System;
using System.Threading;

namespace Unity.Container
{
    public partial class ContainerScopeAsync
    {
        #region Registration

        protected virtual void RegisterAnonymous(in RegistrationData data)
        {
            // Iterate and register types
            foreach (var type in data.RegisterAs)
            {
                // Skip invalid types
                if (null == type) continue;

                // Check for existing
                var hash = (uint)type.GetHashCode();

                var directory = _directoryMeta;

                ref var metaSection = ref directory[hash % directory.Length];

                var bucket = hash % metaSection.Length;

                ref var metadata = ref metaSection.Metadata[bucket];

                while (metadata.Position > 0)
                {
                    ref var candidate = ref _registrySections[metadata.Buffer].Buffer[metadata.Position];
                    if (candidate.Contract.Type == type && 0 == candidate.Identity)
                    {
                        // Found existing
                        //ReplaceManager(ref candidate, data.Manager);
                        _version += 1;
                        break;
                    }

                    metadata = ref metaSection.Metadata[metadata.Next];
                }

                lock (metaSection.SyncRoot)
                {

                }

                // Add new registration
                if (0 == metadata.Position)
                {
                    var buffer = _currentBuffer;
                    ref var segment = ref _registrySections[buffer];
                    
                    var index = segment.GetSlot();
                    while (segment.Length <= index)
                    {
                        buffer = ExpandRegistry(buffer);
                        segment = ref _registrySections[buffer];
                        index = segment.GetSlot();
                    }

                    segment.Buffer[index] = new Registry(hash, type, data.Manager);


                    index = Interlocked.Increment(ref metaSection.Tail);
                    ref var storage = ref metaSection.Metadata[index];

                    storage.Buffer = buffer;
                    storage.Next = Interlocked.Exchange(ref metadata.Position, index);
                    // TODO: overflow
                    //metaSection.Metadata[indexMeta].Next = metadata.Position;
                    //metadata.Position.Position = _registryCount;


                    _version += 1;
                }
            }
        }

        protected virtual void RegisterContracts(in RegistrationData data)
        {
            //var nameHash = (uint)data.Name!.GetHashCode();
            //var nameIndex = IndexOf(nameHash, data.Name, data.RegisterAs.Length);

            //lock (_registrySync)
            //{
            //    ref var references = ref _identityData[nameIndex].References;
            //    var referenceCount = references[0];
            //    var requiredLength = data.RegisterAs.Length + referenceCount;

            //    // Expand references if required
            //    if (requiredLength >= references.Length)
            //        Array.Resize(ref references, (int)Math.Round(requiredLength / LoadFactor) + 1);

            //    // Expand registry if required
            //    requiredLength = _registrations + data.RegisterAs.Length;
            //    if (requiredLength >= _registryMax) ExpandRegistry(requiredLength);

            //    // Iterate and register types
            //    foreach (var type in data.RegisterAs)
            //    {
            //        // Skip invalid types
            //        if (null == type) continue;

            //        // Check for existing
            //        var hash = type.GetHashCode(nameHash);
            //        var bucket = hash % _registryMeta.Length;
            //        var position = _registryMeta[bucket].Position;

            //        while (position > 0)
            //        {
            //            ref var candidate = ref _registryData[position];
            //            if (candidate.Contract.Type == type && nameIndex == candidate.Identity)
            //            {
            //                // Found existing
            //                ReplaceManager(ref candidate, data.Manager);
            //                _version += 1;
            //                break;
            //            }

            //            position = _registryMeta[position].Next;
            //        }

            //        // Add new registration
            //        if (0 == position)
            //        {
            //            _registryData[++_registrations] = new Registry(hash, type, data.Name, nameIndex, data.Manager);
            //            _registryMeta[_registrations].Next = _registryMeta[bucket].Position;
            //            _registryMeta[bucket].Position = _registrations;
            //            references[++referenceCount] = _registrations;
            //            _version += 1;
            //        }
            //    }

            //    // Record new count after all done
            //    references[0] = referenceCount;
            //}

            //return;
        }

        #endregion


        #region Contracts

        protected virtual int IndexOf(uint hash, string? name, int required)
        {
            throw new NotImplementedException();
            //var length = _identities;

            //// Check if already registered
            //var bucket = hash % _identityMeta.Length;
            //var position = _identityMeta[bucket].Position;
            //while (position > 0)
            //{
            //    if (_identityData[position].Name == name) return position;
            //    position = _identityMeta[position].Next;
            //}

            //lock (_contractSync)
            //{
            //    // Check again if length changed during wait for lock
            //    if (length != _identities)
            //    {
            //        bucket = hash % _identityMeta.Length;
            //        position = _identityMeta[bucket].Position;
            //        while (position > 0)
            //        {
            //            if (_identityData[position].Name == name) return position;
            //            position = _identityMeta[position].Next;
            //        }
            //    }

            //    // Expand if required
            //    if (_identities >= _identityMax)
            //    {
            //        var size = Prime.Numbers[++_contractPrime];
            //        _identityMax = (int)(size * LoadFactor);

            //        Array.Resize(ref _identityData, size);
            //        _identityMeta = new Metadata[size];

            //        // Rebuild buckets
            //        for (var current = START_INDEX; current <= _identities; current++)
            //        {
            //            bucket = _identityData[current].Hash % size;
            //            _identityMeta[current].Next = _identityMeta[bucket].Position;
            //            _identityMeta[bucket].Position = current;
            //        }

            //        bucket = hash % _identityMeta.Length;
            //    }

            //    _identityData[++_identities] = new Identity(hash, name, required + 1);
            //    _identityMeta[_identities].Next = _identityMeta[bucket].Position;
            //    _identityMeta[bucket].Position = _identities;

            //    return _identities;
            //}
        }

        #endregion
    }
}
