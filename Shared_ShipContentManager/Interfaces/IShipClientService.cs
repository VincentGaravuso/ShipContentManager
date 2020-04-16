using ServiceStack;
using Shared_ShipContentManager.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ShipContentManager.Interfaces
{
    public interface IShipClientService
    {
        Task<Pack> CreatePack(Pack pack);
        Task<List<Pack>> GetAllPacks();
        Task<List<Question>> GetAllQuestions();
        Task<Question> CreateQuestion(Question question);
        Task<Question> DeleteQuestion(Question deletableQuestion);
        Task<Pack> UpdatePackName(string packId, string PackName);

    }
}
