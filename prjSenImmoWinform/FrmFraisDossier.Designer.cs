namespace prjSenImmoWinform
{
    partial class FrmFraisDossier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateNaissance = new System.Windows.Forms.MaskedTextBox();
            this.txtLieuNaissance = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMontantEncaissement = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCommentaires = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateEncaissement = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.groupBox1.Controls.Add(this.txtAdresse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDateNaissance);
            this.groupBox1.Controls.Add(this.txtLieuNaissance);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtNom);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtPrenom);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prospect";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(60, 75);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.ReadOnly = true;
            this.txtAdresse.Size = new System.Drawing.Size(353, 54);
            this.txtAdresse.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Adresse";
            // 
            // txtDateNaissance
            // 
            this.txtDateNaissance.Location = new System.Drawing.Point(60, 49);
            this.txtDateNaissance.Mask = "00/00/0000";
            this.txtDateNaissance.Name = "txtDateNaissance";
            this.txtDateNaissance.ReadOnly = true;
            this.txtDateNaissance.Size = new System.Drawing.Size(172, 20);
            this.txtDateNaissance.TabIndex = 37;
            this.txtDateNaissance.ValidatingType = typeof(System.DateTime);
            // 
            // txtLieuNaissance
            // 
            this.txtLieuNaissance.Location = new System.Drawing.Point(289, 49);
            this.txtLieuNaissance.Name = "txtLieuNaissance";
            this.txtLieuNaissance.ReadOnly = true;
            this.txtLieuNaissance.Size = new System.Drawing.Size(124, 20);
            this.txtLieuNaissance.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(270, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "à";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(289, 19);
            this.txtNom.Name = "txtNom";
            this.txtNom.ReadOnly = true;
            this.txtNom.Size = new System.Drawing.Size(124, 20);
            this.txtNom.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(254, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 41;
            this.label12.Text = "Nom";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Né(e) le";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(60, 19);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.ReadOnly = true;
            this.txtPrenom.Size = new System.Drawing.Size(172, 20);
            this.txtPrenom.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Prénom";
            // 
            // txtMontantEncaissement
            // 
            this.txtMontantEncaissement.Location = new System.Drawing.Point(84, 178);
            this.txtMontantEncaissement.Name = "txtMontantEncaissement";
            this.txtMontantEncaissement.Size = new System.Drawing.Size(96, 20);
            this.txtMontantEncaissement.TabIndex = 275;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 274;
            this.label5.Text = "Commentaires";
            // 
            // txtCommentaires
            // 
            this.txtCommentaires.Location = new System.Drawing.Point(84, 204);
            this.txtCommentaires.Name = "txtCommentaires";
            this.txtCommentaires.Size = new System.Drawing.Size(356, 156);
            this.txtCommentaires.TabIndex = 273;
            this.txtCommentaires.Text = "";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 272;
            this.label2.Text = "Montant";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateEncaissement
            // 
            this.dtpDateEncaissement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateEncaissement.Location = new System.Drawing.Point(84, 154);
            this.dtpDateEncaissement.Name = "dtpDateEncaissement";
            this.dtpDateEncaissement.Size = new System.Drawing.Size(96, 20);
            this.dtpDateEncaissement.TabIndex = 271;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 270;
            this.label4.Text = "Date prévue";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 32);
            this.button1.TabIndex = 278;
            this.button1.Text = "Supprimer";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Location = new System.Drawing.Point(12, 376);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(86, 32);
            this.cmdEnregistrer.TabIndex = 277;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // cmdFermer
            // 
            this.cmdFermer.Location = new System.Drawing.Point(354, 376);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(86, 32);
            this.cmdFermer.TabIndex = 276;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // FrmFraisDossier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(452, 430);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdEnregistrer);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.txtMontantEncaissement);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCommentaires);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDateEncaissement);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmFraisDossier";
            this.Text = "Gestion des frais de dossiers";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtDateNaissance;
        private System.Windows.Forms.TextBox txtLieuNaissance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMontantEncaissement;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtCommentaires;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateEncaissement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.Button cmdFermer;
    }
}