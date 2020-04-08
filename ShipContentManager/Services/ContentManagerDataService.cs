using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShipContentManager.Models;

namespace ShipContentManager.Services
{
    public static class ContentManagerDataService
    {
        private static List<Question> localQuestions = new List<Question>();
        private static List<Pack> localPacks = new List<Pack>();
        public static List<Question> GetLocalQuestions()
        {
            return localQuestions;
        }
        public static List<Pack> GetLocalPacks()
        {
            return localPacks;
        }
        public static List<Question> GetQuestionsFromServer()
        {
            localQuestions.Clear();
            Random random = new Random();
            //Querying database for list
            for (int i = 0; i < 15; i++)
            {
                Question q = new Question();
                q.QuestionText = "What is a good sample text to use for demonstrating what a question may look like?";
                q.DateCreated = DateTime.Now;
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                q.Packs.Add(localPacks.ElementAt(random.Next(0, localPacks.Count())));
                localQuestions.Add(q);
            }
            return localQuestions;
        }
        public static List<Pack> GetPacksFromServer()
        {
            localPacks.Clear();
            //Querying database for list
            for (int i = 0; i < 15; i++)
            {
                Pack p = new Pack();
                p.Name = $"Pack {i}";
                p.DateCreated = DateTime.Now;
                p.IsMiniPack = true;
                localPacks.Add(p);
            }
            return localPacks;
        }
    }
}
