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
        //mais surtout, ne proposer qu'une solution correcte à l'utilisateur pour les ouverts et fermés et toutes les autres incorrectes
        //distance minimale entre le point initial et le point final à respecter, genre min 5
        //2 modes pour l'utilisateur : donner les ouverts et fermés pour Dijkstra ou A*
        //affichage de l'arbre de recherche final sur le form + le meilleur chemin trouvé et son coût

        //propriétés utiles pour l'affichage du graphe
        public static Random rnd = new Random();
        public List<Arete> Aretes { get; private set; }
        public List<Sommet> Sommets { get; private set; } //liste des noeuds du graphe

        //Propriétés utiles pour la résolution du graphe au sens du plus court chemin 
        public List<Sommet> SommetsOuverts { get; private set; } //liste des noeuds ouverts
        public List<Sommet> SommetsFermes { get; private set; } //liste des noeuds fermés
        public Sommet SommetActuel { get; set; }
        public Sommet SommetInitial { get; private set; }
        public Sommet SommetFinal { get; private set; }
        public List<Sommet> PlusCourtChemin { get; private set; }
        public double CoutPlusCourtChemin { get; private set; }
        public List<List<Sommet>> EtatsSuccessifsOuverts { get; private set; }
        public List<List<Sommet>> EtatsSuccessifsFermes { get; private set; }
        public bool ResolutionAEtoile { get; private set; }

        public Graphe(int xMin, int xMax, int yMin, int yMax)
        {
            Sommets = new List<Sommet>();
            GenereSommets(xMin,xMax,yMin,yMax); //initialise la liste Sommets
            Aretes = new List<Arete>();
            ConnecteSommets(); //initialise la liste Aretes
        }
        public Sommet DeterminePointInitial() //problème : attention au graphe linéaire, i.e dont tous les sommets n'ont qu'une seule incidence... réglé avec compteur
        {
            Sommet s;
            int compteur = 0;
            List<int> indicesDejaTestes = new List<int>();
            int n = Sommets.Count;
            do
            {
                int i;
                do
                {
                    i = rnd.Next(n);
                }
                while (indicesDejaTestes.Contains(i));
                indicesDejaTestes.Add(i);
                s = Sommets[i];
                compteur++;
            }
            while (s.Incidences.Count < 2 && compteur < n);
            return s;
        }
        public Sommet DeterminePointFinal() //problème : attention au graphe linéaire, i.e dont tous les sommets n'ont qu'une seule incidence... réglé avec compteur
        {
            Sommet s;
            int compteur = 0;
            double distanceMin;
            List<int> indicesDejaTestes = new List<int>();
            int n = Sommets.Count;
            do
            {
                int k;
                do
                {
                    k = rnd.Next(n);
                }
                while (indicesDejaTestes.Contains(k));
                indicesDejaTestes.Add(k);
                s = Sommets[k];
                distanceMin = CalculeDistanceMinimale(s, SommetInitial);
                compteur++;
            }
            while (s.CalculeDistance(SommetInitial) <= 2*distanceMin/3 && compteur < n ); //|| s.Incidences.Count < 2);
            return s;
        }
        /// <summary>
        /// Calcule la distance minimale entre 2 sommets s1 et s2 pour qu'ils soient séparés d'au moins 2 sommets
        /// </summary>
        public double CalculeDistanceMinimale(Sommet s1, Sommet s2)
        {
            // création de deep copies des listes d'incidences des somets s1 et s2 pour ne pas que ces dernières soient modifiées
            List<Arete> incidencesS1 = DeepCopy(s1.Incidences);
            List<Arete> incidencesS2 = DeepCopy(s2.Incidences);

            //calcul de la distance max sur ces 2 nouvelles listes
            double distanceMax1 = CalculeDistanceMax(incidencesS1,0);
            double distanceMax2 = CalculeDistanceMax(incidencesS2, 0);

            double distanceMin = distanceMax1 + distanceMax2;
            return distanceMin;

        }
        public List<Arete> DeepCopy(List<Arete> liste)
        {
            Arete[] tab = new Arete[liste.Count];
            liste.CopyTo(tab);
            List<Arete> copieListe = new List<Arete>(tab);
            return copieListe;
        }
        public List<Sommet> DeepCopy(List<Sommet> liste)
        {
            Sommet[] tab = new Sommet[liste.Count];
            liste.CopyTo(tab);
            List<Sommet> copieListe = new List<Sommet>(tab);
            return copieListe;
        }
        /// <summary>
        /// Calcule la distance entre un sommet et son sommet voisin le plus éloigné
        /// </summary>
        public double CalculeDistanceMax(List<Arete> incidences, double distanceMax)
        {
            if(incidences.Count == 1)
            {
                return distanceMax;
            }
            else
            {
                Arete a1 = incidences[0];
                Arete a2 = incidences[1];
                if(a1.Cout < a2.Cout)
                {
                    incidences.Remove(a1);
                    return CalculeDistanceMax(incidences, a2.Cout);
                }
                else
                {
                    incidences.Remove(a2);
                    return CalculeDistanceMax(incidences, a1.Cout);
                }
            }
        }
        public void GenereSommets(int xMin, int xMax, int yMin, int yMax)
        {
            //int nbPoints = 7;
            //Sommets.Add(new Sommet(1, 1));
            //Sommets.Add(new Sommet(2, 5));
            //Sommets.Add(new Sommet(3, 3));
            //Sommets.Add(new Sommet(1, 4));
            //Sommets.Add(new Sommet(4, 6));
            //Sommets.Add(new Sommet(5, 7));
            //Sommets.Add(new Sommet(7, 7));
            int nbPoints = rnd.Next(5, MainForm.alphabet.Length + 1);
            for (int i = 0; i < nbPoints; i++)
            {
                float partieDecimaleX = (float)rnd.NextDouble();
                float partieDecimaleY = (float)rnd.NextDouble();
                float x = rnd.Next(xMin + 1, xMax - 1) + partieDecimaleX;
                float y = rnd.Next(yMin + 1, yMax - 1) + partieDecimaleY;
                Sommet s = new Sommet(x, y,true);
                if (!Sommets.Exists(z => z.IsEqual(s) || (z.CalculeDistance(s) < 1))) //si le sommet s n'est pas déjà dans les sommets du graphe et à une distance >= 1 des autres sommets
                {
                    Sommets.Add(s);
                }
            }
        }
        /// <summary>
        /// Génère le Graphe de Voisinage Relatif (GVR) d'après le nuage de points Sommets
        /// </summary>
        public void ConnecteSommets()
        {
            int n = Sommets.Count;
            for (int i = 0; i < n; i++)
            {
                Sommet s1 = Sommets[i];
                for(int j = i + 1; j < n; j++) //Parcours "en triangle"
                {
                    Sommet s2 = Sommets[j];
                    if (DoiventEtreRelies(s1, s2)) //Si les deux sommets doivent être reliés
                    {
                        Arete a = new Arete(s1, s2);
                        Aretes.Add(a);
                        s1.Incidences.Add(a);
                        s2.Incidences.Add(a);
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
        /// Prédicat vrai ssi le graphe de Gabriel comporte l'arête entre s1 et s2
        /// </summary>
        public bool DoiventEtreRelies(Sommet s1, Sommet s2)
        {
            Sommet s0 = s1.CalculeMilieu(s2); //Milieu de s1 et s2
            double r = s1.CalculeDistance(s2) / 2; //Rayon du disque ouvert d'exclusion
            foreach (Sommet s in Sommets)
            {
                if (s.CalculeDistance(s0) < r) //Si un sommet est dans le disque ouvert
                {
                    if (!s.IsEqual(s1) && !s.IsEqual(s2)) //Pour ne pas tester le sommet avec lui-même 
                    {
                        return false; //Sortie de boucle si un sommet est dans le domaine d'exclusion
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Résolution du graphe au sens du plus court chemin
        /// </summary>
        /// <param name="AEtoile">booléen indiquant l'utilisation d'A* ou non</param>
        public void ResoutGraphePlusCourtChemin(bool AEtoile)
        {
            SommetsOuverts = new List<Sommet>();
            SommetsFermes = new List<Sommet>();
            SommetInitial = DeterminePointInitial();
            SommetActuel = SommetInitial;
            SommetFinal = DeterminePointFinal();
            PlusCourtChemin = new List<Sommet>();
            ResolutionAEtoile = AEtoile;
            EtatsSuccessifsFermes = new List<List<Sommet>>();
            EtatsSuccessifsOuverts = new List<List<Sommet>>();
            RechercheSolutionAEtoile();
        }
        public void RechercheSolutionAEtoile()
        {
            Sommet s = SommetInitial;
            SommetsOuverts.Add(s);
            EtatsSuccessifsOuverts.Add(SommetsOuverts); //objets de type référence donc seront tous les mêmes à la fin de A*....... breaker le while
            EtatsSuccessifsFermes.Add(SommetsFermes);
            // tant que le noeud n'est pas terminal et que ouverts n'est pas vide
            while (SommetsOuverts.Count != 0 && s.EndState() == false)
            {
                // Le meilleur noeud des ouverts est supposé placé en tête de liste
                // On le place dans les fermés
                SommetsOuverts.Remove(s);
                EtatsSuccessifsOuverts.Add(DeepCopy(SommetsOuverts));
                SommetsFermes.Add(s);
                EtatsSuccessifsFermes.Add(DeepCopy(SommetsFermes));

                // Il faut trouver les noeuds successeurs de s
                MAJSuccesseurs(s);
                // Inutile de retrier car les insertions ont été faites en respectant l'ordre

                // On prend le meilleur, donc celui en position 0, pour continuer à explorer les états
                // A condition qu'il existe bien sûr
                if (SommetsOuverts.Count > 0)
                {
                    s = SommetsOuverts.First();
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
            if (s != null)
            {
                PlusCourtChemin.Add(s);
                CoutPlusCourtChemin += s.CoutTotal;
                while (!s.IsEqual(SommetInitial))
                {
                    s = s.SommetParent;
                    PlusCourtChemin.Insert(0, s);  // On insère en position 1
                    CoutPlusCourtChemin += s.CoutTotal;
                }
            }
        }

        private void MAJSuccesseurs(Sommet s)
        {
            // On fait appel à GetListSucc, méthode abstraite qu'on doit réécrire pour chaque
            // problème. Elle doit retourner la liste complète des noeuds successeurs de s.
            List<Sommet> listsucc = s.GetSuccesseurs();
            foreach (Sommet succ in listsucc)
            {
                // succ est-il une copie d'un nœud déjà vu et placé dans la liste des fermés ?
                Sommet succBis = SommetsFermes.Find(x => x.IsEqual(succ));
                if (succBis == null)
                {
                    // Rien dans les fermés. Est-il dans les ouverts ?
                    succBis = SommetsOuverts.Find(x => x.IsEqual(succ));
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
                            SommetsOuverts.Remove(succBis);
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
            if (SommetsOuverts.Count == 0)
            { SommetsOuverts.Add(NewNode); }
            else
            {
                Sommet s = SommetsOuverts[0];
                bool trouve = false;
                int i = 0;
                do
                    if (NewNode.CoutTotal < s.CoutTotal)
                    {
                        SommetsOuverts.Insert(i, NewNode);
                        trouve = true;
                    }
                    else
                    {
                        i++;
                        if (SommetsOuverts.Count == i)
                        {
                            s = null;
                            SommetsOuverts.Insert(i, NewNode);
                        }
                        else
                        { s = SommetsOuverts[i]; }
                    }
                while ((s != null) && (trouve == false));
            }
        }
        public Arete RetrouveArete(Sommet s1, Sommet s2)
        {
            if(s1.Incidences.Count < s2.Incidences.Count) //On effectue une recherche sur la plus petite liste d'incidences possible
            {
                return s1.Incidences.Find(x => x.IsEqual(new Arete(s1, s2)));
            }
            else
            {
                if(s1.Incidences.Count >= s2.Incidences.Count)
                {
                    return s2.Incidences.Find(x => x.IsEqual(new Arete(s1, s2)));
                }
                else
                {
                    return null;
                }
            }
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
