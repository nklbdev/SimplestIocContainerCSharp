using System;

namespace SimplestIocContainer
{
    internal class MultipleInstanceBinding : Binding
    {
        private object _resolvedInstance;
        private bool _isResolved;

        public MultipleInstanceBinding(Func<Container, object> resolver) : base(resolver)
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