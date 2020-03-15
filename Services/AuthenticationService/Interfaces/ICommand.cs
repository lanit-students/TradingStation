namespace AuthenticationService.Interfaces
{
    /// <summary>
    /// Basic interface for a command that returns a result.
    /// </summary>
    public interface ICommand<in Input, out Output>
    {
        Output Execute(Input data);
    }

    /// <summary>
    /// Basis interface for a command without result.
    /// </summary>
    public interface ICommand<Input>
    {
        void Execute(Input data);
    }
}
