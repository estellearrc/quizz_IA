using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Domain;

namespace App
{
    public partial class MainForm : Form
    {
        private IQuizRepository _quizRepository;

        public static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
        public static Dijkstra d;
        public static Quiz _quiz;
        CheckBox[] lesCheckBoxes = null;
        public int compteur = 0;
        
        public MainForm(IQuizRepository quizRepository)
        {
            InitializeComponent();
            _quizRepository = quizRepository;
            _quiz = new Quiz(_quizRepository.Get20Questions());
            Size = new Size(550, 550); //dans le designer.cs
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //d = new Dijkstra();
            //d.Show();
        }


        private void Afficher_Question(Question question)
        {
            if (question != null) //par précaution on teste si la question est nulle mais normalement elle ne sera jamais nulle
            {

                btnValider.Text = "Valider";
                txtQuestion.Text = question.Intitule;
                if (question.Type == Question.TypeQues.QCM)
                { 
                    CheckBox[] checkBoxes = new CheckBox[question.LesReponses.Count];
                    lesCheckBoxes = new CheckBox[question.LesReponses.Count];

                    for (int i = 0; i < question.LesReponses.Count; i++)
                    {
                        checkBoxes[i] = new CheckBox();
                        checkBoxes[i].Location = new Point(69, 145 + i * 20);
                        checkBoxes[i].Text = alphabet[i] + ".  " + question.LesReponses[i].Intitule;
                        checkBoxes[i].AutoSize = true;
                        this.Controls.Add(checkBoxes[i]);
                    }
                }

                if (question.Type == Question.TypeQues.QCM)
                {
                    for (int i = 0; i < question.LesReponses.Count; i++)
                    {

                    }
                }

                if (question.Type == Question.TypeQues.saisieNum)
                {

                }


            }
        }

        private bool Correction(Question question )
        {
            bool juste = true;
            // compter les points 
            if (question.Type == Question.TypeQues.saisieNum)
            {

            }
            return juste;
        }
        private void Affiche_Correction(Question question, bool avoirJuste) {
            Color couleurtxt = Color.FromKnownColor(KnownColor.Green);
            if (avoirJuste)
            {
                couleurtxt= Color.FromKnownColor(KnownColor.Red);
                txtBoxCorrection.Text = "C'est ça ! BRAVO !";
                txtBoxCorrection.ForeColor = couleurtxt;
            }
            else
            {
                txtBoxCorrection.ForeColor = couleurtxt;
                string correction = "";
                if (question.Type == Question.TypeQues.QCM)
                {
                    correction="Faux!Il fallait cocher:";
                    for (int i = 0; i < question.LesReponses.Count; i++)
                    {
                        if (question.LesReponses[i].EstCorrecte)
                        {
                            correction += " " + alphabet[i];
                        }                                                                             
                    }
                  
                }
                else
                {
                    correction = "Non, la bonne réponse est " + question.LesReponses[0].Intitule;
                }
                txtBoxCorrection.Text = correction;
            }
        }

        private void Nettoyer_Form()
        {
            txtBoxCorrection.Text = "";
            foreach (CheckBox checkbox in lesCheckBoxes)
            {
                this.Controls.Remove(checkbox);
            }

        }                    
                     
                    
        private void btnValider_Click(object sender, EventArgs e)
        {
           

            if (btnValider.Text == "Suivant"|| btnValider.Text == "Commencer")
            {
                if (btnValider.Text == "Suivant")
                {
                    // on nettoie le form
                }
                //on affiche la question numéro "compteur"
                Afficher_Question(_quiz.LesQuestions[compteur]);
            }
            else if(btnValider.Text == "Valider"){
                //corriger (voir si la reponse est correcte)
                //on met à jour les points
                //afficher correction
                compteur++;
            }


        }
    }
}
