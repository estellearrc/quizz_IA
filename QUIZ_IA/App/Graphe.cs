using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Graphe
    {
        //pour la génération automatique de graphes : gestion de plusieurs solutions possibles avec un simple Dijkstra ou A* : vérifier que le coût du chemin trouvé par l'utilisateur est égal à celui trouvé par l'algo
        //distance minimale entre le point initial et le point final à respecter, genre min 5
        //2 modes pour l'utilisateur : donner les ouverts et fermés pour Dijkstra ou A*
        //affichage de l'arbre de recherche final sur le form + le meilleur chemin trouvé et son coût

        public static Random rnd = new Random();
        public List<Sommet> PointsOuverts { get; private set; } //liste des noeuds ouverts
        public List<Sommet> PointsFermes { get; private set; } //liste des noeuds fermés
        public Sommet PointActuel { get; private set; }
        public List<Arete> Aretes { get; private set; }
        public Sommet PointInitial { get; private set; }
        public Sommet PointFinal { get; private set; }
        
        public Graphe(Dijkstra d)
        {
            PointsOuverts = new List<Sommet>();
            GenereSommets(d);
            PointsFermes = new List<Sommet>();
            Aretes = new List<Arete>();
            ConnecteSommets();
            PointInitial = DeterminePointInitial();
            PointActuel = PointInitial;
            PointFinal = DeterminePointFinal();
        }
        public Sommet DeterminePointFinal()
        {
            Sommet s;
            do
            {
                int n = PointsOuverts.Count;
                int k = rnd.Next(n);
                s = PointsOuverts[k];
            }
            while (s.CalculeDistance(PointInitial) < 5);
            return s;
        }
        public Sommet DeterminePointInitial()
        {
            int n = PointsOuverts.Count;
            int i = rnd.Next(n);
            return PointsOuverts[i];
        }
        public void GenereSommets(Dijkstra d)
        {
            //int nbPoints = 7;
            //PointsOuverts.Add(new Sommet(1, 1));
            //PointsOuverts.Add(new Sommet(2, 5));
            //PointsOuverts.Add(new Sommet(3, 3));
            //PointsOuverts.Add(new Sommet(1, 4));
            //PointsOuverts.Add(new Sommet(4, 6));
            //PointsOuverts.Add(new Sommet(5, 7));
            //PointsOuverts.Add(new Sommet(7, 7));
            int nbPoints = rnd.Next(5, MainForm.alphabet.Length + 1);
            for (int i = 0; i < nbPoints; i++)
            {
                int x = rnd.Next(d.xMin + 1, d.xMax);
                int y = rnd.Next(d.yMin + 1, d.yMax);
                Sommet s = new Sommet(x, y);
                if(!PointsOuverts.Contains(s))
                {
                    PointsOuverts.Add(s);
                }
            }
        }
        /// <summary>
        /// Génère le Graphe de Voisinage Relatif (GVR) d'après le nuage de points PointsOuverts
        /// </summary>
        public void ConnecteSommets()
        {
            int n = PointsOuverts.Count;
            for (int i = 0; i < n; i++)
            {
                Sommet s1 = PointsOuverts[i];
                for(int j = i + 1; j < n; j++) //Parcours "en triangle"
                {
                    Sommet s2 = PointsOuverts[j];
                    if (DoiventEtreRelies(s1, s2)) //Si les deux sommets doivent être reliés
                    {
                        Aretes.Add(new Arete(s1, s2));
                    }
                }
            }
            //Aretes.Add(new Arete(PointsOuverts[0], PointsOuverts[1]));
            //Aretes.Add(new Arete(PointsOuverts[0], PointsOuverts[2]));
            //Aretes.Add(new Arete(PointsOuverts[0], PointsOuverts[3]));
            //Aretes.Add(new Arete(PointsOuverts[1], PointsOuverts[4]));
            //Aretes.Add(new Arete(PointsOuverts[2], PointsOuverts[4]));
            //Aretes.Add(new Arete(PointsOuverts[4], PointsOuverts[5]));
            //Aretes.Add(new Arete(PointsOuverts[5], PointsOuverts[6]));
        }
        /// <summary>
        /// Prédicat vrai ssi le graphe de voisinage relatif comporte l'arête entre s1 et s2
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public bool DoiventEtreRelies(Sommet s1, Sommet s2)
        {
            int r = s1.CalculeDistance(s2); //Rayon des cercles dont l'intersection est le domaine d'exclusion
            foreach (Sommet s in PointsOuverts)
            {
                if (s.CalculeDistance(s1) < r && s.CalculeDistance(s2) < r) //Évaluation paresseuse plutôt que de rechercher le maximum
                {
                    if (!s.IsEqual(s1) && !s.IsEqual(s2)) //Pour ne pas tester le sommet avec lui-même ; condition testée en second
                    {
                        return false; //Sortie de boucle si un sommet est dans le domaine d'exclusion
                    }
                }
            }
            return true;
        }
        public List<Sommet> RechercheSolutionAEtoile(Sommet s0)
        {
            PointsOuverts = new List<Sommet>();
            PointsFermes = new List<Sommet>();
            // Le noeud passé en paramètre est supposé être le noeud initial
            Sommet s = s0;
            PointsOuverts.Add(s0);

            // tant que le noeud n'est pas terminal et que ouverts n'est pas vide
            while (PointsOuverts.Count != 0 && s.EndState() == false)
            {
                // Le meilleur noeud des ouverts est supposé placé en tête de liste
                // On le place dans les fermés
                PointsOuverts.Remove(s);
                PointsFermes.Add(s);

                // Il faut trouver les noeuds successeurs de s
                MAJSuccesseurs(s);
                // Inutile de retrier car les insertions ont été faites en respectant l'ordre

                // On prend le meilleur, donc celui en position 0, pour continuer à explorer les états
                // A condition qu'il existe bien sûr
                if (PointsOuverts.Count > 0)
                {
                    s = PointsOuverts[0];
                }
                else
                {
                    s = null;
                }
            }

            // A* terminé
            // On retourne le chemin qui va du noeud initial au noeud final sous forme de liste
            // Le chemin est retrouvé en partant du noeud final et en accédant aux parents de manière
            // itérative jusqu'à ce qu'on tombe sur le noeud initial
            List<Sommet> sommets = new List<Sommet>();
            if (s != null)
            {
                sommets.Add(s);

                while (s != s0)
                {
                    s = s.SommetParent;
                    sommets.Insert(0, s);  // On insère en position 1
                }
            }
            return sommets;
        }

        private void MAJSuccesseurs(Sommet s)
        {
            // On fait appel à GetListSucc, méthode abstraite qu'on doit réécrire pour chaque
            // problème. Elle doit retourner la liste complète des noeuds successeurs de s.
            List<Sommet> listsucc = s.GetSuccesseurs();
            foreach (Sommet succ in listsucc)
            {
                // succ est-il une copie d'un nœud déjà vu et placé dans la liste des fermés ?
                Sommet succBis = PointsFermes.Find(x => x.IsEqual(succ));
                if (succBis == null)
                {
                    // Rien dans les fermés. Est-il dans les ouverts ?
                    succBis = PointsOuverts.Find(x => x.IsEqual(succ));
                    if (succBis != null)
                    {
                        // Il existe, donc on l'a déjà vu, succ n'est qu'une copie de N2Bis
                        // Le nouveau chemin passant par s est-il meilleur ?
                        if (s.CoutCumule + RetrouveArete(s,succ).Cout < succBis.CoutCumule)
                        {
                            // Mise à jour de succBis
                            succBis.CoutCumule = s.CoutCumule + RetrouveArete(s, succ).Cout;
                            // HCost pas recalculé car toujours bon
                            succBis.RecalculeCoutTotal(); // somme de CoutCumule et CoutHeuristique
                            // Mise à jour de la famille ....
                            succBis.SupprimeLienParent();
                            succBis.SommetParent = s;
                            // Mise à jour des ouverts
                            PointsOuverts.Remove(succBis);
                            InsertNewNodeInOpenList(succBis);
                        }
                        // else on ne fait rien, car le nouveau chemin est moins bon
                    }
                    else
                    {
                        // succ est nouveau, MAJ et insertion dans les ouverts
                        succ.CoutCumule = s.CoutCumule + RetrouveArete(s, succ).Cout;
                        succ.SommetParent = s;
                        succ.CalculCoutTotal(); // somme de GCost et HCost
                        InsertNewNodeInOpenList(succ);
                    }
                }
                // else il est dans les fermés donc on ne fait rien,
                // car on a déjà trouvé le plus court chemin pour aller en succ
            }
        }

        public void InsertNewNodeInOpenList(Sommet NewNode)
        {
            // Insertion pour respecter l'ordre du cout total le plus petit au plus grand
            if (this.PointsOuverts.Count == 0)
            { PointsOuverts.Add(NewNode); }
            else
            {
                Sommet s = PointsOuverts[0];
                bool trouve = false;
                int i = 0;
                do
                    if (NewNode.CoutTotal < s.CoutTotal)
                    {
                        PointsOuverts.Insert(i, NewNode);
                        trouve = true;
                    }
                    else
                    {
                        i++;
                        if (PointsOuverts.Count == i)
                        {
                            s = null;
                            PointsOuverts.Insert(i, NewNode);
                        }
                        else
                        { s = PointsOuverts[i]; }
                    }
                while ((s != null) && (trouve == false));
            }
        }
        public Arete RetrouveArete(Sommet s1, Sommet s2)
        {
            return Aretes.Find(x => x.IsEqual(new Arete(s1, s2)));
        }

        // Si on veut afficher l'arbre de recherche, il suffit de passer un treeview en paramètres
        // Celui-ci est mis à jour avec les noeuds de la liste des fermés, on ne tient pas compte des ouverts
        //public void GetSearchTree(TreeView TV)
        //{
        //    if (PointsFermes == null) return;
        //    if (PointsFermes.Count == 0) return;

        //    // On suppose le TreeView préexistant
        //    TV.Nodes.Clear();

        //    TreeNode TN = new TreeNode(PointsFermes[0].ToString());
        //    TV.Nodes.Add(TN);

        //    AjouteBranche(PointsFermes[0], TN);
        //}

        //// AjouteBranche est exclusivement appelée par GetSearchTree; les noeuds sont ajoutés de manière récursive
        //private void AjouteBranche(Sommet GN, TreeNode TN)
        //{
        //    foreach (Sommet GNfils in GN.Enfants)
        //    {
        //        TreeNode TNfils = new TreeNode(GNfils.ToString());
        //        TN.Nodes.Add(TNfils);
        //        if (GNfils.Enfants.Count > 0) AjouteBranche(GNfils, TNfils);
        //    }
        //}
    }
}
