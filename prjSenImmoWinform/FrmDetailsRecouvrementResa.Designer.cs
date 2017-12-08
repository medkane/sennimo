namespace prjSenImmoWinform
{
    partial class FrmDetailsRecouvrementResa
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
            this.label15 = new System.Windows.Forms.Label();
            this.txtTotalFactureClient = new System.Windows.Forms.TextBox();
            this.txtTotalEncaisseClient = new System.Windows.Forms.TextBox();
            this.txtTotalRestantClient = new System.Windows.Forms.TextBox();
            this.lvAppelsDeFonds = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkCourrierEnvoye = new System.Windows.Forms.CheckBox();
            this.chkFactureGeneree = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTypeVillas = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrixDeVente = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTypeVilla = new System.Windows.Forms.TextBox();
            this.dgHistoriqueAppelDeFonds = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMontantREstantAppelDeFonds = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMontantEncaisséAppelDeFons = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMontantAppelDeFonds = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTauxNiveauAppelDeFonds = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNiveauAppelDeFond = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDateAppelDeFonds = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCompteTiers = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.cmdCourrierFacture = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdFacture = new System.Windows.Forms.Button();
            this.cmdImprimerFactureRelance = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvNotes = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsNote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cmdAjouterNote = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoriqueAppelDeFonds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cmsNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(173, 356);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 259;
            this.label15.Text = "TOTAUX";
            // 
            // txtTotalFactureClient
            // 
            this.txtTotalFactureClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalFactureClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalFactureClient.Location = new System.Drawing.Point(233, 352);
            this.txtTotalFactureClient.Name = "txtTotalFactureClient";
            this.txtTotalFactureClient.Size = new System.Drawing.Size(80, 20);
            this.txtTotalFactureClient.TabIndex = 258;
            this.txtTotalFactureClient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalEncaisseClient
            // 
            this.txtTotalEncaisseClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalEncaisseClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalEncaisseClient.Location = new System.Drawing.Point(316, 352);
            this.txtTotalEncaisseClient.Name = "txtTotalEncaisseClient";
            this.txtTotalEncaisseClient.Size = new System.Drawing.Size(80, 20);
            this.txtTotalEncaisseClient.TabIndex = 257;
            this.txtTotalEncaisseClient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalRestantClient
            // 
            this.txtTotalRestantClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalRestantClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRestantClient.Location = new System.Drawing.Point(402, 353);
            this.txtTotalRestantClient.Name = "txtTotalRestantClient";
            this.txtTotalRestantClient.Size = new System.Drawing.Size(80, 20);
            this.txtTotalRestantClient.TabIndex = 256;
            this.txtTotalRestantClient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lvAppelsDeFonds
            // 
            this.lvAppelsDeFonds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAppelsDeFonds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lvAppelsDeFonds.FullRowSelect = true;
            this.lvAppelsDeFonds.GridLines = true;
            this.lvAppelsDeFonds.Location = new System.Drawing.Point(3, 174);
            this.lvAppelsDeFonds.Name = "lvAppelsDeFonds";
            this.lvAppelsDeFonds.Size = new System.Drawing.Size(452, 156);
            this.lvAppelsDeFonds.TabIndex = 255;
            this.lvAppelsDeFonds.UseCompatibleStateImageBehavior = false;
            this.lvAppelsDeFonds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Appel de fond";
            this.columnHeader9.Width = 158;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Taux";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 46;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Montant";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader11.Width = 80;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Encaissé";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader12.Width = 80;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "A Recouvrer";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader13.Width = 80;
            // 
            // chkCourrierEnvoye
            // 
            this.chkCourrierEnvoye.AutoSize = true;
            this.chkCourrierEnvoye.Location = new System.Drawing.Point(13, 514);
            this.chkCourrierEnvoye.Name = "chkCourrierEnvoye";
            this.chkCourrierEnvoye.Size = new System.Drawing.Size(100, 17);
            this.chkCourrierEnvoye.TabIndex = 254;
            this.chkCourrierEnvoye.Text = "Courrier envoyé";
            this.chkCourrierEnvoye.UseVisualStyleBackColor = true;
            this.chkCourrierEnvoye.Visible = false;
            // 
            // chkFactureGeneree
            // 
            this.chkFactureGeneree.AutoSize = true;
            this.chkFactureGeneree.Location = new System.Drawing.Point(143, 121);
            this.chkFactureGeneree.Name = "chkFactureGeneree";
            this.chkFactureGeneree.Size = new System.Drawing.Size(104, 17);
            this.chkFactureGeneree.TabIndex = 253;
            this.chkFactureGeneree.Text = "Facture générée";
            this.chkFactureGeneree.UseVisualStyleBackColor = true;
            this.chkFactureGeneree.CheckedChanged += new System.EventHandler(this.chkFactureGeneree_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(49, 510);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Type villa";
            this.label5.Visible = false;
            // 
            // cmbTypeVillas
            // 
            this.cmbTypeVillas.Enabled = false;
            this.cmbTypeVillas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTypeVillas.FormattingEnabled = true;
            this.cmbTypeVillas.Location = new System.Drawing.Point(107, 510);
            this.cmbTypeVillas.Name = "cmbTypeVillas";
            this.cmbTypeVillas.Size = new System.Drawing.Size(135, 21);
            this.cmbTypeVillas.TabIndex = 16;
            this.cmbTypeVillas.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 250;
            this.label13.Text = "Prix de vente";
            // 
            // txtPrixDeVente
            // 
            this.txtPrixDeVente.Location = new System.Drawing.Point(75, 65);
            this.txtPrixDeVente.Name = "txtPrixDeVente";
            this.txtPrixDeVente.ReadOnly = true;
            this.txtPrixDeVente.Size = new System.Drawing.Size(96, 20);
            this.txtPrixDeVente.TabIndex = 249;
            this.txtPrixDeVente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(217, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 247;
            this.label10.Text = "Type villa";
            // 
            // txtTypeVilla
            // 
            this.txtTypeVilla.Location = new System.Drawing.Point(272, 39);
            this.txtTypeVilla.Name = "txtTypeVilla";
            this.txtTypeVilla.ReadOnly = true;
            this.txtTypeVilla.Size = new System.Drawing.Size(186, 20);
            this.txtTypeVilla.TabIndex = 246;
            // 
            // dgHistoriqueAppelDeFonds
            // 
            this.dgHistoriqueAppelDeFonds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgHistoriqueAppelDeFonds.BackgroundColor = System.Drawing.Color.White;
            this.dgHistoriqueAppelDeFonds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistoriqueAppelDeFonds.Enabled = false;
            this.dgHistoriqueAppelDeFonds.Location = new System.Drawing.Point(272, 506);
            this.dgHistoriqueAppelDeFonds.Name = "dgHistoriqueAppelDeFonds";
            this.dgHistoriqueAppelDeFonds.RowHeadersVisible = false;
            this.dgHistoriqueAppelDeFonds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistoriqueAppelDeFonds.Size = new System.Drawing.Size(139, 17);
            this.dgHistoriqueAppelDeFonds.TabIndex = 245;
            this.dgHistoriqueAppelDeFonds.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Restant";
            // 
            // txtMontantREstantAppelDeFonds
            // 
            this.txtMontantREstantAppelDeFonds.Location = new System.Drawing.Point(337, 82);
            this.txtMontantREstantAppelDeFonds.Name = "txtMontantREstantAppelDeFonds";
            this.txtMontantREstantAppelDeFonds.ReadOnly = true;
            this.txtMontantREstantAppelDeFonds.Size = new System.Drawing.Size(113, 20);
            this.txtMontantREstantAppelDeFonds.TabIndex = 19;
            this.txtMontantREstantAppelDeFonds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Encaissé";
            // 
            // txtMontantEncaisséAppelDeFons
            // 
            this.txtMontantEncaisséAppelDeFons.Location = new System.Drawing.Point(58, 82);
            this.txtMontantEncaisséAppelDeFons.Name = "txtMontantEncaisséAppelDeFons";
            this.txtMontantEncaisséAppelDeFons.ReadOnly = true;
            this.txtMontantEncaisséAppelDeFons.Size = new System.Drawing.Size(113, 20);
            this.txtMontantEncaisséAppelDeFons.TabIndex = 17;
            this.txtMontantEncaisséAppelDeFons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Montant appels de fond";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txtMontantAppelDeFonds
            // 
            this.txtMontantAppelDeFonds.Location = new System.Drawing.Point(337, 54);
            this.txtMontantAppelDeFonds.Name = "txtMontantAppelDeFonds";
            this.txtMontantAppelDeFonds.ReadOnly = true;
            this.txtMontantAppelDeFonds.Size = new System.Drawing.Size(113, 20);
            this.txtMontantAppelDeFonds.TabIndex = 15;
            this.txtMontantAppelDeFonds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Taux";
            // 
            // txtTauxNiveauAppelDeFonds
            // 
            this.txtTauxNiveauAppelDeFonds.Location = new System.Drawing.Point(337, 26);
            this.txtTauxNiveauAppelDeFonds.Name = "txtTauxNiveauAppelDeFonds";
            this.txtTauxNiveauAppelDeFonds.ReadOnly = true;
            this.txtTauxNiveauAppelDeFonds.Size = new System.Drawing.Size(48, 20);
            this.txtTauxNiveauAppelDeFonds.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Niveau";
            // 
            // txtNiveauAppelDeFond
            // 
            this.txtNiveauAppelDeFond.Location = new System.Drawing.Point(58, 26);
            this.txtNiveauAppelDeFond.Name = "txtNiveauAppelDeFond";
            this.txtNiveauAppelDeFond.ReadOnly = true;
            this.txtNiveauAppelDeFond.Size = new System.Drawing.Size(240, 20);
            this.txtNiveauAppelDeFond.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Lot";
            // 
            // txtLot
            // 
            this.txtLot.Location = new System.Drawing.Point(75, 39);
            this.txtLot.Name = "txtLot";
            this.txtLot.ReadOnly = true;
            this.txtLot.Size = new System.Drawing.Size(96, 20);
            this.txtLot.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Client";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(75, 13);
            this.txtClient.Name = "txtClient";
            this.txtClient.ReadOnly = true;
            this.txtClient.Size = new System.Drawing.Size(383, 20);
            this.txtClient.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Date";
            // 
            // txtDateAppelDeFonds
            // 
            this.txtDateAppelDeFonds.Location = new System.Drawing.Point(58, 54);
            this.txtDateAppelDeFonds.Name = "txtDateAppelDeFonds";
            this.txtDateAppelDeFonds.ReadOnly = true;
            this.txtDateAppelDeFonds.Size = new System.Drawing.Size(113, 20);
            this.txtDateAppelDeFonds.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(412, 517);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 28);
            this.button1.TabIndex = 249;
            this.button1.Text = "Fermer";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(6, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label15);
            this.splitContainer1.Panel2.Controls.Add(this.txtTotalFactureClient);
            this.splitContainer1.Panel2.Controls.Add(this.txtTotalEncaisseClient);
            this.splitContainer1.Panel2.Controls.Add(this.txtTotalRestantClient);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCourrierFacture);
            this.splitContainer1.Size = new System.Drawing.Size(490, 508);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.TabIndex = 250;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.Controls.Add(this.txtCompteTiers);
            this.panel1.Controls.Add(this.label50);
            this.panel1.Controls.Add(this.txtClient);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtLot);
            this.panel1.Controls.Add(this.txtPrixDeVente);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtTypeVilla);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(477, 110);
            this.panel1.TabIndex = 260;
            // 
            // txtCompteTiers
            // 
            this.txtCompteTiers.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCompteTiers.Location = new System.Drawing.Point(272, 65);
            this.txtCompteTiers.Name = "txtCompteTiers";
            this.txtCompteTiers.ReadOnly = true;
            this.txtCompteTiers.Size = new System.Drawing.Size(186, 20);
            this.txtCompteTiers.TabIndex = 293;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(204, 69);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(65, 13);
            this.label50.TabIndex = 292;
            this.label50.Text = "Compte tiers";
            // 
            // cmdCourrierFacture
            // 
            this.cmdCourrierFacture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCourrierFacture.Image = global::prjSenImmoWinform.Properties.Resources.printer_16;
            this.cmdCourrierFacture.Location = new System.Drawing.Point(657, 131);
            this.cmdCourrierFacture.Name = "cmdCourrierFacture";
            this.cmdCourrierFacture.Size = new System.Drawing.Size(79, 33);
            this.cmdCourrierFacture.TabIndex = 252;
            this.cmdCourrierFacture.Text = "Courrier";
            this.cmdCourrierFacture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCourrierFacture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCourrierFacture.UseVisualStyleBackColor = true;
            this.cmdCourrierFacture.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtDateAppelDeFonds);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtMontantREstantAppelDeFonds);
            this.panel2.Controls.Add(this.lvAppelsDeFonds);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtNiveauAppelDeFond);
            this.panel2.Controls.Add(this.txtMontantEncaisséAppelDeFons);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmdFacture);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtTauxNiveauAppelDeFonds);
            this.panel2.Controls.Add(this.txtMontantAppelDeFonds);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.chkFactureGeneree);
            this.panel2.Location = new System.Drawing.Point(4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(463, 336);
            this.panel2.TabIndex = 251;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label4.Location = new System.Drawing.Point(-2, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(466, 18);
            this.label4.TabIndex = 258;
            this.label4.Text = "Historique des appels de fond ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(466, 18);
            this.label3.TabIndex = 257;
            this.label3.Text = "Dernier appel de fond ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdFacture
            // 
            this.cmdFacture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFacture.Image = global::prjSenImmoWinform.Properties.Resources.printer_16;
            this.cmdFacture.Location = new System.Drawing.Point(6, 116);
            this.cmdFacture.Name = "cmdFacture";
            this.cmdFacture.Size = new System.Drawing.Size(131, 29);
            this.cmdFacture.TabIndex = 251;
            this.cmdFacture.Text = "Editer la facture";
            this.cmdFacture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdFacture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdFacture.UseVisualStyleBackColor = true;
            this.cmdFacture.Click += new System.EventHandler(this.cmdFacture_Click);
            // 
            // cmdImprimerFactureRelance
            // 
            this.cmdImprimerFactureRelance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdImprimerFactureRelance.Image = global::prjSenImmoWinform.Properties.Resources.printer_16;
            this.cmdImprimerFactureRelance.Location = new System.Drawing.Point(6, 517);
            this.cmdImprimerFactureRelance.Name = "cmdImprimerFactureRelance";
            this.cmdImprimerFactureRelance.Size = new System.Drawing.Size(103, 28);
            this.cmdImprimerFactureRelance.TabIndex = 243;
            this.cmdImprimerFactureRelance.Text = "Relance";
            this.cmdImprimerFactureRelance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdImprimerFactureRelance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdImprimerFactureRelance.UseVisualStyleBackColor = true;
            this.cmdImprimerFactureRelance.Click += new System.EventHandler(this.cmdImprimerFactureRelance_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 135);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(478, 372);
            this.tabControl1.TabIndex = 294;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(470, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Niveau recouvrement";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvNotes);
            this.tabPage2.Controls.Add(this.txtNote);
            this.tabPage2.Controls.Add(this.cmdAjouterNote);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(470, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Prise de notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvNotes
            // 
            this.lvNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lvNotes.ContextMenuStrip = this.cmsNote;
            this.lvNotes.FullRowSelect = true;
            this.lvNotes.HideSelection = false;
            this.lvNotes.Location = new System.Drawing.Point(3, 6);
            this.lvNotes.Name = "lvNotes";
            this.lvNotes.Size = new System.Drawing.Size(461, 280);
            this.lvNotes.TabIndex = 290;
            this.lvNotes.UseCompatibleStateImageBehavior = false;
            this.lvNotes.View = System.Windows.Forms.View.Details;
            this.lvNotes.SelectedIndexChanged += new System.EventHandler(this.lvNotes_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date";
            this.columnHeader6.Width = 76;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Note";
            this.columnHeader7.Width = 664;
            // 
            // cmsNote
            // 
            this.cmsNote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifierToolStripMenuItem,
            this.supprimerToolStripMenuItem});
            this.cmsNote.Name = "cmsNote";
            this.cmsNote.Size = new System.Drawing.Size(130, 48);
            // 
            // modifierToolStripMenuItem
            // 
            this.modifierToolStripMenuItem.Name = "modifierToolStripMenuItem";
            this.modifierToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.modifierToolStripMenuItem.Text = "Modifier";
            this.modifierToolStripMenuItem.Click += new System.EventHandler(this.modifierToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.BackColor = System.Drawing.SystemColors.Info;
            this.txtNote.Location = new System.Drawing.Point(4, 290);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(412, 49);
            this.txtNote.TabIndex = 289;
            // 
            // cmdAjouterNote
            // 
            this.cmdAjouterNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAjouterNote.Image = global::prjSenImmoWinform.Properties.Resources.sticky_notes_24;
            this.cmdAjouterNote.Location = new System.Drawing.Point(419, 289);
            this.cmdAjouterNote.Name = "cmdAjouterNote";
            this.cmdAjouterNote.Size = new System.Drawing.Size(47, 51);
            this.cmdAjouterNote.TabIndex = 288;
            this.cmdAjouterNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAjouterNote.UseVisualStyleBackColor = true;
            this.cmdAjouterNote.Click += new System.EventHandler(this.cmdAjouterNote_Click);
            // 
            // FrmDetailsRecouvrementResa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(497, 550);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgHistoriqueAppelDeFonds);
            this.Controls.Add(this.cmdImprimerFactureRelance);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTypeVillas);
            this.Controls.Add(this.chkCourrierEnvoye);
            this.Name = "FrmDetailsRecouvrementResa";
            this.Text = "FrmDetailsRecouvrementResa";
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoriqueAppelDeFonds)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.cmsNote.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTotalFactureClient;
        private System.Windows.Forms.TextBox txtTotalEncaisseClient;
        private System.Windows.Forms.TextBox txtTotalRestantClient;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.CheckBox chkCourrierEnvoye;
        private System.Windows.Forms.CheckBox chkFactureGeneree;
        private System.Windows.Forms.Button cmdCourrierFacture;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdFacture;
        private System.Windows.Forms.ComboBox cmbTypeVillas;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrixDeVente;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTypeVilla;
        private System.Windows.Forms.DataGridView dgHistoriqueAppelDeFonds;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMontantREstantAppelDeFonds;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMontantEncaisséAppelDeFons;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMontantAppelDeFonds;
        private System.Windows.Forms.Button cmdImprimerFactureRelance;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTauxNiveauAppelDeFonds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNiveauAppelDeFond;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLot;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDateAppelDeFonds;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtCompteTiers;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvAppelsDeFonds;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvNotes;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button cmdAjouterNote;
        private System.Windows.Forms.ContextMenuStrip cmsNote;
        private System.Windows.Forms.ToolStripMenuItem modifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
    }
}