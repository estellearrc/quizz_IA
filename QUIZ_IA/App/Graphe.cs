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
        public static Random rnd = new Random();
        public List<Sommet> OpenPoints { get; private set; } //liste des noeuds ouverts
        public List<Sommet> Closedpoints { get; private set; } //liste des noeuds fermés
        public Sommet CurrentPoint { get; private set; }
        public List<Arete> Aretes { get; private set; }
        public Sommet InitialPoint { get; private set; }
        public Sommet LastPoint { get; private set; }
        
        public Graphe(Dijkstra d)
        {
            OpenPoints = new List<Sommet>();
            GenerePoints(d);
            Closedpoints = new List<Sommet>();
            Aretes = new List<Arete>();
            ConnectePoints();
            CurrentPoint = OpenPoints.First();
            InitialPoint = OpenPoints.First();
            LastPoint = OpenPoints.Last();
        }
        public void GenerePoints(Dijkstra d)
        {
            int nbPoints = 7;
            OpenPoints.Add(new Sommet(1, 1));
            OpenPoints.Add(new Sommet(2, 5));
            OpenPoints.Add(new Sommet(3, 3));
            OpenPoints.Add(new Sommet(1, 4));
            OpenPoints.Add(new Sommet(4, 6));
            OpenPoints.Add(new Sommet(5, 7));
            OpenPoints.Add(new Sommet(7, 7));
            //int nbPoints = rnd.Next(5, LabelledPoint.alphabet.Length + 1);
            //for (int i = 0; i < nbPoints; i++)
            //{
            //    float x = rnd.Next((int)d.xMin + 1, (int)d.xMax);
            //    float y = rnd.Next((int)d.yMin + 1 , (int)d.yMax);
            //    PointsToScan.Add(new LabelledPoint(x, y));
            //}
        }
        public void ConnectePoints()
        {
            int nbPoints = OpenPoints.Count;
            int nbRelMax = nbPoints * nbPoints;
            int nbRelations = 7; // rnd.Next(2 * nbPoints);
            Aretes.Add(new Arete(OpenPoints[0], OpenPoints[1]));
            Aretes.Add(new Arete(OpenPoints[0], OpenPoints[2]));
            Aretes.Add(new Arete(OpenPoints[0], OpenPoints[3]));
            Aretes.Add(new Arete(OpenPoints[1], OpenPoints[4]));
            Aretes.Add(new Arete(OpenPoints[2], OpenPoints[4]));
            Aretes.Add(new Arete(OpenPoints[4], OpenPoints[5]));
            Aretes.Add(new Arete(OpenPoints[5], OpenPoints[6]));
            //for(int i = 0; i < nbRelations; i++)
            //{
            //    int index1 = rnd.Next(nbPoints);
            //    int index2 = rnd.Next(nbPoints);
            //    Relations.Add(new Relation(PointsToScan[index1], PointsToScan[index2]));
            //}
        }

        public List<GenericNode> RechercheSolutionAEtoile(GenericNode N0)
        {
            OpenPoints = new List<GenericNode>();
            Closedpoints = new List<GenericNode>();
            // Le noeud passé en paramètre est supposé être le noeud initial
            GenericNode N = N0;
            L_Ouverts.Add(N0);

            // tant que le noeud n'est pas terminal et que ouverts n'est pas vide
            while (L_Ouverts.Count != 0 && N.EndState() == false)
            {
                // Le meilleur noeud des ouverts est supposé placé en tête de liste
                // On le place dans les fermés
                L_Ouverts.Remove(N);
                L_Fermes.Add(N);

                // Il faut trouver les noeuds successeurs de N
                this.MAJSuccesseurs(N);
                // Inutile de retrier car les insertions ont été faites en respectant l'ordre

                // On prend le meilleur, donc celui en position 0, pour continuer à explorer les états
                // A condition qu'il existe bien sûr
                if (L_Ouverts.Count > 0)
                {
                    N = L_Ouverts[0];
                }
                else
                {
                    N = null;
                }
            }

            // A* terminé
            // On retourne le chemin qui va du noeud initial au noeud final sous forme de liste
            // Le chemin est retrouvé en partant du noeud final et en accédant aux parents de manière
            // itérative jusqu'à ce qu'on tombe sur le noeud initial
            List<GenericNode> _LN = new List<GenericNode>();
            if (N != null)
            {
                _LN.Add(N);

                while (N != N0)
                {
                    N = N.GetNoeud_Parent();
                    _LN.Insert(0, N);  // On insère en position 1
                }
            }
            return _LN;
        }

        private void MAJSuccesseurs(GenericNode N)
        {
            // On fait appel à GetListSucc, méthode abstraite qu'on doit réécrire pour chaque
            // problème. Elle doit retourner la liste complète des noeuds successeurs de N.
            List<GenericNode> listsucc = N.GetListSucc();
            foreach (GenericNode N2 in listsucc)
            {
                // N2 est-il une copie d'un nœud déjà vu et placé dans la liste des fermés ?
                GenericNode N2bis = ChercheNodeDansFermes(N2);
                if (N2bis == null)
                {
                    // Rien dans les fermés. Est-il dans les ouverts ?
                    N2bis = ChercheNodeDansOuverts(N2);
                    if (N2bis != null)
                    {
                        // Il existe, donc on l'a déjà vu, N2 n'est qu'une copie de N2Bis
                        // Le nouveau chemin passant par N est-il meilleur ?
                        if (N.GetGCost() + N.GetArcCost(N2) < N2bis.GetGCost())
                        {
                            // Mise à jour de N2bis
                            N2bis.SetGCost(N.GetGCost() + N.GetArcCost(N2));
                            // HCost pas recalculé car toujours bon
                            N2bis.RecalculeCoutTotal(); // somme de GCost et HCost
                            // Mise à jour de la famille ....
                            N2bis.Supprime_Liens_Parent();
                            N2bis.SetNoeud_Parent(N);
                            // Mise à jour des ouverts
                            L_Ouverts.Remove(N2bis);
                            this.InsertNewNodeInOpenList(N2bis);
                        }
                        // else on ne fait rien, car le nouveau chemin est moins bon
                    }
                    else
                    {
                        // N2 est nouveau, MAJ et insertion dans les ouverts
                        N2.SetGCost(N.GetGCost() + N.GetArcCost(N2));
                        N2.SetNoeud_Parent(N);
                        N2.calculCoutTotal(); // somme de GCost et HCost
                        this.InsertNewNodeInOpenList(N2);
                    }
                }
                // else il est dans les fermés donc on ne fait rien,
                // car on a déjà trouvé le plus court chemin pour aller en N2
            }
        }

        public void InsertNewNodeInOpenList(GenericNode NewNode)
        {
            // Insertion pour respecter l'ordre du cout total le plus petit au plus grand
            if (this.L_Ouverts.Count == 0)
            { L_Ouverts.Add(NewNode); }
            else
            {
                GenericNode N = L_Ouverts[0];
                bool trouve = false;
                int i = 0;
                do
                    if (NewNode.Cout_Total < N.Cout_Total)
                    {
                        L_Ouverts.Insert(i, NewNode);
                        trouve = true;
                    }
                    else
                    {
                        i++;
                        if (L_Ouverts.Count == i)
                        {
                            N = null;
                            L_Ouverts.Insert(i, NewNode);
                        }
                        else
                        { N = L_Ouverts[i]; }
                    }
                while ((N != null) && (trouve == false));
            }
        }

        // Si on veut afficher l'arbre de recherche, il suffit de passer un treeview en paramètres
        // Celui-ci est mis à jour avec les noeuds de la liste des fermés, on ne tient pas compte des ouverts
        public void GetSearchTree(TreeView TV)
        {
            if (L_Fermes == null) return;
            if (L_Fermes.Count == 0) return;

            // On suppose le TreeView préexistant
            TV.Nodes.Clear();

            TreeNode TN = new TreeNode(L_Fermes[0].ToString());
            TV.Nodes.Add(TN);

            AjouteBranche(L_Fermes[0], TN);
        }

        // AjouteBranche est exclusivement appelée par GetSearchTree; les noeuds sont ajoutés de manière récursive
        private void AjouteBranche(GenericNode GN, TreeNode TN)
        {
            foreach (GenericNode GNfils in GN.GetEnfants())
            {
                TreeNode TNfils = new TreeNode(GNfils.ToString());
                TN.Nodes.Add(TNfils);
                if (GNfils.GetEnfants().Count > 0) AjouteBranche(GNfils, TNfils);
            }
        }
    }
}
