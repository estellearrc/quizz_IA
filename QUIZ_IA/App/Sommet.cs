﻿using System;
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
            throw new NotImplementedException();
        }

        public override List<GenericNode> GetListSucc()
        {
            throw new NotImplementedException();
        }

        public override double CalculeHCost()
        {
            throw new NotImplementedException();
        }
    }
}