namespace DataBaseService.Interfaces
{
    public interface IRepository<Input>
    {
        void Create(Input data);
    }
}
