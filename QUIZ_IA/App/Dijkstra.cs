using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Dijkstra : Form
    {
        // Unit defined in world coordinate system:
        public float xMin = 0f;
        public float xMax = 10f;
        public float yMin = 0f;
        public float yMax = 10f;
        // Define the offset in pixel:
        private int offset = 20;
        public Panel drawingPanel;
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
            drawingPanel.Height = ClientRectangle.Height - 12 * offset;
        }
        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Graph graphDijkstra = new Graph(this);
            foreach (LabelledPoint pt in graphDijkstra.PointsToScan)
            {
                pt.Ptf = Point2D(pt.Ptf);
                PlotPoint2D(pt, g, graphDijkstra);
            }
            foreach (LabelledRelation r in graphDijkstra.Relations)
            {
                TraceRelation(r, g);
            }
            g.Dispose();
        }
        private PointF Point2D(PointF ptf)
        {
            PointF aPoint = new PointF();
            aPoint.X = (ptf.X - xMin) * drawingPanel.Width / (xMax - xMin);
            aPoint.Y = drawingPanel.Height - (ptf.Y - yMin) * drawingPanel.Height / (yMax - yMin);
            return aPoint;
        }
        private void PlotPoint2D(LabelledPoint pt, Graphics g, Graph gD)
        {
            PointF ptf = pt.Ptf;
            Font f = new Font("Arial", 11, FontStyle.Regular);
            g.DrawString(pt.Label, f, Brushes.Black, ptf);
            if (pt.Equals(gD.CurrentPoint))
            {
                SolidBrush aBrush = new SolidBrush(Color.Red);
                int w = 6;
                g.FillRectangle(aBrush, ptf.X - w / 2, ptf.Y - w / 2, w, w);
            }
        }
        private void TraceRelation(LabelledRelation r, Graphics g)
        {
            Pen aPen = new Pen(Color.Black, 2);
            // Set line caps and dash style:
            //aPen.StartCap = LineCap.RoundAnchor;
            //aPen.EndCap = LineCap.RoundAnchor;
            aPen.DashStyle = DashStyle.Dash;
            aPen.DashOffset = 500;
            PointF ptf1 = r.Pt1.Ptf;
            PointF ptf2 = r.Pt2.Ptf;
            g.DrawLine(aPen, ptf1, ptf2);
            aPen.Dispose();
            Font f = new Font("Arial", 10, FontStyle.Italic);
            PointF pt1 = r.Pt1.Ptf;
            PointF pt2 = r.Pt2.Ptf;
            PointF milieu = new PointF((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);
            g.DrawString(r.ToString(), f, Brushes.Black, milieu);
        }
    }
}
