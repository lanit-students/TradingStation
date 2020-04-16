namespace DTO.BrokerRequests
{
    public class InternalCreateUserRequest
    {
        public User User { get; set; }
        public UserCredential Credential { get; set; }
        public UserAvatar UserAvatar { get; set; }
    }
}
