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
        Task<Pack> SendCreatePackRequst(Pack pack);
        Task<List<Pack>> SendGetPacksRequest();
        Task<List<Question>> SendGetQuestionsRequest();
        Task<Question> SendCreateQuestionRequst(Question question);
        Task<Question> SendDeleteQuestionRequest(Question deletableQuestion);
        Task<Pack> SendUpdatePackNameRequest(string packId, string PackName);

    }
}
