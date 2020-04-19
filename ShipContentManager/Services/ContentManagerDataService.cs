using System.Collections.Generic;
using System.Threading.Tasks;
using Shared_ShipContentManager.Models;
using Shared_ShipContentManager.Interfaces;

namespace ShipContentManager.Services
{
    public class ContentManagerDataService
    {
        private List<Question> storedLocalQuestions;
        private List<Pack> storedLocalPacks;
        private IShipClientService shipService;
        public ContentManagerDataService(IShipClientService shipService)
        {
            this.shipService = shipService;
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
            return storedLocalQuestions = await shipService.GetAllQuestions();
        }
        public async Task<List<Pack>> GetPacksFromServer()
        {
            return storedLocalPacks = await shipService.GetAllPacks();
        }
        public async Task<Question> CreateQuestion(Question question)
        {
            return await shipService.CreateQuestion(question);
        }
        public async Task<Question> UpdateQuestion(Question question)
        {
            return await shipService.UpdateQuestion(question);
        }
        public async Task<bool> DeleteQuestion(Question question)
        {
            return await shipService.DeleteQuestion(question);
        }
        public async Task<Pack> CreatePack(Pack pack)
        {
            return await shipService.CreatePack(pack);
        }
        public async Task<Pack> UpdatePack(string packId, string packName)
        {
            return await shipService.UpdatePackName(packId, packName);
        }
    }
}
