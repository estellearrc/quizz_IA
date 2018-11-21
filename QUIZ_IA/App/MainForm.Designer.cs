﻿namespace App
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
            this.NumBox = new System.Windows.Forms.TextBox();
            this.labelSaisie = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
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
            this.btnValider.Click += new System.EventHandler(this.BtnValider_Click);
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
            this.textBxNumQuestion.Text = "IA QUIZ ! ";
            this.textBxNumQuestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NumBox
            // 
            this.NumBox.BackColor = System.Drawing.SystemColors.Window;
            this.NumBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumBox.Location = new System.Drawing.Point(196, 199);
            this.NumBox.Name = "NumBox";
            this.NumBox.Size = new System.Drawing.Size(137, 31);
            this.NumBox.TabIndex = 5;
            this.NumBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumBox_KeyPress);
            // 
            // labelSaisie
            // 
            this.labelSaisie.AutoSize = true;
            this.labelSaisie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSaisie.Location = new System.Drawing.Point(199, 175);
            this.labelSaisie.Name = "labelSaisie";
            this.labelSaisie.Size = new System.Drawing.Size(131, 16);
            this.labelSaisie.TabIndex = 6;
            this.labelSaisie.Text = "Saisir la réponse ici :";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(108, 237);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(322, 144);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(534, 512);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelSaisie);
            this.Controls.Add(this.NumBox);
            this.Controls.Add(this.textBxNumQuestion);
            this.Controls.Add(this.txtBoxCorrection);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.txtQuestion);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Quiz IA";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.TextBox txtBoxCorrection;
        private System.Windows.Forms.TextBox textBxNumQuestion;
        private System.Windows.Forms.TextBox NumBox;
        private System.Windows.Forms.Label labelSaisie;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

