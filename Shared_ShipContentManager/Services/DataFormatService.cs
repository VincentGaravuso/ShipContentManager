using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Shared_ShipContentManager.Services
{
    public static class DataFormatService
    {
        public static StringContent JsonToStringContent<T>(ref T model)
        {
            var json = JsonConvert.SerializeObject(model);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async static Task<string> ResponseMessageToString(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
