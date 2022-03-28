namespace luxclusif.user.application.Interfaces
{
    public interface IMessageSenderInterface
    {
        Task Send(string name, object data);
    }
}
