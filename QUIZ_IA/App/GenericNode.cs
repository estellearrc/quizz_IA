using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    // classe abstraite, il est donc impératif de créer une classe qui en hérite
    // pour résoudre un problème particulier en y ajoutant des infos liées au contexte du problème
    abstract public class GenericNode
    {
        public double GCost { get; protected set; }               //coût du chemin du noeud initial jusqu'à ce noeud
        public double HCost { get; protected set; }               //estimation heuristique du coût pour atteindre le noeud final
        public double TotalCost { get; protected set; }           //coût total (g+h)
        protected GenericNode _parentNode;
        public GenericNode ParentNode                             // noeud parent
        {
            get
            {
                return _parentNode;
            }
            protected set
            {
                ParentNode = value;
                value.Enfants.Add(this);
            }
        }     
        public List<GenericNode> Enfants { get; protected set; }  // noeuds enfants

        public GenericNode()
        {
            ParentNode = null;
            Enfants = new List<GenericNode>();
        }

        public void SupprimeLienParent()
        {
            if (ParentNode != null)
            {
                ParentNode.Enfants.Remove(this);
                ParentNode = null;
            }
        }

        public void CalculCoutTotal()
        {
            HCost = CalculeHCost();
            TotalCost = GCost + HCost;
        }

        public void RecalculeCoutTotal()
        {
            TotalCost = GCost + HCost;
        }

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public abstract bool IsEqual(GenericNode N);
        public abstract bool EndState();
        public abstract List<GenericNode> GetListSucc();
        public abstract double CalculeHCost();
        // On peut aussi penser à surcharger ToString() pour afficher correctement un état
        // c'est utile pour l'affichage du treenode
    }
}
