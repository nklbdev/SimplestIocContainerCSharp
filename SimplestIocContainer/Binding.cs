using System;

namespace SimplestIocContainer
{
    internal abstract class Binding : IBinding
    {
        protected readonly Func<Container, object> Resolver;

        protected Binding(Func<Container, object> resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));
            Resolver = resolver;
        }

        public abstract object Resolve(Container container);
    }
}