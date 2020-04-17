using System.Collections.Generic;
using System.Threading.Tasks;
using Shared_ShipContentManager.Models;
using Shared_ShipContentManager.Interfaces;
using Shared_ShipContentManager.Services;
using ShipContentManager;
using System.Runtime.CompilerServices;

namespace ShipContentManager.Services
{
    public class ContentManagerDataService
    {
        private List<Question> storedLocalQuestions;
        private List<Pack> storedLocalPacks;
        private IShipClientService shipService { get; set; } = new ShipClient();

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
        public async Task<Pack> UpdatePack(string packId, string packName)
        {
            return await shipService.UpdatePackName(packId, packName);
        }
    }
}
