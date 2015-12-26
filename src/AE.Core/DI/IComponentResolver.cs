namespace AE.Core.DI
{
    public interface IComponentResolver : IDependency
    {
        T Resolve<T>();
    }
}