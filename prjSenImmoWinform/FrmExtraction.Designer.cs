namespace prjSenImmoWinform
{
    partial class FrmExtraction
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Prénom");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Nom");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Statut");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Téléphone");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Mail");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Origine");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Source");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Nationalité");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Adresse");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Pays");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Profession");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Date entrée");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Date création");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Date dépôt");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("Date réservation");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("Type contrat");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("Type Villa");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("Numéro Lot");
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("Prix de vente");
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("Remise");
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem("Date livraison");
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem("Montant versé");
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem("Commercial");
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem("Chef d\'équipe");
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem("Nature apporteur");
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem("Apporteur d\'affaires");
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem("Taux apporteur");
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem("Coopérative");
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem("Taux coopérative");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbAKYS = new System.Windows.Forms.RadioButton();
            this.rbKERRIA = new System.Windows.Forms.RadioButton();
            this.rbClients = new System.Windows.Forms.RadioButton();
            this.rbProspects = new System.Windows.Forms.RadioButton();
            this.pDates = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDebut = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdRAZ = new System.Windows.Forms.Button();
            this.cmdExtraire = new System.Windows.Forms.Button();
            this.pOperateur = new System.Windows.Forms.Panel();
            this.cmbOperateur = new System.Windows.Forms.ComboBox();
            this.cmbValeur = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbCriteres = new System.Windows.Forms.ComboBox();
            this.cmdAjouterCritere = new System.Windows.Forms.Button();
            this.lvCriteres = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsCriteres = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.lvFields = new System.Windows.Forms.ListView();
            this.Champs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.lvResult = new System.Windows.Forms.ListView();
            this.cmdExporter = new System.Windows.Forms.Button();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pDates.SuspendLayout();
            this.pOperateur.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cmsCriteres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(6, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(985, 518);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 178);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.rbClients);
            this.panel4.Controls.Add(this.rbProspects);
            this.panel4.Controls.Add(this.pDates);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cmdRAZ);
            this.panel4.Controls.Add(this.cmdExtraire);
            this.panel4.Controls.Add(this.pOperateur);
            this.panel4.Controls.Add(this.cmbValeur);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.cmdAjouterCritere);
            this.panel4.Controls.Add(this.lvCriteres);
            this.panel4.Location = new System.Drawing.Point(5, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(963, 164);
            this.panel4.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbAKYS);
            this.panel3.Controls.Add(this.rbKERRIA);
            this.panel3.Location = new System.Drawing.Point(3, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 22);
            this.panel3.TabIndex = 23;
            // 
            // rbAKYS
            // 
            this.rbAKYS.AutoSize = true;
            this.rbAKYS.Checked = true;
            this.rbAKYS.Location = new System.Drawing.Point(3, 2);
            this.rbAKYS.Name = "rbAKYS";
            this.rbAKYS.Size = new System.Drawing.Size(53, 17);
            this.rbAKYS.TabIndex = 21;
            this.rbAKYS.TabStop = true;
            this.rbAKYS.Text = "AKYS";
            this.rbAKYS.UseVisualStyleBackColor = true;
            // 
            // rbKERRIA
            // 
            this.rbKERRIA.AutoSize = true;
            this.rbKERRIA.Location = new System.Drawing.Point(88, 2);
            this.rbKERRIA.Name = "rbKERRIA";
            this.rbKERRIA.Size = new System.Drawing.Size(65, 17);
            this.rbKERRIA.TabIndex = 22;
            this.rbKERRIA.Text = "KERRIA";
            this.rbKERRIA.UseVisualStyleBackColor = true;
            // 
            // rbClients
            // 
            this.rbClients.AutoSize = true;
            this.rbClients.Checked = true;
            this.rbClients.Location = new System.Drawing.Point(287, 28);
            this.rbClients.Name = "rbClients";
            this.rbClients.Size = new System.Drawing.Size(56, 17);
            this.rbClients.TabIndex = 20;
            this.rbClients.TabStop = true;
            this.rbClients.Text = "Clients";
            this.rbClients.UseVisualStyleBackColor = true;
            // 
            // rbProspects
            // 
            this.rbProspects.AutoSize = true;
            this.rbProspects.Location = new System.Drawing.Point(214, 28);
            this.rbProspects.Name = "rbProspects";
            this.rbProspects.Size = new System.Drawing.Size(67, 17);
            this.rbProspects.TabIndex = 19;
            this.rbProspects.Text = "Prospect";
            this.rbProspects.UseVisualStyleBackColor = true;
            this.rbProspects.CheckedChanged += new System.EventHandler(this.rbProspects_CheckedChanged);
            // 
            // pDates
            // 
            this.pDates.Controls.Add(this.label2);
            this.pDates.Controls.Add(this.dtpDebut);
            this.pDates.Controls.Add(this.dtpFin);
            this.pDates.Controls.Add(this.label1);
            this.pDates.Location = new System.Drawing.Point(640, 49);
            this.pDates.Name = "pDates";
            this.pDates.Size = new System.Drawing.Size(315, 28);
            this.pDates.TabIndex = 13;
            this.pDates.Visible = false;
            this.pDates.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "entre le";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDebut
            // 
            this.dtpDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDebut.Location = new System.Drawing.Point(50, 4);
            this.dtpDebut.Name = "dtpDebut";
            this.dtpDebut.Size = new System.Drawing.Size(111, 20);
            this.dtpDebut.TabIndex = 11;
            this.dtpDebut.ValueChanged += new System.EventHandler(this.dtpDebut_ValueChanged);
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(200, 4);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(111, 20);
            this.dtpFin.TabIndex = 9;
            this.dtpFin.ValueChanged += new System.EventHandler(this.dtpFin_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(167, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "et le";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(-1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(961, 23);
            this.label5.TabIndex = 18;
            this.label5.Text = "Critères d\'extraction";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdRAZ
            // 
            this.cmdRAZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRAZ.Location = new System.Drawing.Point(869, 115);
            this.cmdRAZ.Name = "cmdRAZ";
            this.cmdRAZ.Size = new System.Drawing.Size(87, 27);
            this.cmdRAZ.TabIndex = 2;
            this.cmdRAZ.Text = "Réinitialiser";
            this.cmdRAZ.UseVisualStyleBackColor = true;
            this.cmdRAZ.Click += new System.EventHandler(this.cmdRAZ_Click);
            // 
            // cmdExtraire
            // 
            this.cmdExtraire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExtraire.Enabled = false;
            this.cmdExtraire.Location = new System.Drawing.Point(869, 83);
            this.cmdExtraire.Name = "cmdExtraire";
            this.cmdExtraire.Size = new System.Drawing.Size(87, 26);
            this.cmdExtraire.TabIndex = 0;
            this.cmdExtraire.Text = "Extraire";
            this.cmdExtraire.UseVisualStyleBackColor = true;
            this.cmdExtraire.Click += new System.EventHandler(this.cmdExtraire_Click);
            // 
            // pOperateur
            // 
            this.pOperateur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pOperateur.Controls.Add(this.cmbOperateur);
            this.pOperateur.Location = new System.Drawing.Point(234, 50);
            this.pOperateur.Name = "pOperateur";
            this.pOperateur.Size = new System.Drawing.Size(143, 24);
            this.pOperateur.TabIndex = 17;
            // 
            // cmbOperateur
            // 
            this.cmbOperateur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperateur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOperateur.FormattingEnabled = true;
            this.cmbOperateur.Items.AddRange(new object[] {
            "égal",
            "est différent de ",
            "supérieur ou égal",
            "inférieur ou égal"});
            this.cmbOperateur.Location = new System.Drawing.Point(3, 1);
            this.cmbOperateur.Name = "cmbOperateur";
            this.cmbOperateur.Size = new System.Drawing.Size(135, 21);
            this.cmbOperateur.TabIndex = 12;
            // 
            // cmbValeur
            // 
            this.cmbValeur.FormattingEnabled = true;
            this.cmbValeur.Location = new System.Drawing.Point(385, 51);
            this.cmbValeur.Name = "cmbValeur";
            this.cmbValeur.Size = new System.Drawing.Size(140, 21);
            this.cmbValeur.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbCriteres);
            this.panel2.Location = new System.Drawing.Point(3, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 24);
            this.panel2.TabIndex = 14;
            // 
            // cmbCriteres
            // 
            this.cmbCriteres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCriteres.FormattingEnabled = true;
            this.cmbCriteres.Items.AddRange(new object[] {
            "Apporteur d\'affaires",
            "Chef d\'équipe",
            "Commercial",
            "Coopérative",
            "Date création",
            "Date dépôt",
            "Date entrée",
            "Date livraison",
            "Date réservation",
            "Nature apporteur",
            "Origine",
            "Pays",
            "Prix de vente",
            "Remise",
            "Source",
            "Type contrat",
            "Type villa"});
            this.cmbCriteres.Location = new System.Drawing.Point(3, 0);
            this.cmbCriteres.Name = "cmbCriteres";
            this.cmbCriteres.Size = new System.Drawing.Size(217, 21);
            this.cmbCriteres.Sorted = true;
            this.cmbCriteres.TabIndex = 5;
            this.cmbCriteres.SelectedIndexChanged += new System.EventHandler(this.cmbCriteres_SelectedIndexChanged);
            // 
            // cmdAjouterCritere
            // 
            this.cmdAjouterCritere.Location = new System.Drawing.Point(554, 48);
            this.cmdAjouterCritere.Name = "cmdAjouterCritere";
            this.cmdAjouterCritere.Size = new System.Drawing.Size(80, 25);
            this.cmdAjouterCritere.TabIndex = 15;
            this.cmdAjouterCritere.Text = "Ajouter";
            this.cmdAjouterCritere.UseVisualStyleBackColor = true;
            this.cmdAjouterCritere.Click += new System.EventHandler(this.cmdAjouterCritere_Click);
            // 
            // lvCriteres
            // 
            this.lvCriteres.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCriteres.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvCriteres.ContextMenuStrip = this.cmsCriteres;
            this.lvCriteres.FullRowSelect = true;
            this.lvCriteres.Location = new System.Drawing.Point(2, 79);
            this.lvCriteres.MultiSelect = false;
            this.lvCriteres.Name = "lvCriteres";
            this.lvCriteres.Size = new System.Drawing.Size(861, 78);
            this.lvCriteres.TabIndex = 16;
            this.lvCriteres.UseCompatibleStateImageBehavior = false;
            this.lvCriteres.View = System.Windows.Forms.View.Details;
            this.lvCriteres.SelectedIndexChanged += new System.EventHandler(this.lvCriteres_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Critère";
            this.columnHeader1.Width = 219;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Opérateur";
            this.columnHeader2.Width = 166;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Valeur";
            this.columnHeader3.Width = 268;
            // 
            // cmsCriteres
            // 
            this.cmsCriteres.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem});
            this.cmsCriteres.Name = "cmsCriteres";
            this.cmsCriteres.Size = new System.Drawing.Size(130, 26);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.lvFields);
            this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtTotal);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.lvResult);
            this.splitContainer2.Size = new System.Drawing.Size(985, 328);
            this.splitContainer2.SplitterDistance = 175;
            this.splitContainer2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Champs à afficher";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvFields
            // 
            this.lvFields.AllowDrop = true;
            this.lvFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Champs});
            this.lvFields.FullRowSelect = true;
            this.lvFields.GridLines = true;
            this.lvFields.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24,
            listViewItem25,
            listViewItem26,
            listViewItem27,
            listViewItem28,
            listViewItem29});
            this.lvFields.Location = new System.Drawing.Point(4, 4);
            this.lvFields.Name = "lvFields";
            this.lvFields.Size = new System.Drawing.Size(165, 317);
            this.lvFields.TabIndex = 1;
            this.lvFields.UseCompatibleStateImageBehavior = false;
            this.lvFields.View = System.Windows.Forms.View.Details;
            this.lvFields.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView2_ItemDrag);
            // 
            // Champs
            // 
            this.Champs.Text = "Champs";
            this.Champs.Width = 144;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(5, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(795, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Résultats";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvResult
            // 
            this.lvResult.AllowDrop = true;
            this.lvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResult.FullRowSelect = true;
            this.lvResult.HideSelection = false;
            this.lvResult.Location = new System.Drawing.Point(5, 29);
            this.lvResult.Name = "lvResult";
            this.lvResult.Size = new System.Drawing.Size(795, 270);
            this.lvResult.TabIndex = 0;
            this.lvResult.UseCompatibleStateImageBehavior = false;
            this.lvResult.View = System.Windows.Forms.View.Details;
            this.lvResult.SelectedIndexChanged += new System.EventHandler(this.lvResult_SelectedIndexChanged);
            this.lvResult.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.lvResult.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            // 
            // cmdExporter
            // 
            this.cmdExporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExporter.Enabled = false;
            this.cmdExporter.Location = new System.Drawing.Point(834, 530);
            this.cmdExporter.Name = "cmdExporter";
            this.cmdExporter.Size = new System.Drawing.Size(75, 28);
            this.cmdExporter.TabIndex = 1;
            this.cmdExporter.Text = "Exporter";
            this.cmdExporter.UseVisualStyleBackColor = true;
            this.cmdExporter.Click += new System.EventHandler(this.cmdExporter_Click);
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(915, 530);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(75, 28);
            this.cmdFermer.TabIndex = 2;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(668, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.Location = new System.Drawing.Point(700, 302);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 5;
            // 
            // FrmExtraction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(995, 561);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.cmdExporter);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmExtraction";
            this.Text = "FrmExtraction";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pDates.ResumeLayout(false);
            this.pOperateur.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.cmsCriteres.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button cmdExporter;
        private System.Windows.Forms.Button cmdExtraire;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lvResult;
        private System.Windows.Forms.ListView lvFields;
        private System.Windows.Forms.ColumnHeader Champs;
        private System.Windows.Forms.Button cmdRAZ;
        private System.Windows.Forms.ComboBox cmbValeur;
        private System.Windows.Forms.ComboBox cmbCriteres;
        private System.Windows.Forms.ComboBox cmbOperateur;
        private System.Windows.Forms.DateTimePicker dtpDebut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Panel pDates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvCriteres;
        private System.Windows.Forms.Button cmdAjouterCritere;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip cmsCriteres;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.Panel pOperateur;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbClients;
        private System.Windows.Forms.RadioButton rbProspects;
        private System.Windows.Forms.RadioButton rbKERRIA;
        private System.Windows.Forms.RadioButton rbAKYS;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label6;
    }
}