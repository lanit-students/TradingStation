namespace DataBaseService.Interfaces
{
    public interface ICommand<I>
    {
        void Execute(I Data);
    }
    public interface ICommand<in I, out T>
    {
        T Execute(I data);
    }    
}
