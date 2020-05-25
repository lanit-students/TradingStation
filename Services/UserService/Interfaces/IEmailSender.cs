namespace UserService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string email, ISecretTokenEngine secretTokenEngine);
    }
}
