using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{       /// <summary>
        /// Modélise une réponse possible à une question en donnant son intitulé 
        /// et en disant si il s'agit d'une bonne réponse ou non
        /// </summary>
    public class Reponse
    {
        /// <summary>
        /// IDentifiant de la réponse
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Intitulé de la réponse
        /// </summary>
        public virtual string Intitule { get; set; }

        /// <summary>
        /// Nombre de points associés à la question
        /// </summary>
        public virtual int Points { get; set; }


        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Reponse() { }

        /// <summary>
        /// Constructeur de Réponses
        /// </summary>

        public Reponse(int id, string intitule, int points)
        {
            Id = id;
            Intitule = intitule;
            Points = points;
        }

        public bool EstVrai()
        {
            if (Points == 0) return false;
            return true;
        }

    }
}
