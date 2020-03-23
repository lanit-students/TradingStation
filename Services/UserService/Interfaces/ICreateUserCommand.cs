using DTO;


namespace UserService.Interfaces
{
    public interface ICreateUserCommand
    {
        string Execute(UserCredential userCredential);
    }
}
