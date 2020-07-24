using Unity.BuiltIn;

namespace Unity.Container
{
    public partial class ProfessionalScope : ContainerScope
    {
        #region Constructors

        // Hijack constructor
        internal ProfessionalScope(ContainerScope scope)
            : base(scope)
        {
        }

        // child constructor
        protected ProfessionalScope(Scope scope)
            : base(scope)
        {
        }

        #endregion
    }
}
