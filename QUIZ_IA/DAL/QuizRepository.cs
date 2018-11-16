using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public class QuizRepository : Repository, IQuizRepository
    {
        public List<Question> GetAllQuestions()
        {
            return Session.Query<Question>().ToList();
        }

        public List<Question> Get20Questions()
        {
            List<Question> allQuestions = GetAllQuestions();
            List<Question> twentyQuestions = new List<Question>();
            Random random = new Random();
            for (int i = 0; i<20;i++)
            {
                int randomQuestion = random.Next(0, allQuestions.Count);
                twentyQuestions.Add(allQuestions[randomQuestion]);
                allQuestions.Remove(allQuestions[randomQuestion]);
            }
            return twentyQuestions;
        }
        public int CalculateMaxMark()
        {
            string requete = "SELECT SUM(point) FROM question";
            string result = Session.CreateSQLQuery(requete).ToString();
            return Convert.ToInt32(result);
        }
    }
}
