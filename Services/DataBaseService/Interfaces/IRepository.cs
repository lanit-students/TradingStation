namespace DataBaseService.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T data);
    }
}
