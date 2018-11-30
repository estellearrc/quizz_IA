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
        private int xMin = 0;
        private int xMax = 10;
        private int yMin = 0;
        private int yMax = 10;
        // Define the offset in pixel:
        private int offset = 20;
        public static Panel zoneDessin; //design context = zone de dessin
        public static Graphe grapheDijkstra; //graphe soumis à l'agorithme Dijkstra ou A*
        public Graphics dessin; //dessin du graphe graphDijkstra dans le zoneDessin

        private int indiceOuvertCorrect;
        private int indiceFermeCorrect;
        public bool avoirJuste;

        RadioButton[] lesRadiobuttons = null;

        Button btnValider = new Button();
        Label lblConsigne = null;
        Label lblOuvert = null;
        Label lblFerme = null;
        Label lblEtape = null;
        Label lblCorrection = null;

        public static Panel zoneRadButFerme;
        public static Panel zoneRadButOuvert;

        private int nbPropositions;
        private int compteur = 0;
        public int score = 0;
        public Dijkstra(bool ResolutionAEtoile)
        {
            //Initialisation des composants et de la fenêtre client
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            Size = new Size(700, 700);

            zoneRadButFerme = new Panel();
            zoneRadButFerme.Location = new Point(120,480);
            zoneRadButFerme.AutoSize = true;
            zoneRadButOuvert = new Panel();
            zoneRadButOuvert.Location = new Point(485, 480);
            zoneRadButOuvert.AutoSize = true;

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

            Controls.Add(zoneRadButOuvert);
            Controls.Add(zoneRadButFerme);

            btnValider.Text = "Valider";
            btnValider.Size = new Size(137, 31);
            btnValider.Location = new Point(280, 600);
            btnValider.Click += new EventHandler(btnValider_Click);
            Controls.Add(btnValider);
            

            //Création du graphe à afficher dans le drawing panel
            grapheDijkstra = new Graphe(xMin,xMax,yMin,yMax);
            grapheDijkstra.ResoutGraphePlusCourtChemin(ResolutionAEtoile);
            AfficheChoixPossible();
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
            Pen aPen = new Pen(Color.Black, 1);
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



        private void AfficheChoixPossible()
        {
            //prendre en compte le compteur

            nbPropositions = grapheDijkstra.EtatsSuccessifsFermes[compteur].Count;
            List<Sommet>[] propositionsOuverts = new List<Sommet>[nbPropositions];
            List<Sommet>[] propositionsFermes = new List<Sommet>[nbPropositions];
            
            GenerePropositions(compteur, out propositionsOuverts, out propositionsFermes);

            btnValider.Text = "Valider";
          
            RadioButton[] radButFerme = new RadioButton[nbPropositions];
            RadioButton[] radButOuvert = new RadioButton[nbPropositions];


            lesRadiobuttons = new RadioButton[2*nbPropositions];

            lblConsigne = new Label();
            lblConsigne.Location= new Point(80, 390);
            lblConsigne.Font = new Font("Calibri", 13, FontStyle.Bold, GraphicsUnit.Point, 0);
            if (grapheDijkstra.ResolutionAEtoile)
            {
                lblConsigne.Text = "Appliquez A* pour trouver le plus court chemin entre " + grapheDijkstra.SommetInitial + " et " + grapheDijkstra.SommetFinal;
            }
            else
            {

                lblConsigne.Text = "Appliquez Dijkstra pour trouver le plus court chemin entre " + grapheDijkstra.SommetInitial + " et " + grapheDijkstra.SommetFinal;
            }
            Controls.Add(lblConsigne);
            lblConsigne.AutoSize = true;

            lblFerme = new Label();
            lblFerme.Location = new Point(100, 455);
            lblFerme.Font = new Font("Calibri", 11, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFerme.Text = "Ensemble des fermés";
            Controls.Add(lblFerme);
            lblFerme.AutoSize = true;

            lblOuvert = new Label();
            lblOuvert.Location = new Point(450, 455);
            lblOuvert.Font = new Font("Calibri", 11, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOuvert.Text = "Ensemble des ouverts";
            Controls.Add(lblOuvert);
            lblOuvert.AutoSize = true;

            lblEtape = new Label();
            lblEtape.Location = new Point(310, 420);
            lblEtape.Font = new Font("Calibri", 15, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEtape.Text = "Etape "+(compteur+1);
            Controls.Add(lblEtape);
            lblEtape.AutoSize = true;

            string choix;

            for (int i = 0; i < nbPropositions; i++)
            {
                radButFerme[i] = new RadioButton();
                radButFerme[i].Location = new Point(5, 5 + i* 20);
                choix = ListeString(propositionsFermes[i ]);            
                radButFerme[i].Text = MainForm.alphabet[i] + ".  "+ choix ;
                radButFerme[i].AutoSize = true;
                zoneRadButFerme.Controls.Add(radButFerme[i]);
                lesRadiobuttons[i] = radButFerme[i];

            }
            for (int i = nbPropositions; i < 2*nbPropositions; i++)
            {
                radButOuvert[i-nbPropositions] = new RadioButton();
                radButOuvert[i-nbPropositions].Location = new Point(5, 5 + (i-nbPropositions) * 20);
                choix = ListeString(propositionsOuverts[i-nbPropositions]);
                radButOuvert[i-nbPropositions].Text = MainForm.alphabet[i] + ".  " + choix;
                radButOuvert[i-nbPropositions].AutoSize = true;
                zoneRadButOuvert.Controls.Add(radButOuvert[i-nbPropositions]);
                lesRadiobuttons[i] = radButOuvert[i-nbPropositions];

            }


        }



        private string ListeString(List<Sommet> liste)
        {
            string choix;
            choix = "{ ";
            int n;
            if(liste == null)
            {
                n = 0;
            }
            else
            {
                n = liste.Count;
            }
            for (int i = 0; i < n; i++)
            {
                choix += liste[i] + ",";
            }
            choix=choix.Substring(0, choix.Length-1);
            choix += " }";
            return choix;
        }

        private bool Corrige()
        {
            bool juste = true;          
                for (int i = 0; i < 2*nbPropositions; i++)
                {
                if (i < nbPropositions)
                {
                    if ((lesRadiobuttons[i].Checked && (i != indiceFermeCorrect )) || (!lesRadiobuttons[i].Checked && (i == indiceFermeCorrect)))
                    {
                        juste = false;
                    }
                }
                else
                {
                    if ((lesRadiobuttons[i].Checked && ((i - nbPropositions) != indiceOuvertCorrect)) || (!lesRadiobuttons[i].Checked &&  ((i - nbPropositions) == indiceOuvertCorrect)))
                    {
                        juste = false;
                    }
                }
                }
            
            return juste;

        }

        private void AfficheCorrection( bool avoirJuste)
        {

            string correction = "";
            lblCorrection = new Label();
          
            lblCorrection.Font = new Font("Calibri", 11, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCorrection.AutoSize = true;
            Color couleurtxt = Color.FromKnownColor(KnownColor.Green);
            if (avoirJuste)
            {
                lblCorrection.Location = new Point(290, 636);
                correction = "C'est ça ! BRAVO !";
                lblCorrection.ForeColor = couleurtxt;
            }
            else
            {
                lblCorrection.Location = new Point(260, 636);
                couleurtxt = Color.FromKnownColor(KnownColor.Red);
                lblCorrection.ForeColor = couleurtxt;
                
                
                    correction = "Faux! Il fallait cocher:";
                    for (int i = 0; i < 2*nbPropositions; i++)
                    {
                        if (i==indiceFermeCorrect)
                        {
                            correction += " " + MainForm.alphabet[i];
                        }
                    if (i-nbPropositions == indiceOuvertCorrect)
                    {
                        correction += " et " + MainForm.alphabet[i];
                    }
                    }

                }
               
                lblCorrection.Text = correction;
            Controls.Add(lblCorrection);

            
        }

        private void NettoieForm()
        {
            Controls.Remove(lblCorrection);
            Controls.Remove(lblConsigne);
            Controls.Remove(lblEtape);
            Controls.Remove(lblFerme);
            Controls.Remove(lblOuvert);
            
            if (lesRadiobuttons != null)
            {
                for (int i = 0; i < nbPropositions; i++)
                {
                    zoneRadButFerme.Controls.Remove(lesRadiobuttons[i]);
                    zoneRadButOuvert.Controls.Remove(lesRadiobuttons[i + nbPropositions]);
                }
              
            }
        }

        private void MAJscore( bool estJuste)
        {
                if (estJuste)
                {
                score += 1;
                }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if (btnValider.Text == "Terminer")
            {
                    Application.Exit();
            }
                
            
            if (compteur< grapheDijkstra.GetNbEtapes())
            {
                if (btnValider.Text == "Suivant" )
                {
                    //grapheDijkstra.SommetActuel = grapheDijkstra.PlusCourtChemin[compteur];
                    NettoieForm();                  
                    AfficheChoixPossible();
                }
                else
                {
                    btnValider.Text = "Suivant";
                    AfficheCorrection(Corrige());
                    MAJscore(Corrige());
                    compteur++;
                }
            }
            else
            {
                btnValider.Text = "Terminer";
                NettoieForm();
                

            }

        }
        public void NoteSur2()
        {
            score = (score * 2) / grapheDijkstra.GetNbEtapes();
        }

        private void GenerePropositions(int numEtape, out List<Sommet>[] propositionsOuverts, out List<Sommet>[] propositionsFermes)
        {
            List<Sommet> reponseCorrecteFermes = grapheDijkstra.EtatsSuccessifsFermes[numEtape];
            List<Sommet> reponseCorrecteOuverts = grapheDijkstra.EtatsSuccessifsOuverts[numEtape];
            List<Sommet> copieReponseCorrecteFermes = grapheDijkstra.DeepCopy(reponseCorrecteFermes);
            List<Sommet> copieReponseCorrecteOuverts = grapheDijkstra.DeepCopy(reponseCorrecteOuverts);
            nbPropositions = 1;
            List<Sommet> OuvertsEtapePrecedente = new List<Sommet>();
            List<Sommet> FermesEtapePrecedente = new List<Sommet>();
            if (numEtape != 0)
            {
                OuvertsEtapePrecedente = grapheDijkstra.EtatsSuccessifsOuverts[numEtape - 1];
                FermesEtapePrecedente = grapheDijkstra.EtatsSuccessifsFermes[numEtape - 1];
                nbPropositions = Math.Min(OuvertsEtapePrecedente.Count,4);
            }
            propositionsOuverts = new List<Sommet>[nbPropositions];
            propositionsFermes = new List<Sommet>[nbPropositions];
            indiceOuvertCorrect = Graphe.rnd.Next(Math.Min(nbPropositions,4));
            indiceFermeCorrect = Graphe.rnd.Next(Math.Min(nbPropositions, 4));
            propositionsOuverts[indiceOuvertCorrect] = reponseCorrecteOuverts;
            propositionsFermes[indiceFermeCorrect] = reponseCorrecteFermes;
            List<int> indicesDejaTestesOuverts = new List<int>();
            List<int> indicesDejaTestesFermes = new List<int>();

            for(int i = 1; i < nbPropositions; i++) //on va faire des rotations sur les sommets fermés puis, après chaque rotation effectuée, on ajoute le premier ouvert à la fin de la liste des fermés
            {
                Sommet F = OuvertsEtapePrecedente[i];
                List<Sommet> configurationPoss = GenereConfigurations(OuvertsEtapePrecedente, F);
                FermesEtapePrecedente.Add(F);
                //List<Sommet> configCorrecte = new List<Sommet>();
                List<Sommet> finOuverts = new List<Sommet>();
                //for(int j = 0; j < nbPropositions - 1; j++)
                //{
                //    configCorrecte.Add(reponseCorrecteOuverts[j]);
                //}
                for(int j = nbPropositions - 1; j < reponseCorrecteOuverts.Count; j++)
                {
                    finOuverts.Add(reponseCorrecteOuverts[j]);
                }
                //if (!configurationPoss.Equals(configCorrecte))
                //{
                    configurationPoss.AddRange(finOuverts);
                    int k;
                    do
                    {
                        k = Graphe.rnd.Next(nbPropositions);
                    }
                    while (k == indiceOuvertCorrect || indicesDejaTestesOuverts.Contains(k));
                    //on l'ajoute à la liste des propositions
                    propositionsOuverts[k] = grapheDijkstra.DeepCopy(configurationPoss);
                    indicesDejaTestesOuverts.Add(k);

                    do
                    {
                        k = Graphe.rnd.Next(nbPropositions);
                    }
                    while (k == indiceFermeCorrect || indicesDejaTestesFermes.Contains(k));
                    //on l'ajoute à la liste des propositions
                    propositionsFermes[k] = grapheDijkstra.DeepCopy(FermesEtapePrecedente);
                    indicesDejaTestesFermes.Add(k);
                
                    FermesEtapePrecedente.Remove(F);
                //}
            }
        }
        public List<Sommet> GenereConfigurations(List<Sommet> liste, Sommet aRetirer)
        {
            List<Sommet> copieListe = grapheDijkstra.DeepCopy(liste);
            copieListe.Remove(aRetirer);
            return copieListe;
        }
    }
}
