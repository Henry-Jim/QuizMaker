namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Quiz Maker! ");

            Quiz quiz = new Quiz();
            UI ui = new UI(quiz);

            while (true)
            {
                Console.WriteLine("\nMain Menu: ");
                Console.WriteLine($"{Constants.MODE_CREATE}. Create a new quiz");
                Console.WriteLine($"{Constants.MODE_PLAY}. Play an existing quiz");
                Console.WriteLine($"{Constants.MODE_REMOVE_QUESTIONS}. Remove questions from a quiz");
                Console.WriteLine($"{Constants.MODE_SAVE}. Save the current quiz");
                Console.WriteLine($"{Constants.MODE_LOAD}. Load a saved quiz");
                Console.WriteLine($"{Constants.MODE_EXIT}. Exit");

                Console.WriteLine("Choose an option");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                    switch (choice)
                {
                    case Constants.MODE_CREATE:
                        Console.WriteLine("Create a new quiz: ");
                        ui.AddQuestions();
                        break;

                    case Constants.MODE_PLAY:
                        if (quiz.GetQuestionCount() == 0)
                        {
                            Console.WriteLine("No questions avaible. Please Create a quiz or load a quiz first.");
                        }
                        else
                        {
                            Console.WriteLine("Play the quiz: ");
                            ui.PlayQuiz();
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
                            ui.RemoveQuestion();
                        }
                        break;

                    case Constants.MODE_SAVE:
                        quiz.SaveToDefault();
                        Console.WriteLine($"Quiz saved successfully to {Constants.DEFAULT_FILE_PATH}!");
                        break;

                    case Constants.MODE_LOAD: 
                        try
                        {
                            quiz = Quiz.LoadFromDefault();
                            ui = new UI(quiz);
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
