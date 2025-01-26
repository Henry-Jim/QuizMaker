using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace QuizMaker
{
    [Serializable]
    public class Quiz
    {
        public static readonly Random random = new Random();

        public List<Question> Questions { get; set; }

        public Quiz()
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
            XmlSerializer serializer = new XmlSerializer(typeof(Quiz));
            using (StreamWriter write = new StreamWriter(path))
            {
                serializer.Serialize(write, this);
            }
        }

        public static Quiz LoadFromFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Quiz));
            using (StreamReader reader = new StreamReader(path))
            {
                return (Quiz)serializer.Deserialize(reader);
            }
        }

        public void SaveToDefault()
        {
            SaveToFile(Constants.DEFAULT_FILE_PATH);
        }

        public static Quiz LoadFromDefault()
        {
            return LoadFromFile(Constants.DEFAULT_FILE_PATH);
        }

        public int GetQuestionCount()
        {
            return Questions.Count;
        }
    }
}
