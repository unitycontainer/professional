


namespace Unity.Container
{
    public partial class ContainerScopeAsync : ContainerScope
    {
        /// <summary>
        /// Scope constructor
        /// </summary>
        /// <param name="container"><see cref="UnityContainer"/> that owns the scope</param>
        internal ContainerScopeAsync(UnityContainer container) 
            : base(container)
        {
        }

        /// <summary>
        /// Copy scope constructor
        /// </summary>
        /// <param name="scope"><see cref="UnityContainer.ContainerScope"/> being replaced by 
        /// this instance</param>
        internal ContainerScopeAsync(ContainerScope scope)
            : base(scope)
        {
        }

        ///<inheritdoc/>
        public override ContainerScope CreateChildScope(UnityContainer container)
            => new ContainerScopeAsync(container);
    }
}
