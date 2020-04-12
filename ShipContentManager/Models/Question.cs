using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ShipContentManager.Models
{
    public class Question
    {
        [JsonProperty("objectId")]
        public string QuestionObjectId { get; set; }
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
