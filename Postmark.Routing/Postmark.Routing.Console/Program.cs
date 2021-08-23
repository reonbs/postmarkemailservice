using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Postmark.Routing.Service.Consumer;
using RabbitMQ.Client;

namespace Postmark.Routing.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("amqp://guest:guest@localhost:5672");

                cfg.ReceiveEndpoint("qualified-email-queue", e =>
                {
                    e.Consumer<EmailConsumer>();
                    e.Bind("email-exchange", s =>
                    {
                        s.RoutingKey = "qualified";
                        s.ExchangeType = ExchangeType.Direct;
                    });
                });

                cfg.ReceiveEndpoint("disqualified-email-queue", e =>
                {
                    e.Consumer<EmailConsumer>();
                    e.Bind("email-exchange", s =>
                    {
                        s.RoutingKey = "disqualified";
                        s.ExchangeType = ExchangeType.Direct;
                    });
                });

            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);

            try
            {
                System.Console.WriteLine("Press enter to exit");
                await Task.Run(() => System.Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
