using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTanico
{
    public class CimsClient
    {
        private static CimsClientSettings _settings { get; set; } = new CimsClientSettings();
        private static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_settings.BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            return client;
        }
        public async Task<CimsContractResponse> GetContracts(string aCustNo)
        {
            var queryString = new Dictionary<string, string>()
            {
                { "CustNo",  aCustNo}
            };

            using (var client = CimsClient.GetHttpClient())
            {
                var requestUri = QueryHelpers.AddQueryString(_settings.ContractsPath, queryString);
                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                using (var res = response.EnsureSuccessStatusCode())
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CimsContractResponse>(jsonString);
                }
            }
        }
        public async Task<CimsCustomerResponse> GetCustomers()
        {
            using (var client = CimsClient.GetHttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(_settings.CustomerPath))
                using (var res = response.EnsureSuccessStatusCode())
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CimsCustomerResponse>(jsonString);
                }
            }
        }
        public async Task<CimsItemsResponse> GetItems()
        {
            using (var client = CimsClient.GetHttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(_settings.ItemPath))
                using (var res = response.EnsureSuccessStatusCode())
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CimsItemsResponse>(jsonString);
                }
            }
        }
    }
    public class CimsContractResponse
    {
        public CimsContract[] Contracts { get; set; }

    }
    public class CimsItemsResponse
    {
        public CimsItem[] Items { get; set; }

    }
    public class CimsCustomerResponse
    {
        public string ErrMsg { get; set; }
        public CimsCustomer[] Customers { get; set; }

    }
    public class CimsContract
    {
        public string CustNo { get; set; }
        public string ContractNo { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string ItemNo { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string ContractType { get; set; }
        public string Quantity { get; set; }
        public string DeliveredQuantity { get; set; }
        public string InvoicedQuantity { get; set; }
        public string TermsOfShipping { get; set; }
        public string InfoText { get; set; }

        public override string ToString()
        {
            return this.CustNo + ", " + this.ContractNo + ", " + this.ItemNo + ", " + this.Description1;
        }

    }
    public class CimsItem
    {
        public string ItemNo { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Unit { get; set; }
        public double PackageSize { get; set; }
        public bool Inactive { get; set; }

        public override string ToString()
        {
            return this.ItemNo + ", " + this.Description1;
        }
    }
    public class CimsCustomer
    {
        public string CustNo { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool Inactive { get; set; }

        public override string ToString()
        {
            return this.CustNo + ", " + this.Name;
        }
    }
    public class CimsClientSettings
    {
        public string BaseUri { get; set; } = @"https://rdg-tanico.addera.it/cimsgrainapi/grainapi/";
        public string Token { get; set; } = "23551e4d116e4730b68ae9ef5a3c2666e5ad518e3611413e99";
        public string CustomerPath { get; set; } = "getCustomers";
        public string ItemPath { get; set; } = "getItems";
        public string ContractsPath { get; set; } = "getContracts";

    }
}
