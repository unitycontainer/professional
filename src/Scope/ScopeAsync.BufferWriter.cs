using System;
using System.Buffers;
using Unity.Storage;

namespace Unity.Container
{
    public partial class ContainerScopeAsync : IBufferWriter<Registration>
    {
        #region Fields

        private int _reserved;

        #endregion


        public void Reserve(int count)
        {
        }

        public void Advance(int count)
        {
        }

        public Memory<Registration> GetMemory(int size = 1)
        {
            throw new NotImplementedException();
        }

        public Span<Registration> GetSpan(int size = 1)
        {
            throw new NotImplementedException();
        }
    }
}
