using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
namespace Shared_ShipContentManager
{
    public partial class ShipClient
    {
        private readonly HttpClient client;
        public ShipClient(HttpClient client)
        {
            this.client = client; 
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Shared_ShipContentManager\appsettings.json");
            var fileLines = File.ReadAllText(filePath);
            var jsonObject = JsonConvert.DeserializeObject<Authentication>(fileLines);

            client.DefaultRequestHeaders.Add(jsonObject.AuthenticationHeader.ToString(), jsonObject.AuthenticationPassword.ToString());
            client.BaseAddress = new Uri("https://projectshipwebapp.azurewebsites.net/api/QuestionsManagement/");
        }
        public async Task<HttpResponseMessage> SendGetPacksRequest()
        {
            return await client.GetAsync("GetAllPacks");
        }
        public async Task<HttpResponseMessage> SendGetQuestionsRequest()
        {
            return await client.GetAsync("GetAllQuestions");
        }
        public async Task<HttpResponseMessage> SendCreatePackRequst(HttpContent content)
        {
            return await client.PostAsync("", content);
        }
        public async Task<HttpResponseMessage> SendCreateQuestionRequst(HttpContent content)
        {
           return await client.PostAsync("", content);
        }
        public async Task<HttpResponseMessage> SendDeleteQuestionRequest()
        {
            return await client.DeleteAsync("");
        }  
        public async Task<HttpResponseMessage> SendUpdatePackRequest(HttpContent content)
        {
            return await client.PutAsync("", content);
        }
    }
}
