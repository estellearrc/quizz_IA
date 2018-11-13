using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Arete
    {
        public Sommet S1 { get; private set; }
        public Sommet S2 { get; private set; }
        public int Distance { get; private set; }
        public Arete(Sommet s1, Sommet s2)
        {
            S1 = s1;
            S2 = s2;
            Distance = CalculeDistance();
        }
        public int CalculeDistance()
        {
            float x1 = S1.Pt.X;
            float x2 = S2.Pt.X;
            float y1 = S1.Pt.Y;
            float y2 = S2.Pt.Y;
            return (int)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
        public override string ToString()
        {
            return Distance.ToString();
        }
    }
}
