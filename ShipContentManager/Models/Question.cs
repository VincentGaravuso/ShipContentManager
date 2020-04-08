using System;
using System.Collections.Generic;
using System.Text;

namespace ShipContentManager.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Pack> Packs { get; set; }
        public Question()
        {
            Packs = new List<Pack>();
        }
        public string DateCreatedToString()
        {
            return DateCreated.ToString("dd/MM/yyyy");
        }
    }

}
