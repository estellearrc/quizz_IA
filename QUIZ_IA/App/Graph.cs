using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Graph
    {
        public static Random rnd = new Random();
        public List<LabelledPoint> PointsToScan { get; private set; }
        public List<LabelledPoint> PointsScanned { get; private set; }
        public LabelledPoint CurrentPoint { get; private set; }
        public List<LabelledRelation> Relations { get; private set; }
        public Graph(Dijkstra d)
        {
            PointsToScan = new List<LabelledPoint>();
            GeneratePointList(d);
            PointsScanned = new List<LabelledPoint>();
            Relations = new List<LabelledRelation>();
            GenerateRelationList();
            CurrentPoint = PointsToScan.First();
        }
        public void GeneratePointList(Dijkstra d)
        {
            int nbPoints = 7;
            PointsToScan.Add(new LabelledPoint(1, 1));
            PointsToScan.Add(new LabelledPoint(2, 5));
            PointsToScan.Add(new LabelledPoint(3, 3));
            PointsToScan.Add(new LabelledPoint(1, 4));
            PointsToScan.Add(new LabelledPoint(4, 6));
            PointsToScan.Add(new LabelledPoint(5, 7));
            PointsToScan.Add(new LabelledPoint(7, 7));
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
            Relations.Add(new LabelledRelation(PointsToScan[0], PointsToScan[1]));
            Relations.Add(new LabelledRelation(PointsToScan[0], PointsToScan[2]));
            Relations.Add(new LabelledRelation(PointsToScan[0], PointsToScan[3]));
            Relations.Add(new LabelledRelation(PointsToScan[1], PointsToScan[4]));
            Relations.Add(new LabelledRelation(PointsToScan[2], PointsToScan[4]));
            Relations.Add(new LabelledRelation(PointsToScan[4], PointsToScan[5]));
            Relations.Add(new LabelledRelation(PointsToScan[5], PointsToScan[6]));
            //for(int i = 0; i < nbRelations; i++)
            //{
            //    int index1 = rnd.Next(nbPoints);
            //    int index2 = rnd.Next(nbPoints);
            //    Relations.Add(new Relation(PointsToScan[index1], PointsToScan[index2]));
            //}
        }
    }
}
