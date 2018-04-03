using EasyNetQ;
using EasyNetQ.Loggers;
using Fundraise.Airtable;
using Fundraise.IntegrationEvents;
using System;

namespace Fundraise.AirtableIntegration.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IEasyNetQLogger logger = new ConsoleLogger();

            using (var bus = RabbitHutch.CreateBus("host=localhost",
                serviceRegister => serviceRegister.Register(serviceProvider => logger)))
            {
                bus.Subscribe<DonationCreatedIntegrationEvent>("airtable_donation", HandleDonationCreatedMessage);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleDonationCreatedMessage(DonationCreatedIntegrationEvent message)
        {
            Console.WriteLine(
                "Message Recieved!\n\n" +
                "Id: " + message.Id + "\n" +
                "UserId: " + message.UserId + "\n" +
                "DonationAmount: " + message.DonationAmount + "\n" +
                "DonorDisplayName: " + message.DonorDisplayName + "\n" +
                "FundraiserId: " + message.FundraiserId + "\n\n"
            );

            var client = new AirtableClient("appJdKhgVuGZUv0va", "keyql7soeJgu7cdsP");

            var x = client.AddDonation(message);
        }
    }
}
