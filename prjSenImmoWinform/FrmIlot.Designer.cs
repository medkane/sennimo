namespace prjSenImmoWinform
{
    partial class FrmIlot
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
            this.components = new System.ComponentModel.Container();
            this.dgIlots = new System.Windows.Forms.DataGridView();
            this.cmsLots = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.détailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterUnLotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdNouveauIlot = new System.Windows.Forms.Button();
            this.dtpDateFinLivraison = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateDebutLivraison = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateFinTravaux = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDateDemarrageTravaux = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpOuverture = new System.Windows.Forms.DateTimePicker();
            this.chkOuvert = new System.Windows.Forms.CheckBox();
            this.txtCommentairesIlot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroIlot = new System.Windows.Forms.TextBox();
            this.cmdSupprimerIlot = new System.Windows.Forms.Button();
            this.cmdEnregistrerIlot = new System.Windows.Forms.Button();
            this.cmdEditerIlot = new System.Windows.Forms.Button();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.pTypeConstruction = new System.Windows.Forms.Panel();
            this.rbImmeuble = new System.Windows.Forms.RadioButton();
            this.rbIlot = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgIlots)).BeginInit();
            this.cmsLots.SuspendLayout();
            this.pTypeConstruction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgIlots
            // 
            this.dgIlots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgIlots.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(219)))), ((int)(((byte)(197)))));
            this.dgIlots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIlots.ContextMenuStrip = this.cmsLots;
            this.dgIlots.Location = new System.Drawing.Point(3, 4);
            this.dgIlots.MultiSelect = false;
            this.dgIlots.Name = "dgIlots";
            this.dgIlots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgIlots.Size = new System.Drawing.Size(510, 229);
            this.dgIlots.TabIndex = 186;
            this.dgIlots.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgIlots_CellContentClick);
            this.dgIlots.SelectionChanged += new System.EventHandler(this.dgIlots_SelectionChanged);
            this.dgIlots.DoubleClick += new System.EventHandler(this.dgIlots_DoubleClick);
            // 
            // cmsLots
            // 
            this.cmsLots.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.détailsToolStripMenuItem,
            this.ajouterUnLotToolStripMenuItem});
            this.cmsLots.Name = "cmsLots";
            this.cmsLots.Size = new System.Drawing.Size(152, 48);
            // 
            // détailsToolStripMenuItem
            // 
            this.détailsToolStripMenuItem.Name = "détailsToolStripMenuItem";
            this.détailsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.détailsToolStripMenuItem.Text = "Détails de l\'ilot";
            this.détailsToolStripMenuItem.Click += new System.EventHandler(this.détailsToolStripMenuItem_Click);
            // 
            // ajouterUnLotToolStripMenuItem
            // 
            this.ajouterUnLotToolStripMenuItem.Name = "ajouterUnLotToolStripMenuItem";
            this.ajouterUnLotToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ajouterUnLotToolStripMenuItem.Text = "Ajouter un lot";
            // 
            // cmdNouveauIlot
            // 
            this.cmdNouveauIlot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdNouveauIlot.Location = new System.Drawing.Point(408, 8);
            this.cmdNouveauIlot.Name = "cmdNouveauIlot";
            this.cmdNouveauIlot.Size = new System.Drawing.Size(91, 27);
            this.cmdNouveauIlot.TabIndex = 181;
            this.cmdNouveauIlot.Text = "Nouveau";
            this.cmdNouveauIlot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdNouveauIlot.UseVisualStyleBackColor = true;
            this.cmdNouveauIlot.Click += new System.EventHandler(this.cmdNouveauIlot_Click);
            // 
            // dtpDateFinLivraison
            // 
            this.dtpDateFinLivraison.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFinLivraison.Location = new System.Drawing.Point(293, 136);
            this.dtpDateFinLivraison.Name = "dtpDateFinLivraison";
            this.dtpDateFinLivraison.Size = new System.Drawing.Size(99, 20);
            this.dtpDateFinLivraison.TabIndex = 206;
            this.dtpDateFinLivraison.ValueChanged += new System.EventHandler(this.dtpDateFinLivraison_ValueChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(264, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 205;
            this.label2.Text = "et le";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDateDebutLivraison
            // 
            this.dtpDateDebutLivraison.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDebutLivraison.Location = new System.Drawing.Point(88, 136);
            this.dtpDateDebutLivraison.Name = "dtpDateDebutLivraison";
            this.dtpDateDebutLivraison.Size = new System.Drawing.Size(96, 20);
            this.dtpDateDebutLivraison.TabIndex = 204;
            this.dtpDateDebutLivraison.ValueChanged += new System.EventHandler(this.dtpDateDebutLivraison_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-1, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 203;
            this.label4.Text = "Livraison entre le";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateFinTravaux
            // 
            this.dtpDateFinTravaux.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFinTravaux.Location = new System.Drawing.Point(293, 84);
            this.dtpDateFinTravaux.Name = "dtpDateFinTravaux";
            this.dtpDateFinTravaux.Size = new System.Drawing.Size(99, 20);
            this.dtpDateFinTravaux.TabIndex = 202;
            this.dtpDateFinTravaux.ValueChanged += new System.EventHandler(this.dtpDateFinTravaux_ValueChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(264, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 201;
            this.label6.Text = "et le";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDateDemarrageTravaux
            // 
            this.dtpDateDemarrageTravaux.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDemarrageTravaux.Location = new System.Drawing.Point(88, 84);
            this.dtpDateDemarrageTravaux.Name = "dtpDateDemarrageTravaux";
            this.dtpDateDemarrageTravaux.Size = new System.Drawing.Size(96, 20);
            this.dtpDateDemarrageTravaux.TabIndex = 200;
            this.dtpDateDemarrageTravaux.ValueChanged += new System.EventHandler(this.dtpDateDemarrageTravaux_ValueChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 199;
            this.label5.Text = "Travaux entre le";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpOuverture
            // 
            this.dtpOuverture.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOuverture.Location = new System.Drawing.Point(206, 110);
            this.dtpOuverture.Name = "dtpOuverture";
            this.dtpOuverture.Size = new System.Drawing.Size(99, 20);
            this.dtpOuverture.TabIndex = 198;
            this.dtpOuverture.ValueChanged += new System.EventHandler(this.dtpOuverture_ValueChanged);
            // 
            // chkOuvert
            // 
            this.chkOuvert.AutoSize = true;
            this.chkOuvert.Location = new System.Drawing.Point(88, 112);
            this.chkOuvert.Name = "chkOuvert";
            this.chkOuvert.Size = new System.Drawing.Size(116, 17);
            this.chkOuvert.TabIndex = 197;
            this.chkOuvert.Text = "Ouvet à la vente le";
            this.chkOuvert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOuvert.UseVisualStyleBackColor = true;
            // 
            // txtCommentairesIlot
            // 
            this.txtCommentairesIlot.Location = new System.Drawing.Point(88, 165);
            this.txtCommentairesIlot.Multiline = true;
            this.txtCommentairesIlot.Name = "txtCommentairesIlot";
            this.txtCommentairesIlot.Size = new System.Drawing.Size(304, 84);
            this.txtCommentairesIlot.TabIndex = 175;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 158;
            this.label1.Text = "Numéro ilôt";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 162;
            this.label3.Text = "Commentaires";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumeroIlot
            // 
            this.txtNumeroIlot.Location = new System.Drawing.Point(88, 57);
            this.txtNumeroIlot.Name = "txtNumeroIlot";
            this.txtNumeroIlot.Size = new System.Drawing.Size(304, 20);
            this.txtNumeroIlot.TabIndex = 173;
            // 
            // cmdSupprimerIlot
            // 
            this.cmdSupprimerIlot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSupprimerIlot.Location = new System.Drawing.Point(408, 95);
            this.cmdSupprimerIlot.Name = "cmdSupprimerIlot";
            this.cmdSupprimerIlot.Size = new System.Drawing.Size(91, 27);
            this.cmdSupprimerIlot.TabIndex = 184;
            this.cmdSupprimerIlot.Text = "Supprimer";
            this.cmdSupprimerIlot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdSupprimerIlot.UseVisualStyleBackColor = true;
            this.cmdSupprimerIlot.Click += new System.EventHandler(this.cmdSupprimerIlot_Click);
            // 
            // cmdEnregistrerIlot
            // 
            this.cmdEnregistrerIlot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEnregistrerIlot.Location = new System.Drawing.Point(408, 66);
            this.cmdEnregistrerIlot.Name = "cmdEnregistrerIlot";
            this.cmdEnregistrerIlot.Size = new System.Drawing.Size(91, 27);
            this.cmdEnregistrerIlot.TabIndex = 183;
            this.cmdEnregistrerIlot.Text = "Enregistrer";
            this.cmdEnregistrerIlot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdEnregistrerIlot.UseVisualStyleBackColor = true;
            this.cmdEnregistrerIlot.Click += new System.EventHandler(this.cmdEnregistrerIlot_Click);
            // 
            // cmdEditerIlot
            // 
            this.cmdEditerIlot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEditerIlot.Location = new System.Drawing.Point(408, 37);
            this.cmdEditerIlot.Name = "cmdEditerIlot";
            this.cmdEditerIlot.Size = new System.Drawing.Size(91, 27);
            this.cmdEditerIlot.TabIndex = 182;
            this.cmdEditerIlot.Text = "Editer";
            this.cmdEditerIlot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdEditerIlot.UseVisualStyleBackColor = true;
            this.cmdEditerIlot.Click += new System.EventHandler(this.cmdEditerIlot_Click);
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(442, 525);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(83, 31);
            this.cmdFermer.TabIndex = 187;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // pTypeConstruction
            // 
            this.pTypeConstruction.Controls.Add(this.rbImmeuble);
            this.pTypeConstruction.Controls.Add(this.rbIlot);
            this.pTypeConstruction.Location = new System.Drawing.Point(213, 5);
            this.pTypeConstruction.Name = "pTypeConstruction";
            this.pTypeConstruction.Size = new System.Drawing.Size(178, 25);
            this.pTypeConstruction.TabIndex = 207;
            // 
            // rbImmeuble
            // 
            this.rbImmeuble.AutoSize = true;
            this.rbImmeuble.Checked = true;
            this.rbImmeuble.Location = new System.Drawing.Point(3, 4);
            this.rbImmeuble.Name = "rbImmeuble";
            this.rbImmeuble.Size = new System.Drawing.Size(85, 17);
            this.rbImmeuble.TabIndex = 1;
            this.rbImmeuble.TabStop = true;
            this.rbImmeuble.Text = "Appartement";
            this.rbImmeuble.UseVisualStyleBackColor = true;
            this.rbImmeuble.CheckedChanged += new System.EventHandler(this.rbImmeuble_CheckedChanged);
            // 
            // rbIlot
            // 
            this.rbIlot.AutoSize = true;
            this.rbIlot.Location = new System.Drawing.Point(94, 5);
            this.rbIlot.Name = "rbIlot";
            this.rbIlot.Size = new System.Drawing.Size(44, 17);
            this.rbIlot.TabIndex = 0;
            this.rbIlot.Text = "Villa";
            this.rbIlot.UseVisualStyleBackColor = true;
            this.rbIlot.CheckedChanged += new System.EventHandler(this.rbIlot_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(6, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgIlots);
            this.splitContainer1.Size = new System.Drawing.Size(519, 509);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 188;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Beige;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cmbProjets);
            this.panel2.Controls.Add(this.pTypeConstruction);
            this.panel2.Controls.Add(this.cmdNouveauIlot);
            this.panel2.Controls.Add(this.cmdEditerIlot);
            this.panel2.Controls.Add(this.dtpDateFinLivraison);
            this.panel2.Controls.Add(this.cmdEnregistrerIlot);
            this.panel2.Controls.Add(this.txtNumeroIlot);
            this.panel2.Controls.Add(this.cmdSupprimerIlot);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpDateDebutLivraison);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtCommentairesIlot);
            this.panel2.Controls.Add(this.dtpDateFinTravaux);
            this.panel2.Controls.Add(this.chkOuvert);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.dtpOuverture);
            this.panel2.Controls.Add(this.dtpDateDemarrageTravaux);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(507, 256);
            this.panel2.TabIndex = 186;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 10);
            this.groupBox1.TabIndex = 211;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 17);
            this.label7.TabIndex = 209;
            this.label7.Text = "Projet";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(88, 6);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(116, 21);
            this.cmbProjets.TabIndex = 208;
            this.cmbProjets.SelectedIndexChanged += new System.EventHandler(this.cmbProjets_SelectedIndexChanged);
            // 
            // FrmIlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(532, 563);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdFermer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmIlot";
            this.Text = "FrmIlot";
            ((System.ComponentModel.ISupportInitialize)(this.dgIlots)).EndInit();
            this.cmsLots.ResumeLayout(false);
            this.pTypeConstruction.ResumeLayout(false);
            this.pTypeConstruction.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgIlots;
        private System.Windows.Forms.Button cmdNouveauIlot;
        private System.Windows.Forms.TextBox txtCommentairesIlot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumeroIlot;
        private System.Windows.Forms.Button cmdSupprimerIlot;
        private System.Windows.Forms.Button cmdEnregistrerIlot;
        private System.Windows.Forms.Button cmdEditerIlot;
        private System.Windows.Forms.DateTimePicker dtpOuverture;
        private System.Windows.Forms.CheckBox chkOuvert;
        private System.Windows.Forms.ContextMenuStrip cmsLots;
        private System.Windows.Forms.ToolStripMenuItem détailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnLotToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtpDateFinTravaux;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDateDemarrageTravaux;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.DateTimePicker dtpDateFinLivraison;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateDebutLivraison;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pTypeConstruction;
        private System.Windows.Forms.RadioButton rbImmeuble;
        private System.Windows.Forms.RadioButton rbIlot;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbProjets;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}