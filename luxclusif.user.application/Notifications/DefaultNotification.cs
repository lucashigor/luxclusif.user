using Hangfire;
using luxclusif.user.application.Interfaces;
using luxclusif.user.application.Models;
using MediatR;

namespace luxclusif.user.application.Notifications
{
    public class DefaultNotification : INotificationHandler<DefaultMessageNotification>
    {

        public readonly IMessageSenderInterface message;

        public DefaultNotification(IMessageSenderInterface message)
        {
            this.message = message;
        }

        public Task Handle(DefaultMessageNotification notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => message.Send(notification.EventName, notification.Data).GetAwaiter().GetResult());

            return Task.CompletedTask;
        }
    }
}
