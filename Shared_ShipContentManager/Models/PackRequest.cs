using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
