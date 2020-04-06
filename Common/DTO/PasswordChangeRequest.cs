
namespace DTO
{
    public class PasswordChangeRequest
    {
        public string OldPasswordHash { get; set; }
        public string NewPasswordHash { get; set; }
    }
}
