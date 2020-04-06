
namespace DTO
{
    public class PasswordHashChangeRequest
    {
        public string OldPasswordHash { get; set; }
        public string NewPasswordHash { get; set; }
    }
}
