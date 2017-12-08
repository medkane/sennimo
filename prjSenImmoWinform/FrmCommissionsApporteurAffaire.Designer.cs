namespace prjSenImmoWinform
{
    partial class FrmCommissionsApporteurAffaire
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcCompteClient = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgCompteApporteurAffaires = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgFactureCommissionsEchues = new System.Windows.Forms.DataGridView();
            this.clID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMontant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clBRS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClMontantNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPayer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgEncaissementsGlobals = new System.Windows.Forms.DataGridView();
            this.txtMontantPaiement = new System.Windows.Forms.TextBox();
            this.cmbModePaiement = new System.Windows.Forms.ComboBox();
            this.txtCommentairePaiement = new System.Windows.Forms.TextBox();
            this.dtpDateVersement = new System.Windows.Forms.DateTimePicker();
            this.txtReferencePaiement = new System.Windows.Forms.TextBox();
            this.cmdEnregistrerPaiement = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmdListerContrat = new System.Windows.Forms.Button();
            this.cmdDossierClient = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lbMontantTotalCommissionsRestantes = new System.Windows.Forms.Label();
            this.txtMontantTotalCommissionsRestantes = new System.Windows.Forms.TextBox();
            this.nudTauxCommission = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lbMontantTotalcommissionsVersees = new System.Windows.Forms.Label();
            this.txtMontantTotalcommissionsVersees = new System.Windows.Forms.TextBox();
            this.lbMontantTotalCommissions = new System.Windows.Forms.Label();
            this.txtMontantTotalCommissions = new System.Windows.Forms.TextBox();
            this.txtTelephoneFixe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelephoneMobile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.cmbApporteurAffaires = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.tcCompteClient.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompteApporteurAffaires)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFactureCommissionsEchues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncaissementsGlobals)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTauxCommission)).BeginInit();
            this.SuspendLayout();
            // 
            // tcCompteClient
            // 
            this.tcCompteClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcCompteClient.Controls.Add(this.tabPage1);
            this.tcCompteClient.Controls.Add(this.tabPage2);
            this.tcCompteClient.Location = new System.Drawing.Point(5, 159);
            this.tcCompteClient.Name = "tcCompteClient";
            this.tcCompteClient.SelectedIndex = 0;
            this.tcCompteClient.Size = new System.Drawing.Size(1073, 362);
            this.tcCompteClient.TabIndex = 58;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgCompteApporteurAffaires);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1065, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Consultation compte";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgCompteApporteurAffaires
            // 
            this.dgCompteApporteurAffaires.AllowUserToAddRows = false;
            this.dgCompteApporteurAffaires.AllowUserToDeleteRows = false;
            this.dgCompteApporteurAffaires.AllowUserToOrderColumns = true;
            this.dgCompteApporteurAffaires.AllowUserToResizeColumns = false;
            this.dgCompteApporteurAffaires.AllowUserToResizeRows = false;
            this.dgCompteApporteurAffaires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCompteApporteurAffaires.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(230)))), ((int)(((byte)(224)))));
            this.dgCompteApporteurAffaires.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCompteApporteurAffaires.Location = new System.Drawing.Point(5, 5);
            this.dgCompteApporteurAffaires.MultiSelect = false;
            this.dgCompteApporteurAffaires.Name = "dgCompteApporteurAffaires";
            this.dgCompteApporteurAffaires.RowHeadersVisible = false;
            this.dgCompteApporteurAffaires.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCompteApporteurAffaires.Size = new System.Drawing.Size(1055, 325);
            this.dgCompteApporteurAffaires.TabIndex = 72;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgFactureCommissionsEchues);
            this.tabPage2.Controls.Add(this.dgEncaissementsGlobals);
            this.tabPage2.Controls.Add(this.txtMontantPaiement);
            this.tabPage2.Controls.Add(this.cmbModePaiement);
            this.tabPage2.Controls.Add(this.txtCommentairePaiement);
            this.tabPage2.Controls.Add(this.dtpDateVersement);
            this.tabPage2.Controls.Add(this.txtReferencePaiement);
            this.tabPage2.Controls.Add(this.cmdEnregistrerPaiement);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1065, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Paiement commissions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgFactureCommissionsEchues
            // 
            this.dgFactureCommissionsEchues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFactureCommissionsEchues.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(230)))), ((int)(((byte)(224)))));
            this.dgFactureCommissionsEchues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgFactureCommissionsEchues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFactureCommissionsEchues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clID,
            this.clDate,
            this.clMontant,
            this.clBRS,
            this.ClMontantNet,
            this.clPayer});
            this.dgFactureCommissionsEchues.Location = new System.Drawing.Point(7, 6);
            this.dgFactureCommissionsEchues.Name = "dgFactureCommissionsEchues";
            this.dgFactureCommissionsEchues.RowHeadersVisible = false;
            this.dgFactureCommissionsEchues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFactureCommissionsEchues.Size = new System.Drawing.Size(996, 196);
            this.dgFactureCommissionsEchues.TabIndex = 231;
            this.dgFactureCommissionsEchues.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFactureCommissionsEchues_CellContentClick);
            this.dgFactureCommissionsEchues.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFactureCommissionsEchues_CellValueChanged);
            // 
            // clID
            // 
            this.clID.HeaderText = "ID";
            this.clID.Name = "clID";
            this.clID.Visible = false;
            this.clID.Width = 5;
            // 
            // clDate
            // 
            this.clDate.HeaderText = "Date facture";
            this.clDate.Name = "clDate";
            this.clDate.Width = 90;
            // 
            // clMontant
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clMontant.DefaultCellStyle = dataGridViewCellStyle7;
            this.clMontant.HeaderText = "Montant Brut";
            this.clMontant.Name = "clMontant";
            // 
            // clBRS
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clBRS.DefaultCellStyle = dataGridViewCellStyle8;
            this.clBRS.HeaderText = "BRS";
            this.clBRS.Name = "clBRS";
            // 
            // ClMontantNet
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ClMontantNet.DefaultCellStyle = dataGridViewCellStyle9;
            this.ClMontantNet.HeaderText = "Montant Net";
            this.ClMontantNet.Name = "ClMontantNet";
            // 
            // clPayer
            // 
            this.clPayer.HeaderText = "Payer";
            this.clPayer.Name = "clPayer";
            this.clPayer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clPayer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clPayer.Width = 50;
            // 
            // dgEncaissementsGlobals
            // 
            this.dgEncaissementsGlobals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEncaissementsGlobals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncaissementsGlobals.Location = new System.Drawing.Point(7, 232);
            this.dgEncaissementsGlobals.MultiSelect = false;
            this.dgEncaissementsGlobals.Name = "dgEncaissementsGlobals";
            this.dgEncaissementsGlobals.RowHeadersVisible = false;
            this.dgEncaissementsGlobals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEncaissementsGlobals.Size = new System.Drawing.Size(996, 98);
            this.dgEncaissementsGlobals.TabIndex = 109;
            // 
            // txtMontantPaiement
            // 
            this.txtMontantPaiement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontantPaiement.Location = new System.Drawing.Point(86, 208);
            this.txtMontantPaiement.Name = "txtMontantPaiement";
            this.txtMontantPaiement.ReadOnly = true;
            this.txtMontantPaiement.Size = new System.Drawing.Size(80, 20);
            this.txtMontantPaiement.TabIndex = 105;
            this.txtMontantPaiement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbModePaiement
            // 
            this.cmbModePaiement.BackColor = System.Drawing.SystemColors.Window;
            this.cmbModePaiement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModePaiement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbModePaiement.FormattingEnabled = true;
            this.cmbModePaiement.Location = new System.Drawing.Point(168, 208);
            this.cmbModePaiement.Name = "cmbModePaiement";
            this.cmbModePaiement.Size = new System.Drawing.Size(79, 21);
            this.cmbModePaiement.TabIndex = 106;
            // 
            // txtCommentairePaiement
            // 
            this.txtCommentairePaiement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommentairePaiement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentairePaiement.Location = new System.Drawing.Point(502, 208);
            this.txtCommentairePaiement.Name = "txtCommentairePaiement";
            this.txtCommentairePaiement.Size = new System.Drawing.Size(410, 20);
            this.txtCommentairePaiement.TabIndex = 108;
            this.txtCommentairePaiement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommentairePaiement_KeyDown);
            // 
            // dtpDateVersement
            // 
            this.dtpDateVersement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateVersement.Location = new System.Drawing.Point(7, 208);
            this.dtpDateVersement.Name = "dtpDateVersement";
            this.dtpDateVersement.Size = new System.Drawing.Size(78, 20);
            this.dtpDateVersement.TabIndex = 104;
            // 
            // txtReferencePaiement
            // 
            this.txtReferencePaiement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferencePaiement.Location = new System.Drawing.Point(250, 208);
            this.txtReferencePaiement.Name = "txtReferencePaiement";
            this.txtReferencePaiement.Size = new System.Drawing.Size(250, 20);
            this.txtReferencePaiement.TabIndex = 107;
            // 
            // cmdEnregistrerPaiement
            // 
            this.cmdEnregistrerPaiement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnregistrerPaiement.Image = global::prjSenImmoWinform.Properties.Resources._079_16;
            this.cmdEnregistrerPaiement.Location = new System.Drawing.Point(918, 205);
            this.cmdEnregistrerPaiement.Name = "cmdEnregistrerPaiement";
            this.cmdEnregistrerPaiement.Size = new System.Drawing.Size(85, 27);
            this.cmdEnregistrerPaiement.TabIndex = 224;
            this.cmdEnregistrerPaiement.Text = "Enregistrer";
            this.cmdEnregistrerPaiement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEnregistrerPaiement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdEnregistrerPaiement.UseVisualStyleBackColor = true;
            this.cmdEnregistrerPaiement.Click += new System.EventHandler(this.cmdEnregistrerPaiement_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Location = new System.Drawing.Point(6, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1072, 147);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(205)))), ((int)(((byte)(193)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.cmdListerContrat);
            this.panel2.Controls.Add(this.cmdDossierClient);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.lbMontantTotalCommissionsRestantes);
            this.panel2.Controls.Add(this.txtMontantTotalCommissionsRestantes);
            this.panel2.Controls.Add(this.nudTauxCommission);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbMontantTotalcommissionsVersees);
            this.panel2.Controls.Add(this.txtMontantTotalcommissionsVersees);
            this.panel2.Controls.Add(this.lbMontantTotalCommissions);
            this.panel2.Controls.Add(this.txtMontantTotalCommissions);
            this.panel2.Controls.Add(this.txtTelephoneFixe);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtTelephoneMobile);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtAdresse);
            this.panel2.Controls.Add(this.cmbApporteurAffaires);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Location = new System.Drawing.Point(9, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1054, 121);
            this.panel2.TabIndex = 142;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.Location = new System.Drawing.Point(53, 87);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(328, 20);
            this.textBox1.TabIndex = 230;
            // 
            // cmdListerContrat
            // 
            this.cmdListerContrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdListerContrat.Location = new System.Drawing.Point(933, 21);
            this.cmdListerContrat.Name = "cmdListerContrat";
            this.cmdListerContrat.Size = new System.Drawing.Size(114, 35);
            this.cmdListerContrat.TabIndex = 65;
            this.cmdListerContrat.Text = "Liste des contrats";
            this.cmdListerContrat.UseVisualStyleBackColor = true;
            this.cmdListerContrat.Visible = false;
            // 
            // cmdDossierClient
            // 
            this.cmdDossierClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDossierClient.Location = new System.Drawing.Point(933, 72);
            this.cmdDossierClient.Name = "cmdDossierClient";
            this.cmdDossierClient.Size = new System.Drawing.Size(114, 35);
            this.cmdDossierClient.TabIndex = 89;
            this.cmdDossierClient.Text = "Dossier client";
            this.cmdDossierClient.UseVisualStyleBackColor = true;
            this.cmdDossierClient.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 90);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 229;
            this.label12.Text = "Email";
            // 
            // lbMontantTotalCommissionsRestantes
            // 
            this.lbMontantTotalCommissionsRestantes.AutoSize = true;
            this.lbMontantTotalCommissionsRestantes.Location = new System.Drawing.Point(437, 90);
            this.lbMontantTotalCommissionsRestantes.Name = "lbMontantTotalCommissionsRestantes";
            this.lbMontantTotalCommissionsRestantes.Size = new System.Drawing.Size(81, 13);
            this.lbMontantTotalCommissionsRestantes.TabIndex = 227;
            this.lbMontantTotalCommissionsRestantes.Text = "Montant restant";
            // 
            // txtMontantTotalCommissionsRestantes
            // 
            this.txtMontantTotalCommissionsRestantes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMontantTotalCommissionsRestantes.Location = new System.Drawing.Point(519, 87);
            this.txtMontantTotalCommissionsRestantes.Name = "txtMontantTotalCommissionsRestantes";
            this.txtMontantTotalCommissionsRestantes.ReadOnly = true;
            this.txtMontantTotalCommissionsRestantes.Size = new System.Drawing.Size(92, 20);
            this.txtMontantTotalCommissionsRestantes.TabIndex = 228;
            this.txtMontantTotalCommissionsRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudTauxCommission
            // 
            this.nudTauxCommission.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nudTauxCommission.Enabled = false;
            this.nudTauxCommission.Location = new System.Drawing.Point(519, 11);
            this.nudTauxCommission.Name = "nudTauxCommission";
            this.nudTauxCommission.ReadOnly = true;
            this.nudTauxCommission.Size = new System.Drawing.Size(45, 20);
            this.nudTauxCommission.TabIndex = 222;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(490, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 140;
            this.label6.Text = "Taux";
            // 
            // lbMontantTotalcommissionsVersees
            // 
            this.lbMontantTotalcommissionsVersees.AutoSize = true;
            this.lbMontantTotalcommissionsVersees.Location = new System.Drawing.Point(411, 64);
            this.lbMontantTotalcommissionsVersees.Name = "lbMontantTotalcommissionsVersees";
            this.lbMontantTotalcommissionsVersees.Size = new System.Drawing.Size(107, 13);
            this.lbMontantTotalcommissionsVersees.TabIndex = 225;
            this.lbMontantTotalcommissionsVersees.Text = "Commissions versées";
            // 
            // txtMontantTotalcommissionsVersees
            // 
            this.txtMontantTotalcommissionsVersees.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMontantTotalcommissionsVersees.Location = new System.Drawing.Point(519, 61);
            this.txtMontantTotalcommissionsVersees.Name = "txtMontantTotalcommissionsVersees";
            this.txtMontantTotalcommissionsVersees.ReadOnly = true;
            this.txtMontantTotalcommissionsVersees.Size = new System.Drawing.Size(92, 20);
            this.txtMontantTotalcommissionsVersees.TabIndex = 226;
            this.txtMontantTotalcommissionsVersees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbMontantTotalCommissions
            // 
            this.lbMontantTotalCommissions.AutoSize = true;
            this.lbMontantTotalCommissions.Location = new System.Drawing.Point(405, 39);
            this.lbMontantTotalCommissions.Name = "lbMontantTotalCommissions";
            this.lbMontantTotalCommissions.Size = new System.Drawing.Size(113, 13);
            this.lbMontantTotalCommissions.TabIndex = 223;
            this.lbMontantTotalCommissions.Text = "Total des commissions";
            // 
            // txtMontantTotalCommissions
            // 
            this.txtMontantTotalCommissions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMontantTotalCommissions.Location = new System.Drawing.Point(519, 36);
            this.txtMontantTotalCommissions.Name = "txtMontantTotalCommissions";
            this.txtMontantTotalCommissions.ReadOnly = true;
            this.txtMontantTotalCommissions.Size = new System.Drawing.Size(92, 20);
            this.txtMontantTotalCommissions.TabIndex = 224;
            this.txtMontantTotalCommissions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTelephoneFixe
            // 
            this.txtTelephoneFixe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTelephoneFixe.Location = new System.Drawing.Point(242, 61);
            this.txtTelephoneFixe.Name = "txtTelephoneFixe";
            this.txtTelephoneFixe.ReadOnly = true;
            this.txtTelephoneFixe.Size = new System.Drawing.Size(139, 20);
            this.txtTelephoneFixe.TabIndex = 221;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 220;
            this.label3.Text = "Fixe";
            // 
            // txtTelephoneMobile
            // 
            this.txtTelephoneMobile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTelephoneMobile.Location = new System.Drawing.Point(53, 61);
            this.txtTelephoneMobile.Name = "txtTelephoneMobile";
            this.txtTelephoneMobile.ReadOnly = true;
            this.txtTelephoneMobile.Size = new System.Drawing.Size(139, 20);
            this.txtTelephoneMobile.TabIndex = 139;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 138;
            this.label5.Text = "Mobile";
            // 
            // txtAdresse
            // 
            this.txtAdresse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAdresse.Location = new System.Drawing.Point(53, 35);
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.ReadOnly = true;
            this.txtAdresse.Size = new System.Drawing.Size(328, 20);
            this.txtAdresse.TabIndex = 129;
            // 
            // cmbApporteurAffaires
            // 
            this.cmbApporteurAffaires.FormattingEnabled = true;
            this.cmbApporteurAffaires.Location = new System.Drawing.Point(53, 10);
            this.cmbApporteurAffaires.Name = "cmbApporteurAffaires";
            this.cmbApporteurAffaires.Size = new System.Drawing.Size(328, 21);
            this.cmbApporteurAffaires.TabIndex = 67;
            this.cmbApporteurAffaires.SelectedIndexChanged += new System.EventHandler(this.cmbApporteurAffaires_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 128;
            this.label8.Text = "Adresse";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(11, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 46;
            this.label20.Text = "Client";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(22, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(0, 13);
            this.label25.TabIndex = 40;
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(988, 527);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(90, 35);
            this.cmdFermer.TabIndex = 145;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // FrmCommissionsApporteurAffaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 568);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.tcCompteClient);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmCommissionsApporteurAffaire";
            this.Text = "Gestion des commissions des Apporteurs d\'Affaires";
            this.Load += new System.EventHandler(this.FrmCommissionsApporteurAffaire_Load);
            this.tcCompteClient.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCompteApporteurAffaires)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFactureCommissionsEchues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncaissementsGlobals)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTauxCommission)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCompteClient;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgCompteApporteurAffaires;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgEncaissementsGlobals;
        private System.Windows.Forms.TextBox txtMontantPaiement;
        private System.Windows.Forms.ComboBox cmbModePaiement;
        private System.Windows.Forms.TextBox txtCommentairePaiement;
        private System.Windows.Forms.DateTimePicker dtpDateVersement;
        private System.Windows.Forms.TextBox txtReferencePaiement;
        private System.Windows.Forms.Button cmdEnregistrerPaiement;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdDossierClient;
        private System.Windows.Forms.ComboBox cmbApporteurAffaires;
        private System.Windows.Forms.Button cmdListerContrat;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbMontantTotalCommissionsRestantes;
        private System.Windows.Forms.TextBox txtMontantTotalCommissionsRestantes;
        private System.Windows.Forms.NumericUpDown nudTauxCommission;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbMontantTotalcommissionsVersees;
        private System.Windows.Forms.TextBox txtMontantTotalcommissionsVersees;
        private System.Windows.Forms.Label lbMontantTotalCommissions;
        private System.Windows.Forms.TextBox txtMontantTotalCommissions;
        private System.Windows.Forms.TextBox txtTelephoneFixe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTelephoneMobile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgFactureCommissionsEchues;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMontant;
        private System.Windows.Forms.DataGridViewTextBoxColumn clBRS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClMontantNet;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clPayer;
        private System.Windows.Forms.Button cmdFermer;
    }
}