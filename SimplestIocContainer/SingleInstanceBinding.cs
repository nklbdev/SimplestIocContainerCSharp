using System;

namespace SimplestIocContainer
{
    internal class SingleInstanceBinding : Binding
    {
        public SingleInstanceBinding(Func<Container, object> resolver) : base(resolver)
        {
        }

        public override object Resolve(Container container)
        {
            return Resolver(container);
        }
    }
}