namespace UserService.Interfaces
{
    public interface ICreateUser<in Input, out Output>
    {
        public Output Execute(Input email, Input password);
    }
}
