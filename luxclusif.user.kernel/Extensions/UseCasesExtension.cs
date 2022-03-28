using luxclusif.user.application.Interfaces;
using luxclusif.user.application.Models;
using luxclusif.user.application.UseCases.User.CreateUser;
using luxclusif.user.infrastructure.rabbitmq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace luxclusif.user.kernel.Extensions;
public static class UseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var assembly1 = Assembly.GetExecutingAssembly();
        Assembly configurationAppAssembly = typeof(CreateUser).Assembly;

        services.AddMediatR(assembly1,configurationAppAssembly);

        services.AddSingleton<IMessageSenderInterface, SendMessageRabbitmq>();

        services.AddScoped<Notifier>();

        return services;
    }
}
