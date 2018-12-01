using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public class Graphe
    {
        //distance minimale entre le point initial et le point final à respecter, genre min 5
        //retirer graphes linéaires

        //propriétés utiles pour l'affichage du graphe
        public static Random rnd = new Random();
        private static int __NBSOMMETS = 11;
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
        public int GetNbEtapes()
        {
            return EtatsSuccessifsOuverts.Count;
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
            //on ne veut pas prendre le SommetInitial comme SommetFinal donc on le supprime des sommets à tester
            List<int> indicesDejaTestes = new List<int>(Sommets.IndexOf(SommetInitial)); 
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
                compteur++;
            }
            //tant que les sommets initial et final ne sont pas assez éloignés et qu'on a pas parcouru tous les sommets du graphe
            while (PointsInitialFinalAssezEloignes(s) == false && compteur < n); 
            return s;
        }
        /// <summary>
        /// Détermine si le sommet s est assez éloigné du SommetInitial, c'est-à-dire qu'ils soient séparés d'au moins 2 sommets
        /// </summary>
        public bool PointsInitialFinalAssezEloignes(Sommet s)
        {
                List<Sommet> plusProchesVoisins = s.GetSuccesseurs();
                foreach (Sommet voisin in plusProchesVoisins)
                {
                    if (voisin.IsEqual(SommetInitial))
                    {
                        return false;
                    }
                    else
                    {
                        List<Sommet> voisinsEloignes = voisin.GetSuccesseurs();
                        foreach (Sommet voisinEloigne in voisinsEloignes)
                        {
                            if (voisinEloigne.IsEqual(SommetInitial))
                            {
                                return false;
                            }
                        }
                    }
                }
            return true;
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
        public void GenereSommets(int xMin, int xMax, int yMin, int yMax)
        {
            //Sommets.Add(new Sommet((float)3.4, (float)8.4,true));
            //Sommets.Add(new Sommet((float)6.7, (float)7.7, true));
            //Sommets.Add(new Sommet((float)8.5, (float)2.8, true));
            //Sommets.Add(new Sommet((float)5.4, (float)8.4, true));
            //Sommets.Add(new Sommet((float)4.5, (float)1.3, true));
            //Sommets.Add(new Sommet((float)3.8, (float)7.5, true));
            //Sommets.Add(new Sommet((float)1.5, (float)3.9, true));
            //Sommets.Add(new Sommet((float)4.5, (float)2.4, true));
            //Sommets.Add(new Sommet((float)7.9, (float)5.9, true));
            int nbPoints = rnd.Next(8, __NBSOMMETS + 1);
            for (int i = 0; i < nbPoints; i++)
            {
                float partieDecimaleX = (float)rnd.NextDouble();
                float partieDecimaleY = (float)rnd.NextDouble();
                float x = rnd.Next(xMin + 1, xMax - 1) + partieDecimaleX;
                float y = rnd.Next(yMin + 1, yMax - 1) + partieDecimaleY;
                Sommet s = new Sommet(x, y, true);
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
            int numEtape = 1;
            Sommet s = SommetInitial;
            SommetsOuverts.Add(s);
            EtatsSuccessifsOuverts.Add(DeepCopy(SommetsOuverts)); 
            EtatsSuccessifsFermes.Add(DeepCopy(SommetsFermes));
            // tant que le noeud n'est pas terminal et que ouverts n'est pas vide
            while (SommetsOuverts.Count != 0 && s.EndState() == false)
            {
                // Le meilleur noeud des ouverts est supposé placé en tête de liste
                // On le place dans les fermés
                SommetsOuverts.Remove(s);
                SommetsFermes.Add(s);

                // Il faut trouver les noeuds successeurs de s
                MAJSuccesseurs(s,numEtape);
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
                EtatsSuccessifsOuverts.Add(DeepCopy(SommetsOuverts));
                EtatsSuccessifsFermes.Add(DeepCopy(SommetsFermes));
                numEtape++;
            }
            SommetsOuverts.Remove(SommetFinal);
            SommetsFermes.Add(SommetFinal);
            EtatsSuccessifsOuverts.Add(SommetsOuverts);
            EtatsSuccessifsFermes.Add(SommetsFermes);
            // A* terminé
            // On retourne le chemin qui va du noeud initial au noeud final sous forme de liste
            // Le chemin est retrouvé en partant du noeud final et en accédant aux parents de manière
            // itérative jusqu'à ce qu'on tombe sur le noeud initial
            if (s != null)
            {
                PlusCourtChemin.Add(s);
                CoutPlusCourtChemin = s.CoutTotal;
                while (!s.IsEqual(SommetInitial))
                {
                    s = s.SommetParent;
                    PlusCourtChemin.Insert(0, s);  // On insère en position 1
                }
            }
        }

        private void MAJSuccesseurs(Sommet s,int numEtape)
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
                            InsertNewNodeInOpenList(succBis,numEtape);
                        }
                        // else on ne fait rien, car le nouveau chemin est moins bon
                    }
                    else
                    {
                        // succ est nouveau, MAJ et insertion dans les ouverts
                        succ.CoutCumule = s.CoutCumule + RetrouveArete(s, succ).Cout;
                        succ.SommetParent = s;
                        succ.CalculCoutTotal(); // somme de GCost et HCost
                        InsertNewNodeInOpenList(succ,numEtape);
                    }
                }
                // else il est dans les fermés donc on ne fait rien,
                // car on a déjà trouvé le plus court chemin pour aller en succ
            }
        }

        public void InsertNewNodeInOpenList(Sommet NewNode, int numEtape)
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
                    if ((NewNode.CoutTotal < s.CoutTotal) && (!EtatsSuccessifsOuverts[numEtape - 1].Contains(s)))//si s n'était pas présent dans les ouverts de l'étape précédente
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

        //Si on veut afficher l'arbre de recherche, il suffit de passer un treeview en paramètres
        // Celui-ci est mis à jour avec les noeuds de la liste des fermés, on ne tient pas compte des ouverts
        public void GetSearchTree(TreeView TV)
        {
            if (SommetsFermes == null) return;
            if (SommetsFermes.Count == 0) return;

            // On suppose le TreeView préexistant
            TV.Nodes.Clear();

            TreeNode TN = new TreeNode(SommetsFermes[0].ToString());
            TV.Nodes.Add(TN);

            AjouteBranche(SommetsFermes[0], TN);
        }

        // AjouteBranche est exclusivement appelée par GetSearchTree; les noeuds sont ajoutés de manière récursive
        private void AjouteBranche(Sommet GN, TreeNode TN)
        {
            foreach (Sommet GNfils in GN.Enfants)
            {
                TreeNode TNfils = new TreeNode(GNfils.ToString());
                TN.Nodes.Add(TNfils);
                if (GNfils.Enfants.Count > 0) AjouteBranche(GNfils, TNfils);
            }
        }
    }
}
