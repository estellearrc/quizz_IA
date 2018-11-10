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
        CheckBox[] ck = null;

        public MainForm(IQuizRepository quizRepository)
        {
            InitializeComponent();
            _quizRepository = quizRepository;

            Size = new Size(550, 550); //dans le designer.cs
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Dijkstra d = new Dijkstra();
            d.Show();
        }


        private void Afficher_Question(Question question)
        {
            if (question != null) //par précaution on teste si la question est nulle mais normalement elle ne sera jamais nulle
            {

                btnValider.Text = "Valider";
                txtQuestion.Text = question.Intitule;
                CheckBox[] checkBoxes = new CheckBox[question.LesReponses.Count];
                ck = new CheckBox[question.LesReponses.Count];

                for (int i = 0; i < question.LesReponses.Count; i++)
                {
                    checkBoxes[i] = new CheckBox();
                    checkBoxes[i].Location = new Point(69, 145 + i * 20);
                    checkBoxes[i].Text = alphabet[i]+".  " + question.LesReponses[i].Intitule;
                    checkBoxes[i].AutoSize = true;
                    this.Controls.Add(checkBoxes[i]);

                }


            }
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
                        if (question.LesReponses[i].EstCorrecte())
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
        private void btnValider_Click(object sender, EventArgs e)
        {
            Afficher_Question(_quizRepository.GetAllQuestions()[0]);
            

        }
    }
}
