// See https://aka.ms/new-console-template for more information

using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Subscriber.EventBusConsumer;

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
            config.AddConsumer<BasketCheckoutConsumer>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("amqp://guest:guest@localhost:5672");
                cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                {
                    c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                });
            });
        })
        .AddScoped<BasketCheckoutConsumer>()
        .BuildServiceProvider();


        var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
        logger.LogDebug("Starting application");

        while (true)
        {
            Console.WriteLine("This is subscriber!");
            Console.ReadLine();
        }
    }
}