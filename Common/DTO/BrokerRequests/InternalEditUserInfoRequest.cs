
namespace DTO.BrokerRequests
{
    public class InternalEditUserInfoRequest
    {
        public User User { get; set; }
        public PasswordChangeRequest UserPasswords { get; set;}
    }
}
