namespace App
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.txtBoxCorrection = new System.Windows.Forms.TextBox();
            this.textBxNumQuestion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtQuestion
            // 
            this.txtQuestion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuestion.Enabled = false;
            this.txtQuestion.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestion.Location = new System.Drawing.Point(31, 53);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(467, 84);
            this.txtQuestion.TabIndex = 0;
            this.txtQuestion.Text = "Afin d\'évaluer vos connaissances en IA, 20 questions vont vous etre posées. Atten" +
    "tion car il peut y avoir plusieurs réponses possibles ! Bonne chance ;)";
            this.txtQuestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.Color.Azure;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnValider.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.Location = new System.Drawing.Point(196, 445);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(137, 31);
            this.btnValider.TabIndex = 1;
            this.btnValider.Text = "Commencer";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // txtBoxCorrection
            // 
            this.txtBoxCorrection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBoxCorrection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxCorrection.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxCorrection.Location = new System.Drawing.Point(31, 386);
            this.txtBoxCorrection.Multiline = true;
            this.txtBoxCorrection.Name = "txtBoxCorrection";
            this.txtBoxCorrection.Size = new System.Drawing.Size(467, 53);
            this.txtBoxCorrection.TabIndex = 2;
            this.txtBoxCorrection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBxNumQuestion
            // 
            this.textBxNumQuestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBxNumQuestion.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBxNumQuestion.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.textBxNumQuestion.Location = new System.Drawing.Point(175, 20);
            this.textBxNumQuestion.Name = "textBxNumQuestion";
            this.textBxNumQuestion.Size = new System.Drawing.Size(174, 24);
            this.textBxNumQuestion.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(534, 512);
            this.Controls.Add(this.textBxNumQuestion);
            this.Controls.Add(this.txtBoxCorrection);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.txtQuestion);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Quiz IA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.TextBox txtBoxCorrection;
        private System.Windows.Forms.TextBox textBxNumQuestion;
    }
}

