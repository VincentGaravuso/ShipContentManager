using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Shared_ShipContentManager.Models;
using Shared_ShipContentManager.Interfaces;
using System.Collections.Generic;

namespace Shared_ShipContentManager.Services
{
    public partial class ShipClient : IShipClientService
    {
        private readonly HttpClient client;
        public ShipClient()
        {
            //TODO: FIND BETTER SOLUTION FOR CREDENTIAL MANAGING
            client = new HttpClient();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Shared_ShipContentManager\appsettings.json");
            var fileLines = File.ReadAllText(filePath);
            var jsonObject = JsonConvert.DeserializeObject<Authentication>(fileLines);

            client.DefaultRequestHeaders.Add(jsonObject.AuthenticationHeader, jsonObject.AuthenticationPassword);
            client.BaseAddress = new Uri("https://projectshipwebapp.azurewebsites.net/api/QuestionsManagement/");
        }
        public async Task<Pack> CreatePack(Pack pack)
        {
            var queryParameters = buildCreatePackQueryParameter(pack);
            var response = await client.PostAsync("CreatePack?" + queryParameters, null);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                return DataFormatService.JsonToModel<Pack>(responseString);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<Question> CreateQuestion(Question question)
        {
            var queryParameters = buildCreateQuestionQueryParameter(question);
            var response = await client.PostAsync("CreateQuestion?" + queryParameters, null);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                return DataFormatService.JsonToModel<Question>(responseString);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<Question> UpdateQuestion(Question question)
        {
            var queryParameters = buildUpdateQuestionQueryParameter(question);
            var response = await client.PutAsync("UpdateQuestion?" + queryParameters, null);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                return DataFormatService.JsonToModel<Question>(responseString);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<bool> DeleteQuestion(Question question)
        {
            var queryParameters = buildDeleteQuestionQueryParameter(question);
            var response = await client.DeleteAsync("DeleteQuestion?" + queryParameters);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<List<Pack>> GetAllPacks()
        {
            var response = await client.GetAsync("GetAllPacks");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                return DataFormatService.JsonToModel<List<Pack>>(responseString);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<List<Question>> GetAllQuestions()
        {
            var response = await client.GetAsync("GetAllQuestions");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                return DataFormatService.JsonToModel<List<Question>>(responseString);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<Pack> UpdatePackName(string packId, string packName)
        {
            var queryParameters = buildUpdatePackQueryParameter(packId, packName);
            var response = await client.PutAsync("UpdatePackName?" + queryParameters, null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                return DataFormatService.JsonToModel<Pack>(responseString);
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        #region BuildQueryMethods
        private object buildCreatePackQueryParameter(Pack pack)
        {
            return $"packName={pack.Name}&IsMiniPack={pack.IsMiniPack}";
        }
        private object buildUpdatePackQueryParameter(string packId, string packName)
        {
            return $"packObjectId={packId}&newPackName={packName}";
        }
        private object buildCreateQuestionQueryParameter(Question question)
        {
            string query = "";
            foreach (string packId in question.Packs)
            {
                query += $"packObjectIds={packId}&";
            }
            query += $"questionText={question.QuestionText}";
            return query;
        }
        private object buildUpdateQuestionQueryParameter(Question question)
        {
            string query = "";
            foreach (string packId in question.Packs)
            {
                query += $"packObjectIds={packId}&";
            }
            query += $"questionId={question.QuestionObjectId}&questionText={question.QuestionText}";

            return query;
        }
        private object buildDeleteQuestionQueryParameter(Question question)
        {
            return $"questionId={question.QuestionObjectId}";
        }
        #endregion
    }
}
