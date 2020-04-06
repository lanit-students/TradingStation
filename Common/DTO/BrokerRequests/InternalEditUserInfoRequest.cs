
namespace DTO.BrokerRequests
{
    public class InternalEditUserInfoRequest
    {
        public User User { get; set; }
        public UserPasswordHashChange UserPasswords { get; set;}
    }
}
