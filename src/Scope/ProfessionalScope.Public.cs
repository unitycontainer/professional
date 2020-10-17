using System;

namespace Unity.Container
{
    public partial class ProfessionalScope
    {
        #region Child Scope

        /// <inheritdoc />
        public override Scope CreateChildScope(int capacity) => new ProfessionalScope(this, capacity);

        #endregion
    }
}
