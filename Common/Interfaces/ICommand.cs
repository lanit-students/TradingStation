using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICommand<T>
    {
        Task<T> Execute();
    }

    public interface ICommand<TIn, TOut>
    {
        Task<TOut> Execute(TIn parameter);
    }
}
