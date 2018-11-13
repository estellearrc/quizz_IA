using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Modélise une question d'un quiz
    /// </summary>
    public class Question
    {
        public enum TypeQues { QCM, saisieNum }
        
        public virtual int Id { get; set; }

        public virtual string Intitule { get; set; }

        public virtual string PieceJointe { get; set; }

        public virtual IList<Reponse> LesReponses { get; set; }

        public virtual TypeQues Type { get; set; }

        public virtual int Points { get; set; }

        public Question() { }

        public Question(string intitule, List<Reponse> lesReponses, int type,int point)
        {
            Intitule = intitule;
            LesReponses = lesReponses;
            Type = (TypeQues)type;
            Points = point;
        }

        public Question(string intitule, List<Reponse> lesReponses, string pieceJointe, int type, int point)
        {
            Intitule = intitule;
            LesReponses = lesReponses;
            Type = (TypeQues)type;
            PieceJointe = pieceJointe;
            Points = point;
        }
       
    }
}
