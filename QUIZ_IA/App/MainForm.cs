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
        public static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
        private Quiz Lequiz { get; set; }
        CheckBox[] ck = null;
        public MainForm()
        {
            InitializeComponent();
            Size = new Size(550, 550);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Dijkstra d = new Dijkstra();
            d.Show();

            List<Reponse> lesRepq = new List<Reponse>();
            lesRepq.Add(new Reponse(0, "L'IA forte a conscience d'elle-même", 1));
            lesRepq.Add(new Reponse(1, "L'IA forte est plus rapide que l'IA faible", 0));
            lesRepq.Add(new Reponse(2, "L'IA forte écrase la faible aux échecs", 0));
            lesRepq.Add(new Reponse(3, "L'IA faible ne sait faire qu'une seule tâche", 1));
            Question quest1 = new Question(0, "Qu'est-ce qui différencie l'IA forte de l'IA faible ?", lesRepq, 0);

            List<Reponse> lesRepq2 = new List<Reponse>();
            lesRepq2.Add(new Reponse(4, "L'algorithme A* est une version simplifiée de Dijkstra", 0));
            lesRepq2.Add(new Reponse(5, "L'ajout d’une heuristique pour estimer le coût du chemin restant pour atteindre le but", 1));
            lesRepq2.Add(new Reponse(6, "L'ajout d’une heuristique pour estimer le coût du chemin restant pour atteindre le but", 0));
            Question quest2 = new Question(1, "Quel est l'intérêt d'utiliser A* plutôt que Dijkstra classique ?", lesRepq2, 0);

            List<Question> questions = new List<Question>();
            questions.Add(quest1);
            questions.Add(quest2);

            Lequiz = new Quiz(questions);

            int score;


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
                    checkBoxes[i].Text = question.LesReponses[i].Intitule;
                    checkBoxes[i].AutoSize = true;
                    this.Controls.Add(checkBoxes[i]);

                }


            }
        }

        private void Affiche_Correction(Question question) { }
        private void btnValider_Click(object sender, EventArgs e)
        {

        }
    }
}
