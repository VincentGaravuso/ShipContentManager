using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared_ShipContentManager.Models
{
        public class Authentication
        {
            public string AuthenticationHeader { get; set; }
            public string AuthenticationPassword { get; set; }
        }
}
