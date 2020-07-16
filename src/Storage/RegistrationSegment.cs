using System.Buffers;

namespace Unity.Storage
{
    public class RegistrationSegment 
    {
        #region Fields

        private Registration[] _array;
        
        #endregion


        #region Constructors

        public RegistrationSegment(int size)
        {
            _array = new Registration[size];
        }

        #endregion

        public RegistrationSegment? Next { get; set; }
    }
}
