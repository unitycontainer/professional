using System;
using Unity.Storage;

namespace Unity.Container
{
    public partial class ConcurrentScope : Scope
    {
        #region Constants

        public const int DATA_BUFFER_SIZE = 37;
        public const int META_BUFFER_SIZE = 89;

        #endregion


        #region Fields

        private RegistrationSegment _head;
        private RegistrationSegment _tail;

        private int _currentBuffer;
        private RegistrySection[] _registrySections;
        private MetadataSection[] _directoryMeta;

        protected object _registrySync = new object();
        protected object _metadataSync = new object();
        protected object _contractSync = new object();


        #endregion

        /// <summary>
        /// Copy constructor
        /// </summary>
        internal ConcurrentScope(Scope scope) 
            : base(scope.Next, scope.Disposables)
        {
            // Registrations
            _tail = _head = new RegistrationSegment(37);

            _registrySections = new[] { new RegistrySection(DATA_BUFFER_SIZE) };
            _directoryMeta = new[] { new MetadataSection(META_BUFFER_SIZE) };

            // Copy data
            GC.SuppressFinalize(scope);
            foreach (var registration in scope.Registrations)
            { 
            }
        }

        /// <summary>
        /// Child scope constructor
        /// </summary>
        /// <param name="scope"><see cref="UnityContainer.ContainerScope"/> being replaced by 
        /// this instance</param>
        internal ConcurrentScope(ConcurrentScope scope)
            : base(scope)
        {
            // Registrations
            _tail = _head = new RegistrationSegment(37);

            _registrySections = new[] { new RegistrySection(DATA_BUFFER_SIZE) };
            _directoryMeta = new[] { new MetadataSection(META_BUFFER_SIZE) };
        }
    }
}
