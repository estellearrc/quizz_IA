﻿using System;
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
        public virtual int Id { get; set; }

        public virtual string Intitule { get; set; }
        public virtual bool EstCorrecte { get; set; }

        public Reponse() { }
       
        public Reponse(string intitule, bool estCorrecte)
        {
            Intitule = intitule;
            EstCorrecte = estCorrecte;
        }

       
    }
}
