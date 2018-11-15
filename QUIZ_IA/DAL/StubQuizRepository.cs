using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public class StubQuizRepository : IQuizRepository
    {
        private Quiz _quiz;
        public StubQuizRepository()
        {
            List<Reponse> lesRepq = new List<Reponse>();
            lesRepq.Add(new Reponse("L'IA forte a conscience d'elle-même", true));
            lesRepq.Add(new Reponse("L'IA forte est plus rapide que l'IA faible", false));
            lesRepq.Add(new Reponse("L'IA forte écrase la faible aux échecs", false));
            lesRepq.Add(new Reponse("L'IA faible ne sait faire qu'une seule tâche", true));
            Question quest1 = new Question("Qu'est-ce qui différencie l'IA forte de l'IA faible ?", lesRepq, 0, 2);

            List<Reponse> lesRepq2 = new List<Reponse>();
            lesRepq2.Add(new Reponse("L'algorithme A* est une version simplifiée de Dijkstra", false));
            lesRepq2.Add(new Reponse("L'ajout d’une heuristique pour estimer le coût du chemin restant pour atteindre le but", true));
            lesRepq2.Add(new Reponse("L'ajout d’une heuristique pour estimer le coût du chemin restant pour atteindre le but", false));
            Question quest2 = new Question("Quel est l'intérêt d'utiliser A* plutôt que Dijkstra classique ?", lesRepq2, 0, 1);

            List<Question> questions = new List<Question>();
            questions.Add(quest1);
            questions.Add(quest2);

            _quiz = new Quiz(questions);
        }
        public int CalculateMaxMark()
        {
            int noteMax = 0;
            foreach(Question q in _quiz.LesQuestions)
            {
                noteMax += q.Points;
            }
            return noteMax;
        }

        public List<Question> Get20Questions()
        {
            throw new NotImplementedException();
        }

        public List<Question> GetAllQuestions()
        {
            return _quiz.LesQuestions;
        }
    }
}
