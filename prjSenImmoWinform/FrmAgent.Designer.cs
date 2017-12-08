namespace prjSenImmoWinform
{
    partial class FrmAgent
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
            this.cmdFermer = new System.Windows.Forms.Button();
            this.cmdSupprimer = new System.Windows.Forms.Button();
            this.cmdEditer = new System.Windows.Forms.Button();
            this.cmdNouveau = new System.Windows.Forms.Button();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMatricule = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.dgUtilisateurs = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pChefEquipe = new System.Windows.Forms.Panel();
            this.chkEstChefEquipe = new System.Windows.Forms.CheckBox();
            this.cmbChefsEquipes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelephoneFixe = new System.Windows.Forms.MaskedTextBox();
            this.txtTelephoneMobile = new System.Windows.Forms.MaskedTextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgUtilisateurs)).BeginInit();
            this.panel1.SuspendLayout();
            this.pChefEquipe.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(464, 549);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(90, 35);
            this.cmdFermer.TabIndex = 140;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // cmdSupprimer
            // 
            this.cmdSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSupprimer.Location = new System.Drawing.Point(437, 91);
            this.cmdSupprimer.Name = "cmdSupprimer";
            this.cmdSupprimer.Size = new System.Drawing.Size(91, 27);
            this.cmdSupprimer.TabIndex = 137;
            this.cmdSupprimer.Text = "Supprimer";
            this.cmdSupprimer.UseVisualStyleBackColor = true;
            this.cmdSupprimer.Click += new System.EventHandler(this.cmdSupprimer_Click);
            // 
            // cmdEditer
            // 
            this.cmdEditer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEditer.Location = new System.Drawing.Point(437, 35);
            this.cmdEditer.Name = "cmdEditer";
            this.cmdEditer.Size = new System.Drawing.Size(91, 27);
            this.cmdEditer.TabIndex = 136;
            this.cmdEditer.Text = "Editer";
            this.cmdEditer.UseVisualStyleBackColor = true;
            this.cmdEditer.Click += new System.EventHandler(this.cmdEditer_Click);
            // 
            // cmdNouveau
            // 
            this.cmdNouveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNouveau.Location = new System.Drawing.Point(437, 7);
            this.cmdNouveau.Name = "cmdNouveau";
            this.cmdNouveau.Size = new System.Drawing.Size(91, 27);
            this.cmdNouveau.TabIndex = 135;
            this.cmdNouveau.Text = "Nouveau";
            this.cmdNouveau.UseVisualStyleBackColor = true;
            this.cmdNouveau.Click += new System.EventHandler(this.cmdNouveau_Click);
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnregistrer.Location = new System.Drawing.Point(437, 63);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(91, 27);
            this.cmdEnregistrer.TabIndex = 9;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 130;
            this.label1.Text = "Matricule";
            // 
            // txtMatricule
            // 
            this.txtMatricule.Location = new System.Drawing.Point(82, 34);
            this.txtMatricule.Name = "txtMatricule";
            this.txtMatricule.Size = new System.Drawing.Size(110, 20);
            this.txtMatricule.TabIndex = 0;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(300, 62);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(110, 20);
            this.txtNom.TabIndex = 2;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(36, 65);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(43, 13);
            this.label22.TabIndex = 126;
            this.label22.Text = "Prénom";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(82, 62);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(179, 20);
            this.txtPrenom.TabIndex = 1;
            // 
            // dgUtilisateurs
            // 
            this.dgUtilisateurs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUtilisateurs.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgUtilisateurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUtilisateurs.Location = new System.Drawing.Point(7, 245);
            this.dgUtilisateurs.Name = "dgUtilisateurs";
            this.dgUtilisateurs.ReadOnly = true;
            this.dgUtilisateurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUtilisateurs.Size = new System.Drawing.Size(543, 296);
            this.dgUtilisateurs.TabIndex = 10;
            this.dgUtilisateurs.SelectionChanged += new System.EventHandler(this.dgUtilisateurs_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.cmbProjets);
            this.panel1.Controls.Add(this.pChefEquipe);
            this.panel1.Controls.Add(this.txtTelephoneFixe);
            this.panel1.Controls.Add(this.txtTelephoneMobile);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.Login);
            this.panel1.Controls.Add(this.txtLogin);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbRoles);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmdSupprimer);
            this.panel1.Controls.Add(this.cmdEditer);
            this.panel1.Controls.Add(this.cmdNouveau);
            this.panel1.Controls.Add(this.cmdEnregistrer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMatricule);
            this.panel1.Controls.Add(this.txtNom);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.txtPrenom);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(9, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 232);
            this.panel1.TabIndex = 137;
            // 
            // pChefEquipe
            // 
            this.pChefEquipe.Controls.Add(this.chkEstChefEquipe);
            this.pChefEquipe.Controls.Add(this.cmbChefsEquipes);
            this.pChefEquipe.Controls.Add(this.label3);
            this.pChefEquipe.Location = new System.Drawing.Point(3, 195);
            this.pChefEquipe.Name = "pChefEquipe";
            this.pChefEquipe.Size = new System.Drawing.Size(525, 29);
            this.pChefEquipe.TabIndex = 231;
            this.pChefEquipe.Visible = false;
            // 
            // chkEstChefEquipe
            // 
            this.chkEstChefEquipe.AutoSize = true;
            this.chkEstChefEquipe.Location = new System.Drawing.Point(404, 7);
            this.chkEstChefEquipe.Name = "chkEstChefEquipe";
            this.chkEstChefEquipe.Size = new System.Drawing.Size(109, 17);
            this.chkEstChefEquipe.TabIndex = 231;
            this.chkEstChefEquipe.Text = "Est Chef d\'équipe";
            this.chkEstChefEquipe.UseVisualStyleBackColor = true;
            this.chkEstChefEquipe.CheckedChanged += new System.EventHandler(this.chkEstChefEquipe_CheckedChanged);
            // 
            // cmbChefsEquipes
            // 
            this.cmbChefsEquipes.BackColor = System.Drawing.SystemColors.Info;
            this.cmbChefsEquipes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChefsEquipes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbChefsEquipes.FormattingEnabled = true;
            this.cmbChefsEquipes.Location = new System.Drawing.Point(79, 5);
            this.cmbChefsEquipes.Name = "cmbChefsEquipes";
            this.cmbChefsEquipes.Size = new System.Drawing.Size(252, 21);
            this.cmbChefsEquipes.TabIndex = 229;
            this.cmbChefsEquipes.SelectedIndexChanged += new System.EventHandler(this.cmbChefsEquipes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 230;
            this.label3.Text = "Chef d\'équipe";
            // 
            // txtTelephoneFixe
            // 
            this.txtTelephoneFixe.Location = new System.Drawing.Point(300, 88);
            this.txtTelephoneFixe.Mask = "00 000 00 00";
            this.txtTelephoneFixe.Name = "txtTelephoneFixe";
            this.txtTelephoneFixe.Size = new System.Drawing.Size(110, 20);
            this.txtTelephoneFixe.TabIndex = 4;
            // 
            // txtTelephoneMobile
            // 
            this.txtTelephoneMobile.Location = new System.Drawing.Point(82, 88);
            this.txtTelephoneMobile.Mask = "00 000 00 00";
            this.txtTelephoneMobile.Name = "txtTelephoneMobile";
            this.txtTelephoneMobile.Size = new System.Drawing.Size(110, 20);
            this.txtTelephoneMobile.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(300, 142);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(110, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // Login
            // 
            this.Login.AutoSize = true;
            this.Login.Location = new System.Drawing.Point(46, 146);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(33, 13);
            this.Login.TabIndex = 226;
            this.Login.Text = "Login";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(82, 142);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(139, 20);
            this.txtLogin.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 228;
            this.label4.Text = "Mot de passe";
            // 
            // cmbRoles
            // 
            this.cmbRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(82, 168);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(328, 21);
            this.cmbRoles.TabIndex = 6;
            this.cmbRoles.SelectedIndexChanged += new System.EventHandler(this.cmbRoles_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 221;
            this.label7.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(82, 116);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(328, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(50, 172);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 13);
            this.label17.TabIndex = 217;
            this.label17.Text = "Rôle";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 140;
            this.label6.Text = "Tél fixe";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 138;
            this.label5.Text = "Tél mobile";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 128;
            this.label2.Text = "Nom";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(5, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 17);
            this.label20.TabIndex = 299;
            this.label20.Text = "Projet";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(82, 7);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(328, 21);
            this.cmbProjets.TabIndex = 298;
            // 
            // FrmAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(237)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(560, 590);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.dgUtilisateurs);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAgent";
            this.Text = "Gestion des utilisateurs";
            ((System.ComponentModel.ISupportInitialize)(this.dgUtilisateurs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pChefEquipe.ResumeLayout(false);
            this.pChefEquipe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Button cmdSupprimer;
        private System.Windows.Forms.Button cmdEditer;
        private System.Windows.Forms.Button cmdNouveau;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMatricule;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.DataGridView dgUtilisateurs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.MaskedTextBox txtTelephoneFixe;
        private System.Windows.Forms.MaskedTextBox txtTelephoneMobile;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label Login;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pChefEquipe;
        private System.Windows.Forms.ComboBox cmbChefsEquipes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkEstChefEquipe;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbProjets;
    }
}