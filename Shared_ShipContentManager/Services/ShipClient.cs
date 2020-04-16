using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Shared_ShipContentManager.Models;
using Shared_ShipContentManager.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using ServiceStack.Text;
using System.Text;
using System.Xml.XPath;

namespace Shared_ShipContentManager.Services
{
    public partial class ShipClient : IShipClientService
    {
        private readonly HttpClient client;
        public ShipClient(HttpClient client)
        {
            //TODO: FIND BETTER SOLUTION FOR CREDENTIAL MANAGING
            this.client = client; 
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Shared_ShipContentManager\appsettings.json");
            var fileLines = File.ReadAllText(filePath);
            var jsonObject = JsonConvert.DeserializeObject<Authentication>(fileLines);

            client.DefaultRequestHeaders.Add(jsonObject.AuthenticationHeader.ToString(), jsonObject.AuthenticationPassword.ToString());
            client.BaseAddress = new Uri("https://projectshipwebapp.azurewebsites.net/api/QuestionsManagement/");
        }
        public async Task<Pack> CreatePack(Pack pack)
        {
            //var json = JsonConvert.SerializeObject(pack);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var stringContent = DataFormatService.JsonToStringContent(ref pack);
            var response = await client.PostAsync("CreatePack", stringContent);
            if (response.IsSuccessStatusCode)
            {
                //var responseString = await request.Content.ReadAsStringAsync();
                var responseString = await DataFormatService.ResponseMessageToString(response);
                var result = JsonConvert.DeserializeObject<Pack>(responseString);
                return result;
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<Question> CreateQuestion(Question question)
        {
            //var json = JsonConvert.SerializeObject(question);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var stringContent = DataFormatService.JsonToStringContent(ref question);
            var response = await client.PostAsync("CreateQuestion", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await DataFormatService.ResponseMessageToString(response);
                var result = JsonConvert.DeserializeObject<Question>(responseString);
                return result;
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        public async Task<Question> DeleteQuestion(Question questionId)
        {
            //string json = JsonConvert.SerializeObject(questionId);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var stringContent = DataFormatService.JsonToStringContent(ref questionId);
            var response = await client.PostAsync("DeleteQuestion", stringContent);
            if (response.IsSuccessStatusCode)
            {
                //var responseString = await request.Content.ReadAsStringAsync();
                var responseString = await DataFormatService.ResponseMessageToString(response);
                var result = JsonConvert.DeserializeObject<Question>(responseString);
                return result;
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
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Pack>>(responseString);
                return result;
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
                //var responseString = await response.Content.ReadAsStringAsync();
                var responseString = await DataFormatService.ResponseMessageToString(response);
                var result = JsonConvert.DeserializeObject<List<Question>>(responseString);
                return result;
            }
            else
            {
                throw new HttpRequestException();

            }
        }
        public async Task<Pack> UpdatePackName(string packId, string packName)
        {
            var requestString = JsonConvert.SerializeObject(new { packId, packName });
            var stringContent = new StringContent(requestString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("UpdatePackName", stringContent);

            if (response.IsSuccessStatusCode)
            {
                //var responseString = await request.Content.ReadAsStringAsync();
                var responseString = await DataFormatService.ResponseMessageToString(response);
                var result = JsonConvert.DeserializeObject<Pack>(responseString);
                return result;
            }
            else
            {
                throw new HttpRequestException();
            }
        }   
    }
}
