using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Unity.Container
{
    public partial class ConcurrentScope
    {
        protected override bool MoveNext(ref int index, ref ContainerRegistration registration)
        {
            index = index + 1;

            if (_registryCount < index) return false;

            //registration = _registryData[index];

            return true;
        }


        #region Hierarchy

        /// <summary>
        /// Method that creates <see cref="IUnityContainer.Registrations"/> enumerator
        /// </summary>
        public HierarchyEnumerable Hierarchy() => new HierarchyEnumerable(this);

        public struct HierarchyEnumerable : IEnumerable<ConcurrentScope>
        {
            private ConcurrentScope _containerScope;

            public HierarchyEnumerable(ConcurrentScope containerScope) => _containerScope = containerScope;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<ConcurrentScope> GetEnumerator() => new HierarchyEnumerator(_containerScope);

            public struct HierarchyEnumerator : IEnumerator<ConcurrentScope>
            {
                private ConcurrentScope  _current;
                private ConcurrentScope? _next;

                public HierarchyEnumerator(ConcurrentScope containerScope)
                {
                    _current = containerScope;
                    _next    = containerScope;
                }

                object IEnumerator.Current => Current;

                public ConcurrentScope Current => _current!;

                public bool MoveNext()
                {
                    if (null == _next) return false;
                    
                    _current = _next;
                    _next = (ConcurrentScope?)_current._next;

                    return true;
                }
                
                public void Dispose() { }

                public void Reset() => throw new NotSupportedException();
            }
        }

        #endregion
    }
}
