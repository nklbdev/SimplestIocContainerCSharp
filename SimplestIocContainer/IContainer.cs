using System;

namespace SimplestIocContainer
{
    public interface IContainer
    {
        IContainer Bind(object key, Func<IContainer, object> resolver, bool isSingleInstance = true);
        IContainer Bind(object key, Func<object> resolver, bool isSingleInstance = true);
        IContainer Bind<T>(Func<IContainer, T> resolver, bool isSingleInstance = true);
        IContainer Bind<T>(Func<T> resolver, bool isSingleInstance = true);
        T Resolve<T>(object key);
        T Resolve<T>();
    }
}