namespace DataBaseService.Interfaces
{
    public interface ICommand<Input>
    {
        void Execute(Input Data);
    }  
    public interface ICommand<in Input, out Output>
    {
        Output Execute(Input data);
    }    
}
