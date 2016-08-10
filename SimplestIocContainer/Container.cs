using System;
using System.Collections.Generic;

namespace SimplestIocContainer
{
    public class Container : IContainer
    {
        private readonly Dictionary<object, IBinding> _bindings = new Dictionary<object, IBinding>();

        public IContainer Bind(object key, Func<IContainer, object> resolver, bool isSingleInstance = true)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));

            if (isSingleInstance)
                _bindings[key] = new SingleInstanceBinding(resolver);
            else
                _bindings[key] = new MultipleInstanceBinding(resolver);

            return this;
        }

        public IContainer Bind(object key, Func<object> resolver, bool isSingleInstance = true)
        {
            return Bind(key, c => resolver(), isSingleInstance);
        }

        public IContainer Bind<T>(Func<IContainer, T> resolver, bool isSingleInstance = true)
        {
            return Bind(typeof (T), c => resolver(c), isSingleInstance);
        }

        public IContainer Bind<T>(Func<T> resolver, bool isSingleInstance = true)
        {
            return Bind(c => resolver(), isSingleInstance);
        }

        public T Resolve<T>(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            IBinding binding;
            if (!_bindings.TryGetValue(key, out binding))
                throw new InvalidOperationException($"IoC: Cannot resolve key \"{key}\"");

            return (T) binding.Resolve(this);
        }

        public T Resolve<T>()
        {
            return Resolve<T>(typeof (T));
        }
    }
}
