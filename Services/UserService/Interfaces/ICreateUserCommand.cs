using DTO;

namespace UserService.Interfaces
{
    public interface ICreateUserCommand
    {
        void Execute(UserEmailPassword userEmailPassword);
    }
}
