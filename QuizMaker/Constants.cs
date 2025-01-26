using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    internal class Constants
    {
        public const string DEFAULT_FILE_PATH = "quiz_data.xml";

        public const int ININTIAL_SCORE = 0;

        public const int MODE_CREATE = 1;
        public const int MODE_PLAY = 2;
        public const int MODE_REMOVE_QUESTIONS = 3;
        public const int MODE_SAVE = 4;
        public const int MODE_LOAD = 5;
        public const int MODE_EXIT = 6;
    }
}
