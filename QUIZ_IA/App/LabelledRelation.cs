using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class LabelledRelation
    {
        public LabelledPoint Pt1 { get; private set; }
        public LabelledPoint Pt2 { get; private set; }
        public int Distance { get; private set; }
        public LabelledRelation(LabelledPoint pt1, LabelledPoint pt2)
        {
            Pt1 = pt1;
            Pt2 = pt2;
            Distance = CalculeDistance();
        }
        public int CalculeDistance()
        {
            float x1 = Pt1.Ptf.X;
            float x2 = Pt2.Ptf.X;
            float y1 = Pt1.Ptf.Y;
            float y2 = Pt2.Ptf.Y;
            return (int)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
        public override string ToString()
        {
            return Distance.ToString();
        }
    }
}
