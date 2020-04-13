using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared_ShipContentManager.Models;
using Shared_ShipContentManager.Services;

namespace ShipContentManager.Services
{
    public static class ContentManagerDataService
    {
        private static List<Question> storedLocalQuestions = new List<Question>();
        private static List<Pack> storedLocalPacks = new List<Pack>();
        public static ShipClient shipClient { get; set; } 
        public static void Init(ShipClient injectedShipClient)
        {
             shipClient = injectedShipClient;
        }
       
        public static List<Question> GetLocalQuestions()
        {
            return storedLocalQuestions;
        }
        public static List<Pack> GetLocalPacks()
        {
            return storedLocalPacks;
        }
        public static async Task<List<Question>> GetQuestionsFromServer()
        {
            storedLocalQuestions.Clear();
            storedLocalQuestions = await shipClient.SendGetQuestionsRequest();
            return storedLocalQuestions;
        }
        public static async Task<List<Pack>> GetPacksFromServer()
        {
            storedLocalPacks.Clear();
            storedLocalPacks = await shipClient.SendGetPacksRequest();
            return storedLocalPacks;
        }
        public static async Task<Question> CreateQuestion(Question question)
        {
            var createResponse = await shipClient.SendCreateQuestionRequst(question);
            return createResponse;
        }
        public static async Task<Pack> UpdatePack(string packId, string packName)
        {
            var updatePackResponse = await shipClient.SendUpdatePackNameRequest(packId, packName);
            return updatePackResponse;
        }
    }
}
