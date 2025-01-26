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
    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }

        [XmlIgnore]
        public HashSet<int> CorrectAnswerIndices { get; set; }

        public List<int> CorrectIndicesList
        {
            get => new List<int>(CorrectAnswerIndices);
            set => CorrectAnswerIndices = new HashSet<int>(value);
        }

        public Question() // Default constructor
        {
            Answers = new List<string>();
            CorrectAnswerIndices = new HashSet<int>();
        }

        public Question(string text, List<string> answers, HashSet<int> correctAnswerIndices)
        {
            Text = text;
            Answers = answers;
            CorrectAnswerIndices = correctAnswerIndices;
        }

        public bool IsCorrect(List<int> userAnswers)
        {
            return CorrectAnswerIndices.SetEquals(userAnswers);
        }
    }
}
