using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Graphe
    {
        public static Random rnd = new Random();
        public List<Sommet> PointsToScan { get; private set; }
        public List<Sommet> PointsScanned { get; private set; }
        public Sommet CurrentPoint { get; private set; }
        public List<Arete> Aretes { get; private set; }
        public Sommet InitialPoint { get; private set; }
        public Sommet LastPoint { get; private set; }
        
        public Graphe(Dijkstra d)
        {
            PointsToScan = new List<Sommet>();
            GeneratePointList(d);
            PointsScanned = new List<Sommet>();
            Aretes = new List<Arete>();
            GenerateRelationList();
            CurrentPoint = PointsToScan.First();
            InitialPoint = PointsToScan.First();
            LastPoint = PointsToScan.Last();
        }
        public void GeneratePointList(Dijkstra d)
        {
            int nbPoints = 7;
            PointsToScan.Add(new Sommet(1, 1));
            PointsToScan.Add(new Sommet(2, 5));
            PointsToScan.Add(new Sommet(3, 3));
            PointsToScan.Add(new Sommet(1, 4));
            PointsToScan.Add(new Sommet(4, 6));
            PointsToScan.Add(new Sommet(5, 7));
            PointsToScan.Add(new Sommet(7, 7));
            //int nbPoints = rnd.Next(5, LabelledPoint.alphabet.Length + 1);
            //for (int i = 0; i < nbPoints; i++)
            //{
            //    float x = rnd.Next((int)d.xMin + 1, (int)d.xMax);
            //    float y = rnd.Next((int)d.yMin + 1 , (int)d.yMax);
            //    PointsToScan.Add(new LabelledPoint(x, y));
            //}
        }
        public void GenerateRelationList()
        {
            int nbPoints = PointsToScan.Count;
            int nbRelMax = nbPoints * nbPoints;
            int nbRelations = 7; // rnd.Next(2 * nbPoints);
            Aretes.Add(new Arete(PointsToScan[0], PointsToScan[1]));
            Aretes.Add(new Arete(PointsToScan[0], PointsToScan[2]));
            Aretes.Add(new Arete(PointsToScan[0], PointsToScan[3]));
            Aretes.Add(new Arete(PointsToScan[1], PointsToScan[4]));
            Aretes.Add(new Arete(PointsToScan[2], PointsToScan[4]));
            Aretes.Add(new Arete(PointsToScan[4], PointsToScan[5]));
            Aretes.Add(new Arete(PointsToScan[5], PointsToScan[6]));
            //for(int i = 0; i < nbRelations; i++)
            //{
            //    int index1 = rnd.Next(nbPoints);
            //    int index2 = rnd.Next(nbPoints);
            //    Relations.Add(new Relation(PointsToScan[index1], PointsToScan[index2]));
            //}
        }
    }
}
