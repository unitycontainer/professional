using System;

namespace Unity.Container
{
    public partial class ProfessionalScope
    {
        #region Add

        public override void Add(in ReadOnlyMemory<RegistrationDescriptor> memory) => Add(memory.Span);

        #endregion


        #region Child Scope

        /// <inheritdoc />
        public override Scope CreateChildScope() => new ProfessionalScope((Scope)this);

        #endregion
    }
}
