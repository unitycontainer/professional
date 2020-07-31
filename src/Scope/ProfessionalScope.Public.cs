using System;

namespace Unity.Container
{
    public partial class ProfessionalScope
    {
        #region Add

        public override void AddAsync(object? state)
        {
            if (null != state) Add(((ReadOnlyMemory<RegistrationDescriptor>)state).Span);
        }


        #endregion


        #region Child Scope

        /// <inheritdoc />
        public override Scope CreateChildScope() => new ProfessionalScope((Scope)this);

        #endregion
    }
}
