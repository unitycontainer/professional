using System;
using System.Runtime.InteropServices;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        #region Public Members

        /// <inheritdoc />
        public override int Contracts => _registryCount;

        /// <inheritdoc />
        public override int Names => _namesCount;

        #endregion


        #region Add

        /// <inheritdoc />
        public override void Add(RegistrationManager manager, params Type[] registerAs)
        {
            foreach (var type in registerAs)
            {
                if (null == type) continue;

                Add(new Contract(type), manager);
            }
        }

        /// <inheritdoc />
        public override void Add(in ReadOnlySpan<RegistrationDescriptor> data)
        {
            for (var i = 0; data.Length > i; i++)
            {
                ref readonly RegistrationDescriptor descriptor = ref data[i];

                if (null == descriptor.Name)
                {
                    foreach (var type in descriptor.RegisterAs)
                    {
                        if (null == type) continue;

                        Add(new Contract(type), descriptor.Manager);
                    }
                }
                else
                {
                    var nameInfo = GetNameInfo(descriptor.Name);

                    foreach (var type in descriptor.RegisterAs)
                    {
                        if (null == type) continue;

                        Add(new Contract(type, nameInfo.Name), descriptor.Manager);
                    }
                }
            }
        }

        #endregion


        #region Contains

        /// <inheritdoc />
        public override bool Contains(Type type)
        {
            var scope = this;

            do
            {
                var bucket = (uint)type.GetHashCode() % scope._registryMeta.Length;
                var position = scope._registryMeta[bucket].Position;

                while (position > 0)
                {
                    ref var candidate = ref scope._registryData[position];
                    if (null == candidate._contract.Name && candidate._contract.Type == type)
                        return true;

                    position = scope._registryMeta[position].Next;
                }

            } while ((scope = (ConcurrentScope?)scope._next) != null);

            return false;
        }

        /// <inheritdoc />
        public override bool Contains(Type type, string name)
        {
            var scope = this;

            do
            {
                if (0 == scope._namesCount) continue;

                var hash = type.GetHashCode(name);
                var bucket = hash % scope._registryMeta.Length;
                var position = scope._registryMeta[bucket].Position;

                while (position > 0)
                {
                    ref var candidate = ref scope._registryData[position];
                    if (candidate._contract.Type == type && candidate._contract.Name == name)
                        return true;

                    position = scope._registryMeta[position].Next;
                }

            } while ((scope = (ConcurrentScope?)scope._next) != null);

            return false;
        }

        #endregion


        #region Child Scope

        /// <inheritdoc />
        public override Scope CreateChildScope() => new ConcurrentScope(this);

        #endregion
    }
}
