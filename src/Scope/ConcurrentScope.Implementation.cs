using System;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        #region Expanding

        private int ExpandRegistry(int buffer)
        {
            lock (_registrySync)
            {
                // TODO: better verification?
                if (_currentBuffer != buffer) return _currentBuffer;

                var currentBuffer = _currentBuffer + 1;
                if (_registrySections.Length <= currentBuffer)
                { 
                    Array.Resize(ref _registrySections, currentBuffer + 1);
                    _registrySections[currentBuffer].Buffer = new Registry[Primes[_registrySections.Length]];
                }
                _currentBuffer = currentBuffer;
            }

            return _currentBuffer;
        }


        #endregion
    }
}
