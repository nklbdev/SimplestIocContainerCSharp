namespace SimplestIocContainer
{
    internal interface IBinding
    {
        object Resolve(Container container);
    }
}