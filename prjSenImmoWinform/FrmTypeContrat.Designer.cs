namespace prjSenImmoWinform
{
    partial class FrmTypeContrat
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
            this.lvTypeContrats = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsTypeContrat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.déactiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.réactiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.pTypeConstruction = new System.Windows.Forms.Panel();
            this.rbImmeuble = new System.Windows.Forms.RadioButton();
            this.rbIlot = new System.Windows.Forms.RadioButton();
            this.chkActif = new System.Windows.Forms.CheckBox();
            this.cmdSupprimer = new System.Windows.Forms.Button();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.cmdEditer = new System.Windows.Forms.Button();
            this.cmdNouveau = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSeuilReservation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSeuilSouscription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLibelle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbDepot = new System.Windows.Forms.RadioButton();
            this.rbReservation = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.LvNiveauAvancements = new System.Windows.Forms.ListView();
            this.txtTauxEncaissementTotal = new System.Windows.Forms.TextBox();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtOrdreNA = new System.Windows.Forms.TextBox();
            this.txtNiveauNA = new System.Windows.Forms.TextBox();
            this.txtTauxNA = new System.Windows.Forms.TextBox();
            this.cmdNewNA = new System.Windows.Forms.Button();
            this.cmdEditerNA = new System.Windows.Forms.Button();
            this.cmdEnregistrerNA = new System.Windows.Forms.Button();
            this.cmdSupprimerNA = new System.Windows.Forms.Button();
            this.cmsTypeContrat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pTypeConstruction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvTypeContrats
            // 
            this.lvTypeContrats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTypeContrats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvTypeContrats.ContextMenuStrip = this.cmsTypeContrat;
            this.lvTypeContrats.FullRowSelect = true;
            this.lvTypeContrats.HideSelection = false;
            this.lvTypeContrats.Location = new System.Drawing.Point(4, 37);
            this.lvTypeContrats.MultiSelect = false;
            this.lvTypeContrats.Name = "lvTypeContrats";
            this.lvTypeContrats.Size = new System.Drawing.Size(440, 468);
            this.lvTypeContrats.TabIndex = 0;
            this.lvTypeContrats.UseCompatibleStateImageBehavior = false;
            this.lvTypeContrats.View = System.Windows.Forms.View.Details;
            this.lvTypeContrats.SelectedIndexChanged += new System.EventHandler(this.lvTypeContrats_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type Contrat";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Catégorie";
            this.columnHeader2.Width = 87;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Souscription";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Réservation";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Statut";
            this.columnHeader5.Width = 68;
            // 
            // cmsTypeContrat
            // 
            this.cmsTypeContrat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.déactiverToolStripMenuItem,
            this.réactiverToolStripMenuItem,
            this.supprimerToolStripMenuItem});
            this.cmsTypeContrat.Name = "cmsTypeContrat";
            this.cmsTypeContrat.Size = new System.Drawing.Size(130, 70);
            // 
            // déactiverToolStripMenuItem
            // 
            this.déactiverToolStripMenuItem.Name = "déactiverToolStripMenuItem";
            this.déactiverToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.déactiverToolStripMenuItem.Text = "Déactiver";
            this.déactiverToolStripMenuItem.Click += new System.EventHandler(this.déactiverToolStripMenuItem_Click);
            // 
            // réactiverToolStripMenuItem
            // 
            this.réactiverToolStripMenuItem.Name = "réactiverToolStripMenuItem";
            this.réactiverToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.réactiverToolStripMenuItem.Text = "Réactiver";
            this.réactiverToolStripMenuItem.Click += new System.EventHandler(this.réactiverToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(6, 8);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdSupprimerNA);
            this.splitContainer1.Panel2.Controls.Add(this.cmdEnregistrerNA);
            this.splitContainer1.Panel2.Controls.Add(this.cmdEditerNA);
            this.splitContainer1.Panel2.Controls.Add(this.cmdNewNA);
            this.splitContainer1.Panel2.Controls.Add(this.txtTauxNA);
            this.splitContainer1.Panel2.Controls.Add(this.txtNiveauNA);
            this.splitContainer1.Panel2.Controls.Add(this.txtOrdreNA);
            this.splitContainer1.Panel2.Controls.Add(this.txtTauxEncaissementTotal);
            this.splitContainer1.Panel2.Controls.Add(this.LvNiveauAvancements);
            this.splitContainer1.Size = new System.Drawing.Size(480, 500);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chkActif);
            this.panel1.Controls.Add(this.cmdSupprimer);
            this.panel1.Controls.Add(this.cmdEnregistrer);
            this.panel1.Controls.Add(this.cmdEditer);
            this.panel1.Controls.Add(this.cmdNouveau);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSeuilReservation);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSeuilSouscription);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtLibelle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbDepot);
            this.panel1.Controls.Add(this.rbReservation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 114);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 228;
            this.label8.Text = "Projet";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(84, 8);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(139, 21);
            this.cmbProjets.TabIndex = 227;
            this.cmbProjets.SelectedIndexChanged += new System.EventHandler(this.cmbProjets_SelectedIndexChanged);
            // 
            // pTypeConstruction
            // 
            this.pTypeConstruction.Controls.Add(this.rbImmeuble);
            this.pTypeConstruction.Controls.Add(this.rbIlot);
            this.pTypeConstruction.Location = new System.Drawing.Point(229, 6);
            this.pTypeConstruction.Name = "pTypeConstruction";
            this.pTypeConstruction.Size = new System.Drawing.Size(143, 25);
            this.pTypeConstruction.TabIndex = 226;
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
            // 
            // rbIlot
            // 
            this.rbIlot.AutoSize = true;
            this.rbIlot.Location = new System.Drawing.Point(94, 4);
            this.rbIlot.Name = "rbIlot";
            this.rbIlot.Size = new System.Drawing.Size(44, 17);
            this.rbIlot.TabIndex = 0;
            this.rbIlot.Text = "Villa";
            this.rbIlot.UseVisualStyleBackColor = true;
            this.rbIlot.CheckedChanged += new System.EventHandler(this.rbIlot_CheckedChanged);
            // 
            // chkActif
            // 
            this.chkActif.AutoSize = true;
            this.chkActif.Location = new System.Drawing.Point(81, 86);
            this.chkActif.Name = "chkActif";
            this.chkActif.Size = new System.Drawing.Size(47, 17);
            this.chkActif.TabIndex = 16;
            this.chkActif.Text = "Actif";
            this.chkActif.UseVisualStyleBackColor = true;
            // 
            // cmdSupprimer
            // 
            this.cmdSupprimer.Location = new System.Drawing.Point(379, 84);
            this.cmdSupprimer.Name = "cmdSupprimer";
            this.cmdSupprimer.Size = new System.Drawing.Size(75, 23);
            this.cmdSupprimer.TabIndex = 15;
            this.cmdSupprimer.Text = "Désactiver";
            this.cmdSupprimer.UseVisualStyleBackColor = true;
            this.cmdSupprimer.Click += new System.EventHandler(this.cmdSupprimer_Click);
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Location = new System.Drawing.Point(379, 59);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(75, 23);
            this.cmdEnregistrer.TabIndex = 14;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // cmdEditer
            // 
            this.cmdEditer.Location = new System.Drawing.Point(379, 34);
            this.cmdEditer.Name = "cmdEditer";
            this.cmdEditer.Size = new System.Drawing.Size(75, 23);
            this.cmdEditer.TabIndex = 13;
            this.cmdEditer.Text = "Editer";
            this.cmdEditer.UseVisualStyleBackColor = true;
            this.cmdEditer.Click += new System.EventHandler(this.cmdEditer_Click);
            // 
            // cmdNouveau
            // 
            this.cmdNouveau.Location = new System.Drawing.Point(379, 9);
            this.cmdNouveau.Name = "cmdNouveau";
            this.cmdNouveau.Size = new System.Drawing.Size(75, 23);
            this.cmdNouveau.TabIndex = 12;
            this.cmdNouveau.Text = "Nouveau";
            this.cmdNouveau.UseVisualStyleBackColor = true;
            this.cmdNouveau.Click += new System.EventHandler(this.cmdNouveau_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(347, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "%";
            // 
            // txtSeuilReservation
            // 
            this.txtSeuilReservation.Location = new System.Drawing.Point(301, 60);
            this.txtSeuilReservation.Name = "txtSeuilReservation";
            this.txtSeuilReservation.Size = new System.Drawing.Size(43, 20);
            this.txtSeuilReservation.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Réservation à ";
            // 
            // txtSeuilSouscription
            // 
            this.txtSeuilSouscription.Location = new System.Drawing.Point(81, 60);
            this.txtSeuilSouscription.Name = "txtSeuilSouscription";
            this.txtSeuilSouscription.Size = new System.Drawing.Size(43, 20);
            this.txtSeuilSouscription.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Souscription à";
            // 
            // txtLibelle
            // 
            this.txtLibelle.Location = new System.Drawing.Point(81, 34);
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(280, 20);
            this.txtLibelle.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type contrat";
            // 
            // rbDepot
            // 
            this.rbDepot.AutoSize = true;
            this.rbDepot.Location = new System.Drawing.Point(169, 11);
            this.rbDepot.Name = "rbDepot";
            this.rbDepot.Size = new System.Drawing.Size(54, 17);
            this.rbDepot.TabIndex = 2;
            this.rbDepot.Text = "Dépôt";
            this.rbDepot.UseVisualStyleBackColor = true;
            // 
            // rbReservation
            // 
            this.rbReservation.AutoSize = true;
            this.rbReservation.Checked = true;
            this.rbReservation.Location = new System.Drawing.Point(81, 11);
            this.rbReservation.Name = "rbReservation";
            this.rbReservation.Size = new System.Drawing.Size(82, 17);
            this.rbReservation.TabIndex = 1;
            this.rbReservation.TabStop = true;
            this.rbReservation.Text = "Réservation";
            this.rbReservation.UseVisualStyleBackColor = true;
            this.rbReservation.CheckedChanged += new System.EventHandler(this.rbReservation_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Catégorie";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(871, 527);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 26);
            this.button5.TabIndex = 16;
            this.button5.Text = "Fermer";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(197)))), ((int)(((byte)(190)))));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(5, 5);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            this.splitContainer2.Panel1.Controls.Add(this.lvTypeContrats);
            this.splitContainer2.Panel1.Controls.Add(this.cmbProjets);
            this.splitContainer2.Panel1.Controls.Add(this.pTypeConstruction);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(948, 516);
            this.splitContainer2.SplitterDistance = 450;
            this.splitContainer2.TabIndex = 17;
            // 
            // LvNiveauAvancements
            // 
            this.LvNiveauAvancements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader6,
            this.columnHeader7});
            this.LvNiveauAvancements.FullRowSelect = true;
            this.LvNiveauAvancements.HideSelection = false;
            this.LvNiveauAvancements.Location = new System.Drawing.Point(6, 27);
            this.LvNiveauAvancements.MultiSelect = false;
            this.LvNiveauAvancements.Name = "LvNiveauAvancements";
            this.LvNiveauAvancements.Size = new System.Drawing.Size(467, 302);
            this.LvNiveauAvancements.TabIndex = 0;
            this.LvNiveauAvancements.UseCompatibleStateImageBehavior = false;
            this.LvNiveauAvancements.View = System.Windows.Forms.View.Details;
            this.LvNiveauAvancements.SelectedIndexChanged += new System.EventHandler(this.LvNiveauAvancements_SelectedIndexChanged);
            // 
            // txtTauxEncaissementTotal
            // 
            this.txtTauxEncaissementTotal.Location = new System.Drawing.Point(413, 338);
            this.txtTauxEncaissementTotal.Name = "txtTauxEncaissementTotal";
            this.txtTauxEncaissementTotal.Size = new System.Drawing.Size(60, 20);
            this.txtTauxEncaissementTotal.TabIndex = 11;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Niveau";
            this.columnHeader6.Width = 346;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Taux";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Ordre";
            this.columnHeader8.Width = 50;
            // 
            // txtOrdreNA
            // 
            this.txtOrdreNA.Location = new System.Drawing.Point(6, 6);
            this.txtOrdreNA.Name = "txtOrdreNA";
            this.txtOrdreNA.Size = new System.Drawing.Size(51, 20);
            this.txtOrdreNA.TabIndex = 12;
            // 
            // txtNiveauNA
            // 
            this.txtNiveauNA.Location = new System.Drawing.Point(59, 6);
            this.txtNiveauNA.Name = "txtNiveauNA";
            this.txtNiveauNA.Size = new System.Drawing.Size(345, 20);
            this.txtNiveauNA.TabIndex = 13;
            // 
            // txtTauxNA
            // 
            this.txtTauxNA.Location = new System.Drawing.Point(405, 6);
            this.txtTauxNA.Name = "txtTauxNA";
            this.txtTauxNA.Size = new System.Drawing.Size(68, 20);
            this.txtTauxNA.TabIndex = 14;
            // 
            // cmdNewNA
            // 
            this.cmdNewNA.Location = new System.Drawing.Point(9, 335);
            this.cmdNewNA.Name = "cmdNewNA";
            this.cmdNewNA.Size = new System.Drawing.Size(75, 27);
            this.cmdNewNA.TabIndex = 16;
            this.cmdNewNA.Text = "Nouveau";
            this.cmdNewNA.UseVisualStyleBackColor = true;
            this.cmdNewNA.Click += new System.EventHandler(this.cmdNewNA_Click);
            // 
            // cmdEditerNA
            // 
            this.cmdEditerNA.Location = new System.Drawing.Point(90, 335);
            this.cmdEditerNA.Name = "cmdEditerNA";
            this.cmdEditerNA.Size = new System.Drawing.Size(75, 27);
            this.cmdEditerNA.TabIndex = 17;
            this.cmdEditerNA.Text = "Editer";
            this.cmdEditerNA.UseVisualStyleBackColor = true;
            this.cmdEditerNA.Click += new System.EventHandler(this.cmdEditerNA_Click);
            // 
            // cmdEnregistrerNA
            // 
            this.cmdEnregistrerNA.Location = new System.Drawing.Point(171, 335);
            this.cmdEnregistrerNA.Name = "cmdEnregistrerNA";
            this.cmdEnregistrerNA.Size = new System.Drawing.Size(75, 27);
            this.cmdEnregistrerNA.TabIndex = 18;
            this.cmdEnregistrerNA.Text = "Enregistrer";
            this.cmdEnregistrerNA.UseVisualStyleBackColor = true;
            this.cmdEnregistrerNA.Click += new System.EventHandler(this.cmdEnregistrerNA_Click);
            // 
            // cmdSupprimerNA
            // 
            this.cmdSupprimerNA.Location = new System.Drawing.Point(252, 337);
            this.cmdSupprimerNA.Name = "cmdSupprimerNA";
            this.cmdSupprimerNA.Size = new System.Drawing.Size(75, 23);
            this.cmdSupprimerNA.TabIndex = 19;
            this.cmdSupprimerNA.Text = "Supprimer";
            this.cmdSupprimerNA.UseVisualStyleBackColor = true;
            this.cmdSupprimerNA.Click += new System.EventHandler(this.cmdSupprimerNA_Click);
            // 
            // FrmTypeContrat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(958, 563);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.button5);
            this.Name = "FrmTypeContrat";
            this.Load += new System.EventHandler(this.FrmTypeContrat_Load);
            this.cmsTypeContrat.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pTypeConstruction.ResumeLayout(false);
            this.pTypeConstruction.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvTypeContrats;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.Button cmdEditer;
        private System.Windows.Forms.Button cmdNouveau;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSeuilReservation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSeuilSouscription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLibelle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbDepot;
        private System.Windows.Forms.RadioButton rbReservation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSupprimer;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.CheckBox chkActif;
        private System.Windows.Forms.ContextMenuStrip cmsTypeContrat;
        private System.Windows.Forms.ToolStripMenuItem déactiverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem réactiverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbProjets;
        private System.Windows.Forms.Panel pTypeConstruction;
        private System.Windows.Forms.RadioButton rbImmeuble;
        private System.Windows.Forms.RadioButton rbIlot;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtTauxEncaissementTotal;
        private System.Windows.Forms.ListView LvNiveauAvancements;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TextBox txtTauxNA;
        private System.Windows.Forms.TextBox txtNiveauNA;
        private System.Windows.Forms.TextBox txtOrdreNA;
        private System.Windows.Forms.Button cmdEnregistrerNA;
        private System.Windows.Forms.Button cmdEditerNA;
        private System.Windows.Forms.Button cmdNewNA;
        private System.Windows.Forms.Button cmdSupprimerNA;
    }
}