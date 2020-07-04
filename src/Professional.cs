﻿using System;
using Unity.Extension;
using Unity.Scope;

namespace Unity
{
    public class Professional : UnityContainerExtension
    {
        protected override void Initialize() => 
            Setup(Context ?? throw new InvalidOperationException());

        public static void Setup(ExtensionContext context)
        {
            context.Container._scope = new ContainerScopeAsync(context.Container._scope);
        }
    }
}
