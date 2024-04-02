namespace Interfaces
{
    public interface IDefaultable<T>
    {
        public T CreateADefault { get; }
    }
}