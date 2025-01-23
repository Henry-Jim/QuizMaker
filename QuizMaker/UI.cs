using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    internal class UI
    {
        private Quiz _quiz;

        public UI(Quiz quiz)
        {
            this._quiz = quiz;
        }

        public void AddQuestions()
        {
            Console.WriteLine("Enter your questions here. Type 'done' to finish.");

            while (true)
            {
                Console.WriteLine("Enter the question text (or type 'done' to finish): ");
                string questionText = Console.ReadLine();
                if (questionText.ToLower() == "done") break;

                Console.WriteLine("Enter possible answers (or type 'done' to finish): ");
                List<string> answers = new List<string>();
                while (true)
                {
                    Console.WriteLine("Ansers: ");
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "done") break;
                    answers.Add(answer);
                }

                Console.WriteLine("Enter the index of correct answers (use comma to separate multiple answers): ");
                HashSet<int> correctAnswerIndices = new HashSet<int>(Console.ReadLine().Split(',').Select(int.Parse));

                Question question = new Question(questionText, answers, correctAnswerIndices);
                _quiz.AddQuestion(question);
            }
        }

        public void RemoveQuestion()
        {
            if (_quiz.GetQuestionCount() == 0)
            {
                Console.WriteLine("There are no questions to remove.");
                return;
            }

            Console.WriteLine("Here are the current questions: ");
            for (int i = 0; i < _quiz.GetQuestionCount(); i++)
            {
                Console.WriteLine($"{i + 1}. {_quiz.Questions[i].Text}");
            }

            Console.WriteLine("Enter the number(s) of the question(s). Use commas to separate: ");
            HashSet<int> indicesToRemove = new HashSet<int>(Console.ReadLine().Split(',').Select(x => int.Parse(x) - 1));

            foreach (var index in indicesToRemove.OrderByDescending(i => i))
            {
                if (index >= 0 && index < _quiz.GetQuestionCount())
                {
                    _quiz.Questions.RemoveAt(index);
                    Console.WriteLine($"Removed question #{index + 1}");
                }
                else
                {
                    Console.WriteLine($"Invalid index: {index + 1}. Skipping...");
                }
            }

            Console.WriteLine("Questions updated successfully.");
        }

        public void PlayQuiz()
        {
            int score = Constants.ININTIAL_SCORE;
            int totalQuestions = _quiz.GetQuestionCount();

            for (int i = 0, i < totalQuestions; i++)
            {
                Question question = _quiz.GetRandomQuestion();
                Console.WriteLine(question.Text);

                for (int j = 0; j < question.Answers.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {question.Answers[j]}");
                }

                Console.WriteLine("Enter the indices of the correct answers (use comma to separate multiple answers): ");
                HashSet<int> userAnswers = new HashSet<int>(Console.ReadLine().Split(',').Select(x => int.Parse(x) - 1));

                if (question.IsCorrect(userAnswers))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }
            }

            Console.WriteLine($"Your score: {score}/{totalQuestions}");
        }
    }
}
