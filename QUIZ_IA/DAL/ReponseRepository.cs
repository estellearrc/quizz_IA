using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public class ReponseRepository : Repository, IReponseRepository
    {
        public int CalculateMaxMark()
        {
            string requete = "SELECT SUM(points) FROM reponse";
            string result = Session.CreateSQLQuery(requete).ToString();
            return Convert.ToInt32(result);
        }
    }
}
