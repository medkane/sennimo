namespace prjSenImmoWinform
{
    partial class FrmListeClients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListeClients));
            this.dgClients = new System.Windows.Forms.DataGridView();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.cmdRechercherClients = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbOptionResa = new System.Windows.Forms.RadioButton();
            this.rbToutTypeOption = new System.Windows.Forms.RadioButton();
            this.rbOptionDepot = new System.Windows.Forms.RadioButton();
            this.label36 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pTypeConstruction = new System.Windows.Forms.Panel();
            this.rbToutTypeConstruction = new System.Windows.Forms.RadioButton();
            this.rbConstructionVilla = new System.Windows.Forms.RadioButton();
            this.rbConstructionAppartement = new System.Windows.Forms.RadioButton();
            this.pSource = new System.Windows.Forms.Panel();
            this.cmbOrigines = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.rbOrigineDesk = new System.Windows.Forms.RadioButton();
            this.rbOriginePerso = new System.Windows.Forms.RadioButton();
            this.rbToutOrigine = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumeroDossier = new System.Windows.Forms.TextBox();
            this.chkAvenant = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbEnCours = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rbSoldes = new System.Windows.Forms.RadioButton();
            this.txtNbProspects = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.chkResilie = new System.Windows.Forms.CheckBox();
            this.cmbCommercial = new System.Windows.Forms.ComboBox();
            this.chkCommercial = new System.Windows.Forms.CheckBox();
            this.lvClients = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCompteTiers = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumeroLot = new System.Windows.Forms.TextBox();
            this.txtTelMobile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgClients)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pTypeConstruction.SuspendLayout();
            this.pSource.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgClients
            // 
            this.dgClients.AllowUserToAddRows = false;
            this.dgClients.AllowUserToDeleteRows = false;
            this.dgClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgClients.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(230)))), ((int)(((byte)(224)))));
            this.dgClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClients.Location = new System.Drawing.Point(258, 455);
            this.dgClients.Name = "dgClients";
            this.dgClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgClients.Size = new System.Drawing.Size(731, 19);
            this.dgClients.TabIndex = 0;
            this.dgClients.Visible = false;
            this.dgClients.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgClients_CellContentClick);
            this.dgClients.SelectionChanged += new System.EventHandler(this.dgClients_SelectionChanged);
            this.dgClients.DoubleClick += new System.EventHandler(this.dgClients_DoubleClick);
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdFermer.Location = new System.Drawing.Point(1108, 584);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(78, 33);
            this.cmdFermer.TabIndex = 204;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdNouveauClient_Click);
            // 
            // cmdRechercherClients
            // 
            this.cmdRechercherClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRechercherClients.Image = global::prjSenImmoWinform.Properties.Resources.common_search_lookup_glyph_16;
            this.cmdRechercherClients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRechercherClients.Location = new System.Drawing.Point(1033, 5);
            this.cmdRechercherClients.Name = "cmdRechercherClients";
            this.cmdRechercherClients.Size = new System.Drawing.Size(131, 27);
            this.cmdRechercherClients.TabIndex = 206;
            this.cmdRechercherClients.Text = "Rechercher (Fn+F1)";
            this.cmdRechercherClients.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRechercherClients.UseVisualStyleBackColor = true;
            this.cmdRechercherClients.Click += new System.EventHandler(this.cmdRechercherClients_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtNumeroDossier);
            this.panel1.Controls.Add(this.chkAvenant);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtNbProspects);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.chkResilie);
            this.panel1.Controls.Add(this.cmbCommercial);
            this.panel1.Controls.Add(this.chkCommercial);
            this.panel1.Controls.Add(this.lvClients);
            this.panel1.Controls.Add(this.dgClients);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1172, 515);
            this.panel1.TabIndex = 207;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label38);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.pTypeConstruction);
            this.panel4.Controls.Add(this.pSource);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.label37);
            this.panel4.Controls.Add(this.cmbProjets);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1162, 40);
            this.panel4.TabIndex = 254;
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(648, 9);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(47, 17);
            this.label38.TabIndex = 252;
            this.label38.Text = "Origine";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbOptionResa);
            this.panel5.Controls.Add(this.rbToutTypeOption);
            this.panel5.Controls.Add(this.rbOptionDepot);
            this.panel5.Location = new System.Drawing.Point(458, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(183, 25);
            this.panel5.TabIndex = 235;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // rbOptionResa
            // 
            this.rbOptionResa.AutoSize = true;
            this.rbOptionResa.Location = new System.Drawing.Point(118, 5);
            this.rbOptionResa.Name = "rbOptionResa";
            this.rbOptionResa.Size = new System.Drawing.Size(50, 17);
            this.rbOptionResa.TabIndex = 229;
            this.rbOptionResa.Text = "Résa";
            this.rbOptionResa.UseVisualStyleBackColor = true;
            this.rbOptionResa.CheckedChanged += new System.EventHandler(this.rbOptionResa_CheckedChanged);
            // 
            // rbToutTypeOption
            // 
            this.rbToutTypeOption.AutoSize = true;
            this.rbToutTypeOption.Checked = true;
            this.rbToutTypeOption.Location = new System.Drawing.Point(3, 5);
            this.rbToutTypeOption.Name = "rbToutTypeOption";
            this.rbToutTypeOption.Size = new System.Drawing.Size(49, 17);
            this.rbToutTypeOption.TabIndex = 227;
            this.rbToutTypeOption.TabStop = true;
            this.rbToutTypeOption.Text = "Tous";
            this.rbToutTypeOption.UseVisualStyleBackColor = true;
            this.rbToutTypeOption.CheckedChanged += new System.EventHandler(this.rbToutTypeOption_CheckedChanged);
            // 
            // rbOptionDepot
            // 
            this.rbOptionDepot.AutoSize = true;
            this.rbOptionDepot.Location = new System.Drawing.Point(58, 5);
            this.rbOptionDepot.Name = "rbOptionDepot";
            this.rbOptionDepot.Size = new System.Drawing.Size(54, 17);
            this.rbOptionDepot.TabIndex = 228;
            this.rbOptionDepot.Text = "Dépôt";
            this.rbOptionDepot.UseVisualStyleBackColor = true;
            this.rbOptionDepot.CheckedChanged += new System.EventHandler(this.rbOptionDepot_CheckedChanged);
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(396, 8);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(61, 17);
            this.label36.TabIndex = 251;
            this.label36.Text = "Catégorie";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(642, -1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(4, 33);
            this.groupBox3.TabIndex = 236;
            this.groupBox3.TabStop = false;
            // 
            // pTypeConstruction
            // 
            this.pTypeConstruction.Controls.Add(this.rbToutTypeConstruction);
            this.pTypeConstruction.Controls.Add(this.rbConstructionVilla);
            this.pTypeConstruction.Controls.Add(this.rbConstructionAppartement);
            this.pTypeConstruction.Location = new System.Drawing.Point(188, 4);
            this.pTypeConstruction.Name = "pTypeConstruction";
            this.pTypeConstruction.Size = new System.Drawing.Size(200, 25);
            this.pTypeConstruction.TabIndex = 250;
            // 
            // rbToutTypeConstruction
            // 
            this.rbToutTypeConstruction.AutoSize = true;
            this.rbToutTypeConstruction.Checked = true;
            this.rbToutTypeConstruction.Location = new System.Drawing.Point(4, 5);
            this.rbToutTypeConstruction.Name = "rbToutTypeConstruction";
            this.rbToutTypeConstruction.Size = new System.Drawing.Size(49, 17);
            this.rbToutTypeConstruction.TabIndex = 229;
            this.rbToutTypeConstruction.TabStop = true;
            this.rbToutTypeConstruction.Text = "Tous";
            this.rbToutTypeConstruction.UseVisualStyleBackColor = true;
            // 
            // rbConstructionVilla
            // 
            this.rbConstructionVilla.AutoSize = true;
            this.rbConstructionVilla.Location = new System.Drawing.Point(64, 5);
            this.rbConstructionVilla.Name = "rbConstructionVilla";
            this.rbConstructionVilla.Size = new System.Drawing.Size(44, 17);
            this.rbConstructionVilla.TabIndex = 227;
            this.rbConstructionVilla.Text = "Villa";
            this.rbConstructionVilla.UseVisualStyleBackColor = true;
            // 
            // rbConstructionAppartement
            // 
            this.rbConstructionAppartement.AutoSize = true;
            this.rbConstructionAppartement.Location = new System.Drawing.Point(114, 5);
            this.rbConstructionAppartement.Name = "rbConstructionAppartement";
            this.rbConstructionAppartement.Size = new System.Drawing.Size(85, 17);
            this.rbConstructionAppartement.TabIndex = 228;
            this.rbConstructionAppartement.Text = "Appartement";
            this.rbConstructionAppartement.UseVisualStyleBackColor = true;
            // 
            // pSource
            // 
            this.pSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pSource.Controls.Add(this.cmbOrigines);
            this.pSource.Controls.Add(this.label34);
            this.pSource.Location = new System.Drawing.Point(896, 5);
            this.pSource.Name = "pSource";
            this.pSource.Size = new System.Drawing.Size(259, 26);
            this.pSource.TabIndex = 243;
            this.pSource.Visible = false;
            // 
            // cmbOrigines
            // 
            this.cmbOrigines.BackColor = System.Drawing.SystemColors.Info;
            this.cmbOrigines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOrigines.FormattingEnabled = true;
            this.cmbOrigines.Location = new System.Drawing.Point(50, 3);
            this.cmbOrigines.Name = "cmbOrigines";
            this.cmbOrigines.Size = new System.Drawing.Size(205, 21);
            this.cmbOrigines.TabIndex = 234;
            this.cmbOrigines.SelectedIndexChanged += new System.EventHandler(this.cmbOrigines_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(4, 8);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 13);
            this.label34.TabIndex = 235;
            this.label34.Text = "Source";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.rbOrigineDesk);
            this.panel8.Controls.Add(this.rbOriginePerso);
            this.panel8.Controls.Add(this.rbToutOrigine);
            this.panel8.Location = new System.Drawing.Point(697, 5);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(163, 25);
            this.panel8.TabIndex = 241;
            // 
            // rbOrigineDesk
            // 
            this.rbOrigineDesk.AutoSize = true;
            this.rbOrigineDesk.Location = new System.Drawing.Point(112, 5);
            this.rbOrigineDesk.Name = "rbOrigineDesk";
            this.rbOrigineDesk.Size = new System.Drawing.Size(50, 17);
            this.rbOrigineDesk.TabIndex = 231;
            this.rbOrigineDesk.Text = "Desk";
            this.rbOrigineDesk.UseVisualStyleBackColor = true;
            this.rbOrigineDesk.CheckedChanged += new System.EventHandler(this.rbOrigineDesk_CheckedChanged);
            // 
            // rbOriginePerso
            // 
            this.rbOriginePerso.AutoSize = true;
            this.rbOriginePerso.Location = new System.Drawing.Point(59, 5);
            this.rbOriginePerso.Name = "rbOriginePerso";
            this.rbOriginePerso.Size = new System.Drawing.Size(52, 17);
            this.rbOriginePerso.TabIndex = 230;
            this.rbOriginePerso.Text = "Perso";
            this.rbOriginePerso.UseVisualStyleBackColor = true;
            this.rbOriginePerso.CheckedChanged += new System.EventHandler(this.rbOriginePerso_CheckedChanged);
            // 
            // rbToutOrigine
            // 
            this.rbToutOrigine.AutoSize = true;
            this.rbToutOrigine.Checked = true;
            this.rbToutOrigine.Location = new System.Drawing.Point(4, 5);
            this.rbToutOrigine.Name = "rbToutOrigine";
            this.rbToutOrigine.Size = new System.Drawing.Size(49, 17);
            this.rbToutOrigine.TabIndex = 229;
            this.rbToutOrigine.TabStop = true;
            this.rbToutOrigine.Text = "Tous";
            this.rbToutOrigine.UseVisualStyleBackColor = true;
            this.rbToutOrigine.CheckedChanged += new System.EventHandler(this.rbToutOrigine_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(390, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(4, 33);
            this.groupBox1.TabIndex = 242;
            this.groupBox1.TabStop = false;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(4, 8);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(41, 17);
            this.label37.TabIndex = 230;
            this.label37.Text = "Projet";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(49, 6);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(135, 21);
            this.cmbProjets.TabIndex = 229;
            this.cmbProjets.SelectedIndexChanged += new System.EventHandler(this.cmbProjets_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(291, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 249;
            this.label7.Text = "Numero Dossier";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtNumeroDossier
            // 
            this.txtNumeroDossier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNumeroDossier.Location = new System.Drawing.Point(375, 485);
            this.txtNumeroDossier.Name = "txtNumeroDossier";
            this.txtNumeroDossier.Size = new System.Drawing.Size(116, 20);
            this.txtNumeroDossier.TabIndex = 248;
            this.txtNumeroDossier.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // chkAvenant
            // 
            this.chkAvenant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAvenant.AutoSize = true;
            this.chkAvenant.Location = new System.Drawing.Point(692, 487);
            this.chkAvenant.Name = "chkAvenant";
            this.chkAvenant.Size = new System.Drawing.Size(71, 17);
            this.chkAvenant.TabIndex = 247;
            this.chkAvenant.Text = "Avenants";
            this.chkAvenant.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.Controls.Add(this.rbEnCours);
            this.panel3.Controls.Add(this.radioButton2);
            this.panel3.Controls.Add(this.rbSoldes);
            this.panel3.Location = new System.Drawing.Point(498, 480);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 25);
            this.panel3.TabIndex = 245;
            // 
            // rbEnCours
            // 
            this.rbEnCours.AutoSize = true;
            this.rbEnCours.Location = new System.Drawing.Point(112, 5);
            this.rbEnCours.Name = "rbEnCours";
            this.rbEnCours.Size = new System.Drawing.Size(68, 17);
            this.rbEnCours.TabIndex = 229;
            this.rbEnCours.Text = "En Cours";
            this.rbEnCours.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(3, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(49, 17);
            this.radioButton2.TabIndex = 227;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Tous";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rbSoldes
            // 
            this.rbSoldes.AutoSize = true;
            this.rbSoldes.Location = new System.Drawing.Point(52, 5);
            this.rbSoldes.Name = "rbSoldes";
            this.rbSoldes.Size = new System.Drawing.Size(57, 17);
            this.rbSoldes.TabIndex = 228;
            this.rbSoldes.Text = "Soldés";
            this.rbSoldes.UseVisualStyleBackColor = true;
            // 
            // txtNbProspects
            // 
            this.txtNbProspects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbProspects.Location = new System.Drawing.Point(1105, 484);
            this.txtNbProspects.Name = "txtNbProspects";
            this.txtNbProspects.Size = new System.Drawing.Size(58, 20);
            this.txtNbProspects.TabIndex = 239;
            this.txtNbProspects.TextChanged += new System.EventHandler(this.txtNbProspects_TextChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label19.Location = new System.Drawing.Point(1010, 487);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(92, 13);
            this.label19.TabIndex = 240;
            this.label19.Text = "Nombre de clients";
            // 
            // chkResilie
            // 
            this.chkResilie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkResilie.AutoSize = true;
            this.chkResilie.Location = new System.Drawing.Point(764, 487);
            this.chkResilie.Name = "chkResilie";
            this.chkResilie.Size = new System.Drawing.Size(62, 17);
            this.chkResilie.TabIndex = 211;
            this.chkResilie.Text = "Résiliés";
            this.chkResilie.UseVisualStyleBackColor = true;
            this.chkResilie.CheckedChanged += new System.EventHandler(this.chkResilie_CheckedChanged);
            // 
            // cmbCommercial
            // 
            this.cmbCommercial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCommercial.FormattingEnabled = true;
            this.cmbCommercial.Location = new System.Drawing.Point(86, 484);
            this.cmbCommercial.Name = "cmbCommercial";
            this.cmbCommercial.Size = new System.Drawing.Size(160, 21);
            this.cmbCommercial.TabIndex = 237;
            this.cmbCommercial.Visible = false;
            this.cmbCommercial.SelectedIndexChanged += new System.EventHandler(this.cmbCommercial_SelectedIndexChanged);
            // 
            // chkCommercial
            // 
            this.chkCommercial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCommercial.AutoSize = true;
            this.chkCommercial.Location = new System.Drawing.Point(3, 486);
            this.chkCommercial.Name = "chkCommercial";
            this.chkCommercial.Size = new System.Drawing.Size(80, 17);
            this.chkCommercial.TabIndex = 238;
            this.chkCommercial.Text = "Commercial";
            this.chkCommercial.UseVisualStyleBackColor = true;
            this.chkCommercial.CheckedChanged += new System.EventHandler(this.chkCommercial_CheckedChanged);
            // 
            // lvClients
            // 
            this.lvClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader4,
            this.columnHeader11,
            this.columnHeader5,
            this.columnHeader16,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader1});
            this.lvClients.FullRowSelect = true;
            this.lvClients.HideSelection = false;
            this.lvClients.Location = new System.Drawing.Point(5, 47);
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(1160, 429);
            this.lvClients.SmallImageList = this.imageList1;
            this.lvClients.TabIndex = 226;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.View = System.Windows.Forms.View.Details;
            this.lvClients.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvClients_ColumnClick);
            this.lvClients.SelectedIndexChanged += new System.EventHandler(this.lvClients_SelectedIndexChanged);
            this.lvClients.DoubleClick += new System.EventHandler(this.lvClients_DoubleClick);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Numéro";
            this.columnHeader7.Width = 56;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Prénom et Nom";
            this.columnHeader8.Width = 190;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Téléphone";
            this.columnHeader9.Width = 121;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "E-mail";
            this.columnHeader10.Width = 145;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Adresse";
            this.columnHeader4.Width = 210;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Souscrit le";
            this.columnHeader11.Width = 88;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Nb Contrats";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Contrat";
            this.columnHeader16.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Lot";
            this.columnHeader3.Width = 55;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Commercial";
            this.columnHeader1.Width = 120;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dollars.ico");
            this.imageList1.Images.SetKeyName(1, "home_w.ico");
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Beige;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtCompteTiers);
            this.panel2.Controls.Add(this.label35);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtNumeroLot);
            this.panel2.Controls.Add(this.txtTelMobile);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtNom);
            this.panel2.Controls.Add(this.txtPrenom);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmdRechercherClients);
            this.panel2.Location = new System.Drawing.Point(5, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1171, 38);
            this.panel2.TabIndex = 208;
            // 
            // txtCompteTiers
            // 
            this.txtCompteTiers.Location = new System.Drawing.Point(849, 8);
            this.txtCompteTiers.Name = "txtCompteTiers";
            this.txtCompteTiers.Size = new System.Drawing.Size(130, 20);
            this.txtCompteTiers.TabIndex = 221;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(782, 12);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(69, 13);
            this.label35.TabIndex = 220;
            this.label35.Text = "Compte Tiers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(678, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 219;
            this.label4.Text = "Lot";
            // 
            // txtNumeroLot
            // 
            this.txtNumeroLot.Location = new System.Drawing.Point(700, 8);
            this.txtNumeroLot.Name = "txtNumeroLot";
            this.txtNumeroLot.Size = new System.Drawing.Size(79, 20);
            this.txtNumeroLot.TabIndex = 218;
            this.txtNumeroLot.TextChanged += new System.EventHandler(this.txtNumeroLot_TextChanged);
            // 
            // txtTelMobile
            // 
            this.txtTelMobile.Location = new System.Drawing.Point(391, 8);
            this.txtTelMobile.Name = "txtTelMobile";
            this.txtTelMobile.Size = new System.Drawing.Size(100, 20);
            this.txtTelMobile.TabIndex = 217;
            this.txtTelMobile.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtTelMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelMobile_KeyDown);
            this.txtTelMobile.Validated += new System.EventHandler(this.txtTelMobile_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(497, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 215;
            this.label2.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(529, 8);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(146, 20);
            this.txtEmail.TabIndex = 214;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            this.txtEmail.Validated += new System.EventHandler(this.txtEmail_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 212;
            this.label3.Text = "Tél Mobile";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 210;
            this.label6.Text = "Nom";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(229, 8);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(102, 20);
            this.txtNom.TabIndex = 209;
            this.txtNom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNom_KeyDown);
            this.txtNom.Validated += new System.EventHandler(this.txtNom_Validated);
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(42, 8);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(154, 20);
            this.txtPrenom.TabIndex = 208;
            this.txtPrenom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrenom_KeyDown);
            this.txtPrenom.Validated += new System.EventHandler(this.txtPrenom_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 207;
            this.label5.Text = "Prénom";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer1.Location = new System.Drawing.Point(7, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 569);
            this.splitContainer1.SplitterDistance = 44;
            this.splitContainer1.TabIndex = 209;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(7, 584);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 28);
            this.button1.TabIndex = 210;
            this.button1.Text = "Extraction";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmListeClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1195, 622);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdFermer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmListeClients";
            this.Text = "Liste des clients";
            this.Load += new System.EventHandler(this.FrmListeClients_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmListeClients_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgClients)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pTypeConstruction.ResumeLayout(false);
            this.pTypeConstruction.PerformLayout();
            this.pSource.ResumeLayout(false);
            this.pSource.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgClients;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Button cmdRechercherClients;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkResilie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTelMobile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvClients;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbOptionResa;
        private System.Windows.Forms.RadioButton rbToutTypeOption;
        private System.Windows.Forms.RadioButton rbOptionDepot;
        private System.Windows.Forms.ComboBox cmbCommercial;
        private System.Windows.Forms.CheckBox chkCommercial;
        private System.Windows.Forms.TextBox txtNbProspects;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton rbOrigineDesk;
        private System.Windows.Forms.RadioButton rbOriginePerso;
        private System.Windows.Forms.RadioButton rbToutOrigine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel pSource;
        private System.Windows.Forms.ComboBox cmbOrigines;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbEnCours;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rbSoldes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumeroLot;
        private System.Windows.Forms.TextBox txtCompteTiers;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox chkAvenant;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumeroDossier;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmbProjets;
        private System.Windows.Forms.Panel pTypeConstruction;
        private System.Windows.Forms.RadioButton rbToutTypeConstruction;
        private System.Windows.Forms.RadioButton rbConstructionVilla;
        private System.Windows.Forms.RadioButton rbConstructionAppartement;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
    }
}