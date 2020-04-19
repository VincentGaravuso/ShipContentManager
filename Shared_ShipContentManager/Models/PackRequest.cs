using Newtonsoft.Json;

namespace Shared_ShipContentManager.Models
{
    public class PackRequest
    {
        [JsonProperty("packName")]
        public string Name { get; set; }
        [JsonProperty("isMiniPack")]
        public bool IsMiniPack { get; set; }
    }
}
