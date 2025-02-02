namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Quiz Maker! ");

            QuizManager quiz = new QuizManager();
            UI ui = new UI();

            while (true)
            {
                int choice = ui.ShowMainMenu();

                switch (choice)
                {
                    case Constants.MODE_CREATE:
                        Console.WriteLine("Create a new quiz: ");
                        List<Question> newQuestions = ui.AddQuestions();

                        if (newQuestions.Count > 0)
                        {
                            quiz.AddQuestions(newQuestions);
                            quiz.SaveAll();
                            Console.WriteLine("Questions added and saved successfully!");
                        }
                        else
                        {
                            Console.WriteLine("No questions were added.");
                        }
                        break;

                    case Constants.MODE_PLAY:
                        if (quiz.GetQuestionCount() == 0)
                        {
                            Console.WriteLine("No questions avaible. Please Create a quiz or load a quiz first.");
                        }
                        else
                        {
                            Console.WriteLine("Play the quiz: ");
                            ui.PlayQuiz(quiz);
                        }
                        break;

                    case Constants.MODE_REMOVE_QUESTIONS:
                        if (quiz.GetQuestionCount() == 0)
                        {
                            Console.WriteLine("No questions avaiable to remove. Please create a quiz or load a quiz first.");
                        }
                        else
                        {
                            Console.WriteLine("Remove Questions: ");
                            ui.RemoveQuestion(quiz);
                        }
                        break;

                    case Constants.MODE_SAVE:
                        quiz.SaveToDefault();
                        Console.WriteLine($"Quiz saved successfully to {Constants.DEFAULT_FILE_PATH}!");
                        break;

                    case Constants.MODE_LOAD: 
                        try
                        {
                            quiz = QuizManager.LoadFromDefault();
                            Console.WriteLine($"Quiz loaded successfully from {Constants.DEFAULT_FILE_PATH}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Failed to load quiz: {e.Message}");
                        }
                        break;

                    case Constants.MODE_EXIT:
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid Choice. Please select a valid option.");
                        break;
                }
            }
        }
    }
}
