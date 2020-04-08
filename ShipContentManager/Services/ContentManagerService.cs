using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShipContentManager.Models;

namespace ShipContentManager.Services
{
    public static class ContentManagerService
    {
        private static List<Question> storedQuestions = new List<Question>();
        private static List<Pack> storedPacks = new List<Pack>();
        public static List<Question> GetStoredQuestions()
        {
            return storedQuestions;
        }
        public static List<Pack> GetStoredPacks()
        {
            return storedPacks;
        }
        public static List<Question> GetQuestionsFromDb()
        {
            storedQuestions.Clear();
            Random random = new Random();
            //Querying database for list
            for (int i = 0; i < 15; i++)
            {
                Question q = new Question();
                q.QuestionText = "What is a good sample text to use for demonstrating what a question may look like?";
                q.DateCreated = DateTime.Now;
                q.Packs.Add(storedPacks.ElementAt(random.Next(0, storedPacks.Count())));
                q.Packs.Add(storedPacks.ElementAt(random.Next(0, storedPacks.Count())));
                storedQuestions.Add(q);
            }
            return storedQuestions;
        }
        public static  List<Pack> GetPacksFromDb()
        {
            storedPacks.Clear();
            //Querying database for list
            for (int i = 0; i < 15; i++)
            {
                Pack p = new Pack();
                p.Name = $"Pack {i}";
                p.DateCreated = DateTime.Now;
                p.IsMiniPack = true;
                storedPacks.Add(p);
            }
            return storedPacks;
        }
    }
}
