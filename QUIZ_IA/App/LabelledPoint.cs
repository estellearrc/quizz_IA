using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class LabelledPoint
    {
        public static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
        private static int _LASTLETTER = -1;
        public string Label { get; private set; }
        public PointF Ptf { get; set; }
        public LabelledPoint(float x, float y)
        {
            Ptf = new PointF(x, y);
            _LASTLETTER++;
            Label = alphabet[_LASTLETTER];
        }
        public bool Equals(LabelledPoint pt)
        {
            return ((Ptf.X == pt.Ptf.X) && (Ptf.Y == pt.Ptf.Y));
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
