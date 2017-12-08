namespace prjSenImmoWinform
{
    partial class FrmTypeOrigine
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.txtDescriptionTypeOrigine = new System.Windows.Forms.TextBox();
            this.cmdEditer = new System.Windows.Forms.Button();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.chkActif = new System.Windows.Forms.CheckBox();
            this.pLimiteDansLeTemps = new System.Windows.Forms.Panel();
            this.dtpDateFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateDebut = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdNouveau = new System.Windows.Forms.Button();
            this.chkLimiteDansLeTemps = new System.Windows.Forms.CheckBox();
            this.txtLibelleTypeOrigine = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgTypeOrigineProspects = new System.Windows.Forms.DataGridView();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pLimiteDansLeTemps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTypeOrigineProspects)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtDescriptionTypeOrigine);
            this.panel1.Controls.Add(this.cmdEditer);
            this.panel1.Controls.Add(this.cmbProjets);
            this.panel1.Controls.Add(this.cmdEnregistrer);
            this.panel1.Controls.Add(this.chkActif);
            this.panel1.Controls.Add(this.pLimiteDansLeTemps);
            this.panel1.Controls.Add(this.cmdNouveau);
            this.panel1.Controls.Add(this.chkLimiteDansLeTemps);
            this.panel1.Controls.Add(this.txtLibelleTypeOrigine);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 221);
            this.panel1.TabIndex = 272;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(301, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 17);
            this.label8.TabIndex = 277;
            this.label8.Text = "Projet";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // cmbProjets
            // 
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(368, 185);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(104, 21);
            this.cmbProjets.TabIndex = 276;
            this.cmbProjets.Visible = false;
            this.cmbProjets.SelectedIndexChanged += new System.EventHandler(this.cmbProjets_SelectedIndexChanged);
            // 
            // txtDescriptionTypeOrigine
            // 
            this.txtDescriptionTypeOrigine.Location = new System.Drawing.Point(8, 52);
            this.txtDescriptionTypeOrigine.Multiline = true;
            this.txtDescriptionTypeOrigine.Name = "txtDescriptionTypeOrigine";
            this.txtDescriptionTypeOrigine.Size = new System.Drawing.Size(376, 99);
            this.txtDescriptionTypeOrigine.TabIndex = 273;
            // 
            // cmdEditer
            // 
            this.cmdEditer.Location = new System.Drawing.Point(390, 44);
            this.cmdEditer.Name = "cmdEditer";
            this.cmdEditer.Size = new System.Drawing.Size(82, 28);
            this.cmdEditer.TabIndex = 275;
            this.cmdEditer.Text = "Editer";
            this.cmdEditer.UseVisualStyleBackColor = true;
            this.cmdEditer.Click += new System.EventHandler(this.cmdEditer_Click);
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Location = new System.Drawing.Point(390, 78);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(82, 28);
            this.cmdEnregistrer.TabIndex = 270;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // chkActif
            // 
            this.chkActif.AutoSize = true;
            this.chkActif.Location = new System.Drawing.Point(344, 156);
            this.chkActif.Name = "chkActif";
            this.chkActif.Size = new System.Drawing.Size(47, 17);
            this.chkActif.TabIndex = 272;
            this.chkActif.Text = "Actif";
            this.chkActif.UseVisualStyleBackColor = true;
            // 
            // pLimiteDansLeTemps
            // 
            this.pLimiteDansLeTemps.Controls.Add(this.dtpDateFin);
            this.pLimiteDansLeTemps.Controls.Add(this.label3);
            this.pLimiteDansLeTemps.Controls.Add(this.dtpDateDebut);
            this.pLimiteDansLeTemps.Controls.Add(this.label4);
            this.pLimiteDansLeTemps.Location = new System.Drawing.Point(8, 180);
            this.pLimiteDansLeTemps.Name = "pLimiteDansLeTemps";
            this.pLimiteDansLeTemps.Size = new System.Drawing.Size(283, 28);
            this.pLimiteDansLeTemps.TabIndex = 271;
            this.pLimiteDansLeTemps.Visible = false;
            // 
            // dtpDateFin
            // 
            this.dtpDateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFin.Location = new System.Drawing.Point(175, 2);
            this.dtpDateFin.Name = "dtpDateFin";
            this.dtpDateFin.Size = new System.Drawing.Size(96, 20);
            this.dtpDateFin.TabIndex = 201;
            this.dtpDateFin.ValueChanged += new System.EventHandler(this.dtpDateFin_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 200;
            this.label3.Text = "à";
            // 
            // dtpDateDebut
            // 
            this.dtpDateDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDebut.Location = new System.Drawing.Point(53, 2);
            this.dtpDateDebut.Name = "dtpDateDebut";
            this.dtpDateDebut.Size = new System.Drawing.Size(96, 20);
            this.dtpDateDebut.TabIndex = 15;
            this.dtpDateDebut.ValueChanged += new System.EventHandler(this.dtpDateDebut_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Période";
            // 
            // cmdNouveau
            // 
            this.cmdNouveau.Location = new System.Drawing.Point(390, 10);
            this.cmdNouveau.Name = "cmdNouveau";
            this.cmdNouveau.Size = new System.Drawing.Size(82, 28);
            this.cmdNouveau.TabIndex = 273;
            this.cmdNouveau.Text = "Nouveau";
            this.cmdNouveau.UseVisualStyleBackColor = true;
            this.cmdNouveau.Click += new System.EventHandler(this.cmdNouveau_Click);
            // 
            // chkLimiteDansLeTemps
            // 
            this.chkLimiteDansLeTemps.AutoSize = true;
            this.chkLimiteDansLeTemps.Location = new System.Drawing.Point(8, 156);
            this.chkLimiteDansLeTemps.Name = "chkLimiteDansLeTemps";
            this.chkLimiteDansLeTemps.Size = new System.Drawing.Size(121, 17);
            this.chkLimiteDansLeTemps.TabIndex = 270;
            this.chkLimiteDansLeTemps.Text = "Limité dans le temps";
            this.chkLimiteDansLeTemps.UseVisualStyleBackColor = true;
            this.chkLimiteDansLeTemps.CheckedChanged += new System.EventHandler(this.chkLimiteDansLeTemps_CheckedChanged);
            // 
            // txtLibelleTypeOrigine
            // 
            this.txtLibelleTypeOrigine.Location = new System.Drawing.Point(44, 10);
            this.txtLibelleTypeOrigine.Name = "txtLibelleTypeOrigine";
            this.txtLibelleTypeOrigine.Size = new System.Drawing.Size(340, 20);
            this.txtLibelleTypeOrigine.TabIndex = 269;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 203;
            this.label5.Text = "Description";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 199;
            this.label2.Text = "Libellé";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgTypeOrigineProspects
            // 
            this.dgTypeOrigineProspects.BackgroundColor = System.Drawing.Color.White;
            this.dgTypeOrigineProspects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTypeOrigineProspects.GridColor = System.Drawing.Color.White;
            this.dgTypeOrigineProspects.Location = new System.Drawing.Point(5, 231);
            this.dgTypeOrigineProspects.MultiSelect = false;
            this.dgTypeOrigineProspects.Name = "dgTypeOrigineProspects";
            this.dgTypeOrigineProspects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTypeOrigineProspects.Size = new System.Drawing.Size(486, 246);
            this.dgTypeOrigineProspects.TabIndex = 274;
            this.dgTypeOrigineProspects.SelectionChanged += new System.EventHandler(this.dgTypeOrigineProspects_SelectionChanged);
            // 
            // cmdFermer
            // 
            this.cmdFermer.Location = new System.Drawing.Point(411, 488);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(80, 30);
            this.cmdFermer.TabIndex = 276;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // FrmTypeOrigine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(219)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(496, 530);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.dgTypeOrigineProspects);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTypeOrigine";
            this.Text = "Prosopis - Gestion des origines des prospects";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pLimiteDansLeTemps.ResumeLayout(false);
            this.pLimiteDansLeTemps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTypeOrigineProspects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLibelleTypeOrigine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateDebut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.Panel pLimiteDansLeTemps;
        private System.Windows.Forms.CheckBox chkLimiteDansLeTemps;
        private System.Windows.Forms.Button cmdNouveau;
        private System.Windows.Forms.DataGridView dgTypeOrigineProspects;
        private System.Windows.Forms.CheckBox chkActif;
        private System.Windows.Forms.DateTimePicker dtpDateFin;
        private System.Windows.Forms.Button cmdEditer;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.TextBox txtDescriptionTypeOrigine;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbProjets;
    }
}