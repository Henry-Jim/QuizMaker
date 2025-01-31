using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    internal class UI
    {

        public void AddQuestions(QuizManager quiz)
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
                quiz.AddQuestion(question);
            }
        }

        public void RemoveQuestion(QuizManager quiz)
        {
            if (quiz.GetQuestionCount() == 0)
            {
                Console.WriteLine("There are no questions to remove.");
                return;
            }

            Console.WriteLine("Here are the current questions: ");
            for (int i = 0; i < quiz.GetQuestionCount(); i++)
            {
                Console.WriteLine($"{i + 1}. {quiz.Questions[i].Text}");
            }

            Console.WriteLine("Enter the number(s) of the question(s). Use commas to separate: ");
            HashSet<int> indicesToRemove = new HashSet<int>(Console.ReadLine().Split(',').Select(x => int.Parse(x) - 1));

            foreach (var index in indicesToRemove.OrderByDescending(i => i))
            {
                if (index >= 0 && index < quiz.GetQuestionCount())
                {
                    quiz.Questions.RemoveAt(index);
                    Console.WriteLine($"Removed question #{index + 1}");
                }
                else
                {
                    Console.WriteLine($"Invalid index: {index + 1}. Skipping...");
                }
            }

            Console.WriteLine("Questions updated successfully.");
        }

        public void PlayQuiz(QuizManager quiz)
        {
            int score = Constants.ININTIAL_SCORE;
            int totalQuestions = quiz.GetQuestionCount();

            if (totalQuestions == 0)
            {
                Console.WriteLine("No questions available to play.");
                return;
            }

            List<Question> shuffledQuestions = quiz.Questions.OrderBy(q => QuizManager.random.Next()).ToList();

            foreach (Question question in shuffledQuestions)
            {
                Console.WriteLine(question.Text);

                for (int j = 0; j < question.Answers.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {question.Answers[j]}");
                }

                Console.WriteLine("Enter the indices of the correct answers (use commas to separate multiple answers, starting from 1): ");
                List<int> userAnswers = Console.ReadLine().Split(',').Select(int.Parse).ToList();

                Console.WriteLine($"Your answer: {string.Join(",", userAnswers)}");
                Console.WriteLine($"Correct answer: {string.Join(",", question.CorrectAnswerIndices)}");

                if (question.IsCorrect(userAnswers))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }

                Console.WriteLine($"Your final score: {score}/{totalQuestions}");
            }
        }

        public int ShowMainMenu()
        {
            Console.WriteLine("\nManin Menu: ");
            Console.WriteLine($"{Constants.MODE_CREATE}. Create a new quiz");
            Console.WriteLine($"{Constants.MODE_PLAY}. Play an existing quiz");
            Console.WriteLine($"{Constants.MODE_REMOVE_QUESTIONS}. Remove questions from a quiz");
            Console.WriteLine($"{Constants.MODE_SAVE}. Save the current quiz");
            Console.WriteLine($"{Constants.MODE_LOAD}. Load a saved quiz");
            Console.WriteLine($"{Constants.MODE_EXIT}. Exit");

            Console.WriteLine("Choose an option");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }

            Console.WriteLine("Invalid choice. Please try again.");
            return -1;
        }
    }
}
