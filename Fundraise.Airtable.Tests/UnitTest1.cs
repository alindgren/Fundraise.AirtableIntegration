using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Fundraise.Airtable.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Save_To_Airtable()
        {
            var client = new AirtableClient("appJdKhgVuGZUv0va", "keyql7soeJgu7cdsP");

            var donationEvent = new IntegrationEvents.DonationCreatedIntegrationEvent(Guid.NewGuid().ToString(), Guid.NewGuid(), 20.50, "Test Donor");

            var x = client.AddDonation(donationEvent);
        }
    }
}
