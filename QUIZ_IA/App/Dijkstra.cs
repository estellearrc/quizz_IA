﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Dijkstra : Form
    {
        // Unit defined in world coordinate system:
        public int xMin = 0;
        public int xMax = 10;
        public int yMin = 0;
        public int yMax = 10;
        // Define the offset in pixel:
        private int offset = 20;
        public static Panel zoneDessin; //design context = zone de dessin
        public static Graphe grapheDijkstra; //graphe soumis à l'agorithme Dijkstra ou A*
        public static Graphics dessin; //dessin du graphe graphDijkstra dans le zoneDessin
        public Dijkstra()
        {
            //Initialisation des composants et de la fenêtre client
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            Size = new Size(700, 700);

            //Ajout d'une zone de dessin drawingPanel
            zoneDessin = new Panel();
            zoneDessin.Location = new Point(0, 0);
            // Subscribing to a paint eventhandler to drawingPanel:
            zoneDessin.Paint += new PaintEventHandler(DrawingPanel_Paint);
            zoneDessin.BackColor = Color.White;
            zoneDessin.BorderStyle = BorderStyle.FixedSingle;
            zoneDessin.Anchor = AnchorStyles.Bottom;
            zoneDessin.Anchor = AnchorStyles.Left;
            zoneDessin.Anchor = AnchorStyles.Right;
            zoneDessin.Anchor = AnchorStyles.Top;
            zoneDessin.Left = offset;
            zoneDessin.Top = offset;
            zoneDessin.Width = ClientRectangle.Width - 2 * offset;
            zoneDessin.Height = ClientRectangle.Height - 14 * offset;
            Controls.Add(zoneDessin);

            //Création du graphe à afficher dans le drawing panel
            grapheDijkstra = new Graphe(this);
        }
        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            //Obtient le graphique WinForm utilisé pour dessiner dedans
            dessin = e.Graphics;
            foreach (Sommet pt in grapheDijkstra.PointsOuverts)
            {
                pt.Pt = Point2D(pt.Pt);
                PlotPoint2D(pt);
            }
            foreach (Arete r in grapheDijkstra.Aretes)
            {
                TraceArete(r);
            }
            dessin.Dispose();
        }
        private PointF Point2D(PointF ptf)
        {
            PointF aPoint = new PointF();
            aPoint.X = (ptf.X - xMin) * zoneDessin.Width / (xMax - xMin);
            aPoint.Y = zoneDessin.Height - (ptf.Y - yMin) * zoneDessin.Height / (yMax - yMin);
            return aPoint;
        }
        private void PlotPoint2D(Sommet s)
        {
            PointF ptf = s.Pt;
            Font f = new Font("Calibri", 11, FontStyle.Bold);
            Brush b = Brushes.Black;
            if(s.IsEqual(grapheDijkstra.PointInitial) || s.IsEqual(grapheDijkstra.PointFinal))
            {
                b = Brushes.Blue;
            }
            dessin.DrawString(s.Label, f, b, ptf);
            if (s.IsEqual(grapheDijkstra.PointActuel))
            {
                SolidBrush aBrush = new SolidBrush(Color.Red);
                int w = 7;
                dessin.FillRectangle(aBrush, ptf.X - w / 2, ptf.Y - w / 2, w, w);
            }
        }
        private void TraceArete(Arete r)
        {
            Pen aPen = new Pen(Color.Black, 2);
            // Set line caps and dash style:
            aPen.StartCap = LineCap.RoundAnchor;
            aPen.EndCap = LineCap.RoundAnchor;
            aPen.DashStyle = DashStyle.Dash;
            aPen.DashOffset = 500;
            PointF ptf1 = r.S1.Pt;
            PointF ptf2 = r.S2.Pt;
            dessin.DrawLine(aPen, ptf1, ptf2);
            aPen.Dispose();
            Font f = new Font("Calibri", 11, FontStyle.Regular);
            PointF milieu = r.S1.CalculeMilieu(r.S2).Pt;
            dessin.DrawString(r.ToString(), f, Brushes.Black, milieu);
        }
    }
}
