using System;
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
        public Panel drawingPanel;
        public Graphe graphDijkstra;
        public Dijkstra()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);


            drawingPanel = new Panel();
            drawingPanel.Location = new Point(0, 0);
            // Subscribing to a paint eventhandler to drawingPanel:
            drawingPanel.Paint += new PaintEventHandler(DrawingPanel_Paint);
            drawingPanel.BackColor = Color.White;
            drawingPanel.BorderStyle = BorderStyle.FixedSingle;
            drawingPanel.Anchor = AnchorStyles.Bottom;
            drawingPanel.Anchor = AnchorStyles.Left;
            drawingPanel.Anchor = AnchorStyles.Right;
            drawingPanel.Anchor = AnchorStyles.Top;
            drawingPanel.Left = offset;
            drawingPanel.Top = offset;
            drawingPanel.Width = ClientRectangle.Width - 2 * offset;
            drawingPanel.Height = ClientRectangle.Height - 14 * offset;


            Size = new Size(550, 550);
            Controls.Add(drawingPanel);


            graphDijkstra = new Graphe(this);
        }
        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Sommet pt in graphDijkstra.PointsToScan)
            {
                pt.Pt = Point2D(pt.Pt);
                PlotPoint2D(pt, g, graphDijkstra);
            }
            foreach (Arete r in graphDijkstra.Aretes)
            {
                TraceRelation(r, g);
            }
            g.Dispose();
        }
        private Point Point2D(Point ptf)
        {
            Point aPoint = new Point();
            aPoint.X = (ptf.X - xMin) * drawingPanel.Width / (xMax - xMin);
            aPoint.Y = drawingPanel.Height - (ptf.Y - yMin) * drawingPanel.Height / (yMax - yMin);
            return aPoint;
        }
        private void PlotPoint2D(Sommet pt, Graphics g, Graphe gD)
        {
            Point ptf = pt.Pt;
            Font f = new Font("Calibri", 11, FontStyle.Bold);
            Brush b = Brushes.Black;
            if(pt.IsEqual(gD.InitialPoint) || pt.IsEqual(gD.LastPoint))
            {
                b = Brushes.Blue;
            }
            g.DrawString(pt.Label, f, b, ptf);
            if (pt.IsEqual(gD.CurrentPoint))
            {
                SolidBrush aBrush = new SolidBrush(Color.Red);
                int w = 6;
                g.FillRectangle(aBrush, ptf.X - w / 2, ptf.Y - w / 2, w, w);
            }
        }
        private void TraceRelation(Arete r, Graphics g)
        {
            Pen aPen = new Pen(Color.Black, 2);
            // Set line caps and dash style:
            //aPen.StartCap = LineCap.RoundAnchor;
            //aPen.EndCap = LineCap.RoundAnchor;
            aPen.DashStyle = DashStyle.Dash;
            aPen.DashOffset = 500;
            Point ptf1 = r.S1.Pt;
            Point ptf2 = r.S2.Pt;
            g.DrawLine(aPen, ptf1, ptf2);
            aPen.Dispose();
            Font f = new Font("Calibri", 10, FontStyle.Regular);
            Point pt1 = r.S1.Pt;
            Point pt2 = r.S2.Pt;
            Point milieu = new Point((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);
            g.DrawString(r.ToString(), f, Brushes.Black, milieu);
        }
    }
}
