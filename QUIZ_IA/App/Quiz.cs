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
    public partial class Quiz : Form
    {
        private List<Question> Lequiz { get; set; }
        CheckBox[] rd = null;
        public Quiz()
        {
            InitializeComponent();
           
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

            Lequiz = new List<Question>();
            Lequiz.Add(quest1);
            Lequiz.Add(quest2);

            int score;

            CheckBox[] rd = null;
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            foreach(Question question in Lequiz)
            {
                if(question!=null)
                {
                    txtQuestion.Text = question.Intitule;
                    CheckBox[] checkBoxes = new CheckBox[question.LesReponses.Count];
                    rd = new CheckBox[question.LesReponses.Count];

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
        }
    }
}
