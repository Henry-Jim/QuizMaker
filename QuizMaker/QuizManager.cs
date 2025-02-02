using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace QuizMaker
{
    
    public class QuizManager
    {
        public static readonly Random random = new Random();
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(QuizManager));

        public List<Question> Questions { get; set; }

        public QuizManager()
        {
            Questions = new List<Question>();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public Question GetRandomQuestion()
        {
            int index = random.Next(Questions.Count);
            return Questions[index];
        }

        public void RemoveQuestion(int index)
        {
            Questions.RemoveAt(index);
        }

        public void SaveToFile(string path)
        {
            
            using (StreamWriter write = new StreamWriter(path))
            {
                _serializer.Serialize(write, this);
            }
        }

        public static QuizManager LoadFromFile(string path)
        {
            
            using (StreamReader reader = new StreamReader(path))
            {
                return (QuizManager)_serializer.Deserialize(reader);
            }
        }

        public void SaveToDefault()
        {
            SaveToFile(Constants.DEFAULT_FILE_PATH);
        }

        public static QuizManager LoadFromDefault()
        {
            return LoadFromFile(Constants.DEFAULT_FILE_PATH);
        }

        public int GetQuestionCount()
        {
            return Questions.Count;
        }

        public void AddQuestions(List<Question> questions)
        {
            Questions.AddRange(questions); // Add all questions at once
        }

        public void SaveAll()
        {
            SaveToDefault(); // Save the entire list instead of one-by-one
        }
    }
}
