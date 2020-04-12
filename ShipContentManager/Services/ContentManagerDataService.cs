using System.Collections.Generic;
using ShipContentManager.Models;
using Shared_ShipContentManager;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShipContentManager.Services
{
    public static class ContentManagerDataService
    {
        private static List<Question> localQuestions = new List<Question>();
        private static List<Pack> localPacks = new List<Pack>();
        private static ServiceProvider serviceProvider;
        private static ShipClient shipClient;
        public static void InitializeShipClientService()
        {
            serviceProvider = new ServiceCollection()
              .AddSingleton<HttpClient>()
              .AddTransient<ShipClient>()
              .BuildServiceProvider();
            shipClient = serviceProvider.GetService<ShipClient>();
        }
        public static List<Question> GetLocalQuestions()
        {
            return localQuestions;
        }
        public static List<Pack> GetLocalPacks()
        {
            return localPacks;
        }
        public static async Task<List<Question>> GetQuestionsFromServer()
        {
            localQuestions.Clear();
            var createResponse = await shipClient.SendGetQuestionsRequest();
            var responseString = await createResponse.Content.ReadAsStringAsync();
            localQuestions = JsonConvert.DeserializeObject<List<Question>>(responseString);
            return localQuestions;
        }
        public static async Task<List<Pack>> GetPacksFromServer()
        {
            localPacks.Clear();
            await shipClient.SendGetPacksRequest();
            var getResponse = await shipClient.SendGetPacksRequest();
            var responseString = await getResponse.Content.ReadAsStringAsync();
            localPacks = JsonConvert.DeserializeObject<List<Pack>>(responseString);
            return localPacks;
        }
        public static async Task<Question> CreateQuestion(HttpContent content)
        {
            var createResponse = await shipClient.SendCreateQuestionRequst(content);
            var responseString = await createResponse.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<Question>(responseString);
            return responseContent;
        }
        public static async Task<Pack> UpdatePack(HttpContent content)
        {
            var createResponse = await shipClient.SendUpdatePackRequest(content);
            var responseString = await createResponse.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<Pack>(responseString);
            return responseContent;
        }
    }
}
