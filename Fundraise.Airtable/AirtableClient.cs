using Fundraise.IntegrationEvents;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fundraise.Airtable
{
    public class AirtableClient
    {
        private string _baseId;
        private string _apiKey;

        private const string BASEURL = "https://api.airtable.com/v0/";

        public AirtableClient(string baseId, string apiKey)
        {
            _baseId = baseId;
            _apiKey = apiKey;
        }

        public string AddDonation(DonationCreatedIntegrationEvent donation)
        {
            var client = new RestClient(BASEURL);
            var request = new RestRequest($"{_baseId}/Donations", Method.POST);
            request.AddHeader("Authorization", "Bearer " + _apiKey);
            var fields = new Dictionary<string, object>();
            fields.Add("Id", donation.Id);
            fields.Add("Donor Id", donation.UserId);
            fields.Add("Creation Date", donation.CreationDate);
            fields.Add("Donation Amount", donation.DonationAmount);
            fields.Add("Donor Display Name", donation.DonorDisplayName);
            fields.Add("Fundraiser Id", donation.FundraiserId);
            var parameters = new Dictionary<string, Dictionary<string, object>>();
            parameters.Add("fields", fields);

            request.AddParameter("application/json", JsonConvert.SerializeObject(parameters), ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error retrieving response.  Check inner details for more info.", response.ErrorException);
            }

            return "ok";
        }
    }
}
