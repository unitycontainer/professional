using System;
using System.Diagnostics;
using System.Threading;

namespace Unity.Storage
{
    public class DoubleSet<TData>
    {
        #region Constants

        public const int SIZE_DATA      = 32;
        public const int SIZE_METADATA  = 17;
        public const int SIZE_DIRECTORY = 2;

        #endregion


        #region Data Fields

        private object _sync = new object();

        private int _entriesCount;
        private TData[] _entries;
        
        private int _archiveCount = 1;
        private TData[][] _archive;

        #endregion

        #region Metadata Fields

        private DirectorySegment[] _directory;

        #endregion


        #region Constructors

        public DoubleSet()
        {
            _entries = new TData[SIZE_DATA];
            _archive = new[] { _entries };

            _directory = new DirectorySegment[SIZE_DIRECTORY];
            for (var i = 0; i < SIZE_DIRECTORY; i++)
            {
#if DEBUG
                _directory[i] = new DirectorySegment(SIZE_METADATA, i);
#else
                _directory[i] = new DirectorySegment(SIZE_METADATA);
#endif
            }
        }

        #endregion


        #region Public

        public bool Add(in TData data)
        {
            var directory = _directory;

            var hash = (uint)(data?.GetHashCode() ?? 0);
            var slot = hash % directory.Length;

            ref var section = ref directory[slot];
            ref var basket  = ref section.Metadata[hash % section.Length];

            var position = basket.Position;

            while (position > 0)
            {
                ref var meta = ref section.Metadata[position];
                ref var entry = ref meta.Buffer[meta.Index];

                if ((null == entry && null == data) || (entry?.Equals(data) ?? false))
                {
                    return false;
                }

                position = basket.Next;
            }

            // Add new registration
            if (0 == position)
            {
                var entries = _entries;
                var entry   = Interlocked.Increment(ref _entriesCount);

                while (_entries.Length <= entry)
                {
                    ExpandEntries();

                    entries = _entries;
                    entry   = Interlocked.Increment(ref _entriesCount);
                }

                entries[entry] = data;

                var metaIndex = Interlocked.Increment(ref section.Position);
                while (section.Length <= metaIndex)
                {
                    ExpandMetadata(slot);
                }

                ref var meta = ref section.Metadata[metaIndex];

                meta.Hash = hash;
                meta.Index = entry;
                meta.Buffer = entries;

                meta.Next = basket.Position;
                meta.Next = Interlocked.Exchange(ref basket.Position, metaIndex);
            }

            return true;
        }


        public bool Add(int hashCode, in TData data)
        {
            return false;
        }

        #endregion


        private void ExpandMetadata(long slot)
        { 
        }

        private void ExpandEntries()
        {
            lock (_sync)
            {
                if (_entries.Length <= _entriesCount)
                {
                    if (_archive.Length <= _archiveCount) 
                        Array.Resize(ref _archive, Prime.Numbers[Prime.IndexOf(_archive.Length)]);

                    _archive[_archiveCount++] = _entries;
                    _entries = new TData[SIZE_DATA];
                    _entriesCount = 0;
                }
            }
        }


        /// <summary>
        /// Internal metadata structure for hash sets and lists
        /// </summary>
        [DebuggerDisplay("Buffer = { Buffer }, Position = { Position }, Next = { Next }")]
        public struct MetadataBin
        {
            public uint Hash;
            public int Next;
            public int Position;

            public int Index;
            public TData[] Buffer;
        }


        [DebuggerDisplay("Length = { Length }, Position = {Position}, Metadata = { Metadata.Length }")]
        public struct DirectorySegment
        {
            public int Length;
            public int Position;
            public object SyncRoot;
            public MetadataBin[] Metadata;

#if DEBUG
            public readonly int ID;
            public DirectorySegment(int size, int index)
            {
                ID = index;
#else
            public DirectorySegment(int size)
            {
#endif
                Length = size;
                Position = 0;
                SyncRoot = new object();
                Metadata = new MetadataBin[Length];
            }
        }

    }
}
