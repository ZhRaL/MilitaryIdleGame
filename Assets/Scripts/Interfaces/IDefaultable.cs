namespace Interfaces
{
    public interface IDefaultable<T>
    {
        public T CreateDefault { get; }
    }
}