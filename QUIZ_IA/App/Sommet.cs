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
    class Sommet
    {
        private static int _LASTLETTER = -1;
        public string Label { get; private set; }
        public Point Pt { get; set; }
        public Sommet(int x, int y)
        {
            Pt = new Point(x, y);
            _LASTLETTER++;
            Label = MainForm.alphabet[_LASTLETTER];
        }
        public bool Equals(Sommet pt)
        {
            return ((Pt.X == pt.Pt.X) && (Pt.Y == pt.Pt.Y));
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
