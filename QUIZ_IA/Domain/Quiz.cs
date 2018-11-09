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

        /// <summary>
        /// La note du quiz
        /// </summary>
        public virtual int Score { get; set; }


        /// <summary>
        /// Liste des questions du quiz 
        /// </summary>
        public virtual List<Question> LesQuestions { get; set; }


        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Quiz() { }

        /// <summary>
        /// Constructeur question sans image
        /// </summary>

        public Quiz(List<Question> questions)
        {
            LesQuestions = questions;
            Score = 0;
        }


    }
}
