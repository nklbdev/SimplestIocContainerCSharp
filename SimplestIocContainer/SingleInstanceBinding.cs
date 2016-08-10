using System;

namespace SimplestIocContainer
{
    internal class SingleInstanceBinding : Binding
    {
        private object _resolvedInstance;
        private bool _isResolved;

        public SingleInstanceBinding(Func<Container, object> resolver) : base(resolver)
        {
        }

        public override object Resolve(Container container)
        {
            if (!_isResolved)
            {
                _resolvedInstance = Resolver(container);
                _isResolved = true;
            }

            return _resolvedInstance;
        }
    }
}