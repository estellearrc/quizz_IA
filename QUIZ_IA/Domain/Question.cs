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

        /// <summary>
        /// Création de l'énumération permettant de mieux savoir de quel type de question il s'agit
        /// </summary>
        public enum TypeQues { QCM, saisieNum }


        /// <summary>
        /// IDentifiant de la question
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Intitulé de la question
        /// </summary>
        public virtual string Intitule { get; set; }

        /// <summary>
        /// Image associé à la question
        /// </summary>
        public virtual string Image { get; set; }


        /// <summary>
        /// Liste des réponses associées à la question 
        /// </summary>
        public virtual List<Reponse> LesReponses { get; set; }

        /// <summary>
        /// Le type de question (QCM ou saisie numérique)
        /// </summary>
        public virtual TypeQues Type { get; set; }
 
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Question() { }

        /// <summary>
        /// Constructeur question sans image
        /// </summary>

        public Question(int id,string intitule, List<Reponse> lesReponses, int type)
        {
            Id = id;
            Intitule = intitule;
            LesReponses = lesReponses;
            Type = (TypeQues)type;
        }

        /// <summary>
        /// Constructeur question avec image
        /// </summary>

        public Question(int id, string intitule, List<Reponse> lesReponses, string image, int type)
        {
            Id = id;
            Intitule = intitule;
            LesReponses = lesReponses;
            Type = (TypeQues)type;
            Image = image;
        }
       
    }
}
