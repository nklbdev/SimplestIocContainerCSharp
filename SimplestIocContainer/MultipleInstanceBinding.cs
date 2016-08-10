using System;

namespace SimplestIocContainer
{
    internal class MultipleInstanceBinding : Binding
    {
        public MultipleInstanceBinding(Func<Container, object> resolver) : base(resolver)
        {
        }

        public override object Resolve(Container container)
        {
            return Resolver(container);
        }
    }
}