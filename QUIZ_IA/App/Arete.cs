using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Arete
    {
        public Sommet Pt1 { get; private set; }
        public Sommet Pt2 { get; private set; }
        public int Distance { get; private set; }
        public Arete(Sommet pt1, Sommet pt2)
        {
            Pt1 = pt1;
            Pt2 = pt2;
            Distance = CalculeDistance();
        }
        public int CalculeDistance()
        {
            float x1 = Pt1.Pt.X;
            float x2 = Pt2.Pt.X;
            float y1 = Pt1.Pt.Y;
            float y2 = Pt2.Pt.Y;
            return (int)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
        public override string ToString()
        {
            return Distance.ToString();
        }
    }
}
