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
    public class Sommet
    {
        private static int _LASTNUMBER = -1; //numéro du dernier sommet créé

        public int Numero { get; private set; }                 //indice du sommet (unique)
        public string Label { get; private set; }               //label du sommet
        public Point Pt { get; set; }                   //coordonnées du sommet
        public List<Arete> Incidences { get; private set; }
        public int CoutCumule { get; set; }               //coût du chemin du noeud initial jusqu'à ce noeud
        public int CoutHeuristique { get; private set; }               //estimation heuristique du coût pour atteindre le noeud final
        public int CoutTotal { get; private set; }           //coût total (g+h)
        public List<Sommet> Enfants { get; private set; }     // noeuds enfants
        private Sommet _sommetParent;
        public Sommet SommetParent                                // noeud parent
        {
            get
            {
                return _sommetParent;
            }
            set
            {
                _sommetParent = value;
                if(_sommetParent != null)
                    value.Enfants.Add(this);
            }
        }
        public Sommet(int x, int y)
        {
            Pt = new Point(x, y);
            _LASTNUMBER++;
            Numero = _LASTNUMBER;
            Label = Label;
            Incidences = new List<Arete>();
            SommetParent = null;
            Enfants = new List<Sommet>();
        }
        public string AttribueLabel()
        {
            string label;
            if (Numero < MainForm.alphabet.Count())
            {
                label = MainForm.alphabet[Numero];
            }
            else
            {
                label = "P" + Numero;
            }
            return label;
        }
        public override string ToString()
        {
            return Label;
        }

        public void SupprimeLienParent()
        {
            if (SommetParent != null)
            {
                SommetParent.Enfants.Remove(this);
                SommetParent = null;
            }
        }

        public void CalculCoutTotal()
        {
            CoutHeuristique = CalculeHCost();
            CoutTotal = CoutCumule + CoutHeuristique;
        }

        public void RecalculeCoutTotal()
        {
            CoutTotal = CoutCumule + CoutHeuristique;
        }

        public bool IsEqual(Sommet s)
        {
            return Numero == s.Numero;
        }
        public bool EndState()
        {
            return IsEqual(Dijkstra.grapheDijkstra.PointFinal);
        }

        public List<Sommet> GetSuccesseurs()
        {
            List<Sommet> lsucc = new List<Sommet>();

            foreach(Arete a in Dijkstra.grapheDijkstra.Aretes)
            {
                if(IsEqual(a.S1))
                {
                    lsucc.Add(a.S1);
                }
                else
                {
                    if (IsEqual(a.S2))
                    {
                        lsucc.Add(a.S2);
                    }
                }
            }
            return lsucc;
        }

        public int CalculeHCost()
        {
            return 0;
        }
        public int CalculeDistance(Sommet s)
        {
            float x1 = Pt.X;
            float x2 = s.Pt.X;
            float y1 = Pt.Y;
            float y2 = s.Pt.Y;
            return (int)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
    }
}
