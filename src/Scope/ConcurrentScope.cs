using System.Threading;
using Unity.Storage;

namespace Unity.Container
{
    public partial class ConcurrentScope : Scope
    {
        #region Constants

        public const float LoadFactor   = 0.72f;
        public const float ReLoadFactor = 1.55f;

        protected const int START_DATA = 4;
        protected const int START_INDEX = 1;
        protected const int HASH_CODE_SEED = 52361;

        protected const int PRIME_ROOT_INDEX = 3;
        protected const int PRIME_CHILD_INDEX = 1;

        #endregion


        #region Fields

        // Registrations
        protected int _registryCount;
        protected int _registryPrime;
        protected Metadata[] _registryMeta;
        protected ContainerRegistration[] _registryData;
        private ReaderWriterLockSlim _registryLock = new ReaderWriterLockSlim();

        // Names
        protected int _namesPrime;
        protected int _namesCount;
        protected Metadata[] _namesMeta;
        protected NameInfo[] _namesData;
        private object _namesLock = new object();

        #endregion


        #region Constructors

        internal ConcurrentScope(Scope scope)
            : base(scope)
        {
            // Names
            _namesPrime = PRIME_ROOT_INDEX;
            _namesMeta = new Metadata[Prime.Numbers[_namesPrime]];
            _namesMeta.Setup(LoadFactor);
            _namesData = new NameInfo[_namesMeta.GetCapacity()];

            // Registrations
            _registryMeta = new Metadata[Prime.Numbers[PRIME_ROOT_INDEX]];
            _registryMeta.Setup(LoadFactor);
            _registryData = new ContainerRegistration[_registryMeta.GetCapacity()];
        }

        // Copy constructor
        protected ConcurrentScope(ConcurrentScope scope)
            : base(scope)
        {
            // Names
            _namesMeta = new Metadata[Prime.Numbers[_namesPrime]];
            _namesMeta.Setup(LoadFactor);
            _namesData = new NameInfo[_namesMeta.GetCapacity()];

            // Registrations
            _registryMeta = new Metadata[Prime.Numbers[PRIME_CHILD_INDEX]];
            _registryMeta.Setup(LoadFactor);
            _registryData = new ContainerRegistration[_registryMeta.GetCapacity()];
        }

        ~ConcurrentScope() => Dispose(false);

        #endregion
    }
}
