namespace DTO.BrokerRequests
{
    public class InternalEditUserInfoRequest
    {
        public User User { get; set; }
        public PasswordHashChangeRequest UserPasswords { get; set;}
        public UserAvatar UserAvatar { get; set; }
    }
}
