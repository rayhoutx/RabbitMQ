using EventBus.Messages.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {

        private readonly ILogger<BasketCheckoutConsumer> _logger;
        public BasketCheckoutConsumer(ILogger<BasketCheckoutConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            //Console.Write($"lastname={context.Message.LastName}, firsname={context.Message.FirstName}  write\n");
            _logger.LogDebug($"lastname={context.Message.LastName}, firsname={context.Message.FirstName}   log\n");

            return Task.CompletedTask;
        }
    }
}
