using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Shared_ShipContentManager.Models
{ 
    public class Question
    {
        [JsonProperty("objectId")]
        public string QuestionObjectId { get; }
        [JsonProperty("questionText")]
        public string QuestionText { get; set; }
        [JsonProperty(null), AllowNull]
        public DateTime DateCreated { get; set; }
        [JsonProperty("packObjectIds")]
        public List<string> Packs { get; set; }

        public string DateCreatedToString()
        {
            return DateCreated.ToString("dd/MM/yyyy");
        }
    }

}
