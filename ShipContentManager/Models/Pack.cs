using System;
using Newtonsoft.Json;
namespace ShipContentManager.Models
{
    public class Pack
    {
        [JsonProperty("packName")]
        public string Name { get; set; }
        [JsonProperty("")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("isMiniPack")]
        public bool IsMiniPack { get; set; }
        [JsonProperty("objectId")]
        public string PackObjectId { get; set; }


        public string DateCreatedToString()
        {
            return DateCreated.ToString("dd/MM/yyyy");
        }
    }
}