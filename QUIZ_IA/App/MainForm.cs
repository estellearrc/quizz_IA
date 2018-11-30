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

        public static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public static Dijkstra d;
        public static Quiz _quiz;
        CheckBox[] lesCheckBoxes = null;
        
        public int compteur = 0;
        
        public MainForm(IQuizRepository quizRepository)
        {
            InitializeComponent();
            _quizRepository = quizRepository;
            _quiz = new Quiz(_quizRepository.Get20Questions());
            Size = new Size(700, 700);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            NumBox.Hide();
            labelSaisie.Hide();
        }

        private void AfficheScore()
        {
            _quiz.NoteSur20(_quiz.CalculNoteMax());
            txtQuestion.Font = new Font("Calibri", 20, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtQuestion.Text = "Note obtenue: " + _quiz.Score + "/20";
            string file;
            if (_quiz.Score>15)
            {
                file = "../../../DAL/images/bravo.jpg";
                pictureBox.Image = Image.FromFile(file);
                textBxNumQuestion.Text = "BRAVO !";
            }
            textBxNumQuestion.Text = "IA QUIZ !";
        }
        private void AfficheQuestion(Question question)
        {
            string file;
            if (question.PieceJointe !=null)
            {
                file = "../../../DAL/images/" + question.PieceJointe;
                pictureBox.Image= Image.FromFile(file);
            }
            else
            {
                file = "../../../DAL/images/ia.jpg" ;
                pictureBox.Image = Image.FromFile(file);
            }

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
                        if(question.Intitule == "A*")
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
                                checkBoxes[i].Location = new Point(69, 400 + i * 20);
                                checkBoxes[i].Font = new Font("Calibri", 11, FontStyle.Regular, GraphicsUnit.Point, 0);
                                checkBoxes[i].Text = alphabet[i] + ".  " + question.LesReponses[i].Intitule;
                                checkBoxes[i].AutoSize = true;
                                Controls.Add(checkBoxes[i]);
                                lesCheckBoxes[i] = checkBoxes[i];
                            }
                        }
                    }
                }

                if (question.Type == Question.TypeQues.saisieNum)// pour la saisie numérique
                {
                    NumBox.Show();
                    labelSaisie.Show();
                }
            }
        }


        private bool Corrige(Question question)
        {
            bool juste = true;
            // compter les points 
            if (question.Type == Question.TypeQues.saisieNum)
            {
                if (NumBox.Text != question.LesReponses[0].Intitule)
                {
                    juste = false;
                }
            }
            if (question.Type == Question.TypeQues.QCM)
            {
                for (int i = 0; i < question.LesReponses.Count; i++)
                {
                    if ((lesCheckBoxes[i].Checked && !question.LesReponses[i].EstCorrecte) || (!lesCheckBoxes[i].Checked && question.LesReponses[i].EstCorrecte))
                    {
                        juste = false;
                    }
                }
            }
            return juste;

        }
        private void AfficheCorrection(Question question, bool avoirJuste)
        {
            Color couleurtxt = Color.FromKnownColor(KnownColor.Green);
            if (avoirJuste)
            {

                txtBoxCorrection.Text = "C'est ça ! BRAVO !";
                txtBoxCorrection.ForeColor = couleurtxt;
            }
            else
            {
                couleurtxt = Color.FromKnownColor(KnownColor.Red);
                txtBoxCorrection.ForeColor = couleurtxt;
                string correction = "";
                if (question.Type == Question.TypeQues.QCM)
                {
                    correction = "Faux! Il fallait cocher: ";
                    for (int i = 0; i < question.LesReponses.Count; i++)
                    {
                        if (question.LesReponses[i].EstCorrecte)
                        {
                            correction +=  alphabet[i]+", " ;
                        }
                    }
                    correction = correction.Substring(0, correction.Length - 2);

                }
                else
                {
                    correction = "Non, la bonne réponse est " + question.LesReponses[0].Intitule;
                }
                txtBoxCorrection.Text = correction;
            }
        }
        private void NettoieForm()
        {
            txtBoxCorrection.Text = "";
            textBxNumQuestion.Text = "";
            NumBox.Hide();
            labelSaisie.Hide();
            NumBox.Text = "";
           
            if (lesCheckBoxes != null)
            {
                foreach (CheckBox checkbox in lesCheckBoxes)
                {
                    Controls.Remove(checkbox);

                }

            }
        }
        private void MAJscore(Question question, bool estJuste)
        {

            if (question.Intitule == "Dijkstra" || question.Intitule == "A*")
            {

            }
            else
            {
                if (estJuste)
                {
                    _quiz.ActualiseScore(question.Points);
                }

            }
        }
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (btnValider.Text == "Terminer")
            {
                var result = MessageBox.Show("Voulez vous recommencer ?", "Recommencer", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
                else
                {
                    txtQuestion.Font = new Font("Calibri", 14, FontStyle.Bold, GraphicsUnit.Point, 0);
                    NettoieForm();
                    compteur = 0;
                    _quiz = new Quiz(_quizRepository.Get20Questions());
                    btnValider.Text = "Commencer";
                }
            }
            if (compteur < _quiz.LesQuestions.Count)
            {
                if (btnValider.Text == "Suivant" || btnValider.Text == "Commencer")
                {
                    if (btnValider.Text == "Suivant")
                    {
                        NettoieForm();
                    }
                    textBxNumQuestion.Text = "Question " + (compteur + 1) + "/" + _quiz.LesQuestions.Count;
                    AfficheQuestion(_quiz.LesQuestions[compteur]);
                }
                else
                {
                    btnValider.Text = "Suivant";
                    AfficheCorrection(_quiz.LesQuestions[compteur], Corrige(_quiz.LesQuestions[compteur]));
                    MAJscore(_quiz.LesQuestions[compteur], Corrige(_quiz.LesQuestions[compteur]));
                    compteur++;
                }
            }
            else
            {
                btnValider.Text = "Terminer";
                NettoieForm();
                AfficheScore();

            }

        }

        private void NumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
            {
                e.Handled = true;
            }
        }

       
    }
}