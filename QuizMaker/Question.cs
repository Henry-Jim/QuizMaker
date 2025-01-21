using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    [Serializable]
    internal class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public HashSet<int> CorrectAnswerIndices { get; set; }

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
