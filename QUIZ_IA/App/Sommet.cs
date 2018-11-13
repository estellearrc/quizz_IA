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
    public class Sommet : GenericNode
    {
        private static int _LASTNUMBER = -1;
        public int Numero { get; private set; } //indice du sommet (unique)
        public string Label { get; private set; } //label du sommet
        public Point Pt { get; set; } //coordonnées du sommet
        public Sommet(int x, int y)
        {
            Pt = new Point(x, y);
            _LASTNUMBER++;
            Numero = _LASTNUMBER;
            if(Numero < MainForm.alphabet.Count())
            {
                Label = MainForm.alphabet[Numero];
            }
            else
            {
                Label = "P" + Numero;
            }
        }
        public override string ToString()
        {
            return Label;
        }

        public override bool IsEqual(GenericNode N)
        {
            Sommet pt = (Sommet)N;
            return Numero == pt.Numero;
        }

        public override double GetArcCost(GenericNode N)
        {
            throw new NotImplementedException();
        }

        public override bool EndState()
        {
            return IsEqual(MainForm.d.graphDijkstra.LastPoint);
        }

        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();

            foreach(Arete a in MainForm.d.graphDijkstra.Aretes)
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

        public override double CalculeHCost()
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
