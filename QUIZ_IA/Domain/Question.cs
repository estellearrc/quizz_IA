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

        public virtual string Image { get; set; }

        public virtual IList<Reponse> LesReponses { get; set; }

        public virtual TypeQues Type { get; set; }

        public Question() { }

        public Question(string intitule, List<Reponse> lesReponses, int type)
        {
            Intitule = intitule;
            LesReponses = lesReponses;
            Type = (TypeQues)type;
        }

        public Question(string intitule, List<Reponse> lesReponses, string image, int type)
        {
            Intitule = intitule;
            LesReponses = lesReponses;
            Type = (TypeQues)type;
            Image = image;
        }
       
    }
}
