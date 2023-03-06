// See https://aka.ms/new-console-template for more information

using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging((loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                .AddConsole())
            .AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                });
            })
            .BuildServiceProvider();

               
        var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
        logger.LogDebug("Starting application");


        IPublishEndpoint publishEndpoint = (IPublishEndpoint)serviceProvider.GetService(typeof(IPublishEndpoint));
        BasketCheckoutEvent eventMessage = new BasketCheckoutEvent{ FirstName = "Xilin", LastName = "Li"};

        int counter = 0;
        while (true)
        {
            Console.WriteLine("This is publisher!");
            Console.ReadLine();

            eventMessage.FirstName += " " + counter;
            publishEndpoint.Publish(eventMessage);
            counter++;
        }

    }
}