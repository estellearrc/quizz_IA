using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{ /// <summary>
  /// Permet de creer un quiz (un ensemble de questions)
  /// </summary>
    public class Quiz
    {
        public int Score { get; set; }
        
        public List<Question> LesQuestions { get; set; }

        public Quiz(List<Question> questions)
        {
            LesQuestions = questions;
            Score = 0;
        }

        public void ActualiseScore(int points)
        {
            Score += points;
        }

        public void NoteSur20(int noteMax)
        {
            Score = (Score * 20) / noteMax; 
        }

    }
}
