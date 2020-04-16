using System.Collections.Generic;
using System.Threading.Tasks;
using Shared_ShipContentManager.Models;
using Shared_ShipContentManager.Interfaces;

namespace ShipContentManager.Services
{
    public class ContentManagerDataService
    {
        private static List<Question> storedLocalQuestions = new List<Question>();
        private static List<Pack> storedLocalPacks = new List<Pack>();
        private static IShipClientService shipClientService;
        public ContentManagerDataService(IShipClientService clientService)
        {
            shipClientService = clientService;
        }
        public List<Question> GetLocalQuestions()
        {
            return storedLocalQuestions;
        }
        public List<Pack> GetLocalPacks()
        {
            return storedLocalPacks;
        }
        public async Task<List<Question>> GetQuestionsFromServer()
        {
            clearLocalQuestions();
            return storedLocalQuestions = await shipClientService.GetAllQuestions();
        }
        public async Task<List<Pack>> GetPacksFromServer()
        {
            clearLocalPacks();
            return storedLocalPacks = await shipClientService.GetAllPacks();
        }
        public async Task<Question> CreateQuestion(Question question)
        {
            return await shipClientService.CreateQuestion(question);

        }
        public static async Task<Pack> UpdatePack(string packId, string packName)
        {
            return await shipClientService.UpdatePackName(packId, packName);
        }
        private void clearLocalPacks()
        {
            storedLocalPacks.Clear();
        }
        private void clearLocalQuestions()
        {
            storedLocalQuestions.Clear();
        }
    }
}
