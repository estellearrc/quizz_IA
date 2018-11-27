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
        private int xMin = 0;
        private int xMax = 10;
        private int yMin = 0;
        private int yMax = 10;
        // Define the offset in pixel:
        private int offset = 20;
        public static Panel zoneDessin; //design context = zone de dessin
        public static Graphe grapheDijkstra; //graphe soumis à l'agorithme Dijkstra ou A*
        public Graphics dessin; //dessin du graphe graphDijkstra dans le zoneDessin

        CheckBox[] lesCheckBoxes = null;
        Button btn = new Button();
        
        public int compteur = 0;
        public static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
        public Dijkstra(bool ResolutionAEtoile)
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
            zoneDessin.Height = ClientRectangle.Height - 16 * offset;
            Controls.Add(zoneDessin);

            //Création du graphe à afficher dans le drawing panel
            grapheDijkstra = new Graphe(xMin,xMax,yMin,yMax);
            grapheDijkstra.ResoutGraphePlusCourtChemin(ResolutionAEtoile);
        }
        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            //Obtient le graphique WinForm utilisé pour dessiner dedans
            dessin = e.Graphics;
            foreach (Sommet pt in grapheDijkstra.Sommets)
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
            if(s.IsEqual(grapheDijkstra.SommetInitial) || s.IsEqual(grapheDijkstra.SommetFinal))
            {
                b = Brushes.Blue;
            }
            dessin.DrawString(s.Label, f, b, ptf);
            if (s.IsEqual(grapheDijkstra.SommetActuel))
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

        private void GenerePropositions(int numEtape, out List<Sommet> propositionsOuverts, out List<Sommet> propositionsFermes)
        {
            List<Sommet> reponseCorrecteFermes = grapheDijkstra.EtatsSuccessifsFermes[numEtape];
            List<Sommet> reponseCorrecteOuverts = grapheDijkstra.EtatsSuccessifsOuverts[numEtape];

            List<Sommet> copieReponseCorrecteFermes = grapheDijkstra.DeepCopy(reponseCorrecteFermes);
            List<Sommet> copieReponseCorrecteOuverts = grapheDijkstra.DeepCopy(reponseCorrecteOuverts);
            int nbPropositions = reponseCorrecteFermes.Count;
            propositionsOuverts = new List<Sommet>(nbPropositions);
            propositionsFermes = new List<Sommet>(nbPropositions);
            for (int i = 0; i < nbPropositions; i++)
            {
            }
        }
        private void AfficheChoixPossible()
        {
            //prendre en compte le compteur
            List<Sommet> propositionsOuverts = new List<Sommet>();
            List<Sommet> propositionsFermes = new List<Sommet>();
            GenerePropositions(compteur, out propositionsOuverts, out propositionsFermes);

            btn.Text = "Valider";
            btn.Location= new Point(300, 300);
            CheckBox[] checkBoxes = new CheckBox[6];
            lesCheckBoxes = new CheckBox[6];

            string choix;

            for (int i = 0; i < 6; i++)
            {
                
                checkBoxes[i] = new CheckBox();
                if (i < 3) { checkBoxes[i].Location = new Point(69, 145 + i * 20);
                    choix=propositionsOuverts[i];
                }
                else { checkBoxes[i].Location = new Point(300, 145 + (i-3) * 20);
                    choix = propositionsFermes[i];
                }
                                
                checkBoxes[i].Text = alphabet[i] + ".  "+ choix ;
                checkBoxes[i].AutoSize = true;
                Controls.Add(checkBoxes[i]);
                lesCheckBoxes[i] = checkBoxes[i];
            }


        }

        private void Affiche()
        {
            if (question != null) //par précaution on teste si la question est nulle mais normalement elle ne sera jamais nulle
            {

                btnValider.Text = "Valider";
                txtQuestion.Text = question.Intitule;
                if (question.Type == Question.TypeQues.QCM)// pour les qcm
                {
                    if (question.Intitule == "Dijkstra")
                    {
                        d = new Dijkstra(false);
                        d.Show();
                    }
                    else
                    {
                        if (question.Intitule == "A*")
                        {
                            d = new Dijkstra(true);
                            d.Show();
                        }
                        else
                        {

                            CheckBox[] checkBoxes = new CheckBox[question.LesReponses.Count];
                            lesCheckBoxes = new CheckBox[question.LesReponses.Count];

                            for (int i = 0; i < question.LesReponses.Count; i++)
                            {
                                checkBoxes[i] = new CheckBox();
                                checkBoxes[i].Location = new Point(69, 145 + i * 20);
                                checkBoxes[i].Text = alphabet[i] + ".  " + question.LesReponses[i].Intitule;
                                checkBoxes[i].AutoSize = true;
                                Controls.Add(checkBoxes[i]);
                                lesCheckBoxes[i] = checkBoxes[i];
                            }
                        }
                    }
                }

        private void GenerePropositions(int nbPropositions, int numEtape, out List<Sommet>[] propositionsOuverts, out List<Sommet>[] propositionsFermes, out int indiceOuvertCorrect, out int indiceFermeCorrect)

        {
            List<Sommet> reponseCorrecteFermes = grapheDijkstra.EtatsSuccessifsFermes[numEtape];
            List<Sommet> reponseCorrecteOuverts = grapheDijkstra.EtatsSuccessifsOuverts[numEtape];

            List<Sommet> copieReponseCorrecteFermes = grapheDijkstra.DeepCopy(reponseCorrecteFermes);
            List<Sommet> copieReponseCorrecteOuverts = grapheDijkstra.DeepCopy(reponseCorrecteOuverts);
            propositionsOuverts = new List<Sommet>[nbPropositions];
            propositionsFermes = new List<Sommet>[nbPropositions];
            indiceOuvertCorrect = Graphe.rnd.Next(nbPropositions);
            indiceFermeCorrect = Graphe.rnd.Next(nbPropositions);
            for (int i = 1; i < nbPropositions; i++) //on va faire des rotations sur les sommets fermés puis, après chaque rotation effectuée, on ajoute le premier ouvert à la fin de la liste des fermés
            {
                Sommet O = copieReponseCorrecteOuverts.First();
                //on met le sommet O à la fin des ouverts
                copieReponseCorrecteOuverts.Remove(O);
                copieReponseCorrecteOuverts.Add(O);
                //on l'ajoute à la liste des propositions
                propositionsOuverts[i] = grapheDijkstra.DeepCopy(copieReponseCorrecteOuverts);

                Sommet F = copieReponseCorrecteFermes.Last();
                //on change le dernier sommet des fermés
                copieReponseCorrecteFermes.Remove(F);
                //on récupère le nouveau premier sommet ouvert
                O = copieReponseCorrecteOuverts.First();
                //On l'ajoute à la fin de la liste des fermés
                copieReponseCorrecteFermes.Add(O);
                //on l'ajoute à la liste des propositions
                propositionsFermes[i] = grapheDijkstra.DeepCopy(copieReponseCorrecteFermes);
            }
        }

    }
}
