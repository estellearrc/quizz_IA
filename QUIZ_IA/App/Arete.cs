﻿using System;
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
        public int Cout { get; private set; }
        public Arete(Sommet s1, Sommet s2)
        {
            S1 = s1;
            S2 = s2;
            Cout = CalculeDistance();
        }
        public bool IsEqual(Arete a)
        {
            return S1.IsEqual(a.S1) && S2.IsEqual(a.S2);
        }
        public override string ToString()
        {
            return Cout.ToString();
        }
    }
}
