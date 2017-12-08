namespace prjSenImmoWinform
{
    partial class FrmRemboursement
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
            this.tcCompteClient = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbDebitTotal = new System.Windows.Forms.Label();
            this.lbSoldeTotal = new System.Windows.Forms.Label();
            this.lbCreditTotal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgCompteRemboursements = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgEncaissementsGlobals = new System.Windows.Forms.DataGridView();
            this.txtMontantRemboursement = new System.Windows.Forms.TextBox();
            this.cmbModePaiement = new System.Windows.Forms.ComboBox();
            this.txtCommentaireRemboursement = new System.Windows.Forms.TextBox();
            this.dtpDateRemboursement = new System.Windows.Forms.DateTimePicker();
            this.txtReferencePaiement = new System.Windows.Forms.TextBox();
            this.cmdEnregistrerPaiement = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTypeClient = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumeroFixe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroMobile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLieuNaissance = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDateNaissance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmdListerContrat = new System.Windows.Forms.Button();
            this.cmdDossierClient = new System.Windows.Forms.Button();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.tcCompteClient.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompteRemboursements)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncaissementsGlobals)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcCompteClient
            // 
            this.tcCompteClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcCompteClient.Controls.Add(this.tabPage1);
            this.tcCompteClient.Controls.Add(this.tabPage2);
            this.tcCompteClient.Location = new System.Drawing.Point(4, 149);
            this.tcCompteClient.Name = "tcCompteClient";
            this.tcCompteClient.SelectedIndex = 0;
            this.tcCompteClient.Size = new System.Drawing.Size(1092, 440);
            this.tcCompteClient.TabIndex = 143;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbDebitTotal);
            this.tabPage1.Controls.Add(this.lbSoldeTotal);
            this.tabPage1.Controls.Add(this.lbCreditTotal);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dgCompteRemboursements);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1084, 414);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Consultation compte remboursement";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbDebitTotal
            // 
            this.lbDebitTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDebitTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDebitTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDebitTotal.Location = new System.Drawing.Point(747, 384);
            this.lbDebitTotal.Name = "lbDebitTotal";
            this.lbDebitTotal.Size = new System.Drawing.Size(110, 17);
            this.lbDebitTotal.TabIndex = 78;
            this.lbDebitTotal.Text = "7690000";
            this.lbDebitTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSoldeTotal
            // 
            this.lbSoldeTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSoldeTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSoldeTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSoldeTotal.Location = new System.Drawing.Point(969, 384);
            this.lbSoldeTotal.Name = "lbSoldeTotal";
            this.lbSoldeTotal.Size = new System.Drawing.Size(110, 17);
            this.lbSoldeTotal.TabIndex = 77;
            this.lbSoldeTotal.Text = "2690000";
            this.lbSoldeTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbCreditTotal
            // 
            this.lbCreditTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCreditTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCreditTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCreditTotal.Location = new System.Drawing.Point(858, 384);
            this.lbCreditTotal.Name = "lbCreditTotal";
            this.lbCreditTotal.Size = new System.Drawing.Size(110, 17);
            this.lbCreditTotal.TabIndex = 76;
            this.lbCreditTotal.Text = "5000000";
            this.lbCreditTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(747, 368);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 17);
            this.label9.TabIndex = 75;
            this.label9.Text = "Débit Total";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(969, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 17);
            this.label5.TabIndex = 74;
            this.label5.Text = "Solde";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(858, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 73;
            this.label4.Text = "Crédit Total";
            // 
            // dgCompteRemboursements
            // 
            this.dgCompteRemboursements.AllowUserToAddRows = false;
            this.dgCompteRemboursements.AllowUserToDeleteRows = false;
            this.dgCompteRemboursements.AllowUserToOrderColumns = true;
            this.dgCompteRemboursements.AllowUserToResizeColumns = false;
            this.dgCompteRemboursements.AllowUserToResizeRows = false;
            this.dgCompteRemboursements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCompteRemboursements.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(230)))), ((int)(((byte)(224)))));
            this.dgCompteRemboursements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCompteRemboursements.Location = new System.Drawing.Point(8, 4);
            this.dgCompteRemboursements.MultiSelect = false;
            this.dgCompteRemboursements.Name = "dgCompteRemboursements";
            this.dgCompteRemboursements.RowHeadersVisible = false;
            this.dgCompteRemboursements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCompteRemboursements.Size = new System.Drawing.Size(1071, 360);
            this.dgCompteRemboursements.TabIndex = 72;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgEncaissementsGlobals);
            this.tabPage2.Controls.Add(this.txtMontantRemboursement);
            this.tabPage2.Controls.Add(this.cmbModePaiement);
            this.tabPage2.Controls.Add(this.txtCommentaireRemboursement);
            this.tabPage2.Controls.Add(this.dtpDateRemboursement);
            this.tabPage2.Controls.Add(this.txtReferencePaiement);
            this.tabPage2.Controls.Add(this.cmdEnregistrerPaiement);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1084, 414);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Remboursements";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgEncaissementsGlobals
            // 
            this.dgEncaissementsGlobals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEncaissementsGlobals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncaissementsGlobals.Location = new System.Drawing.Point(7, 34);
            this.dgEncaissementsGlobals.MultiSelect = false;
            this.dgEncaissementsGlobals.Name = "dgEncaissementsGlobals";
            this.dgEncaissementsGlobals.RowHeadersVisible = false;
            this.dgEncaissementsGlobals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEncaissementsGlobals.Size = new System.Drawing.Size(1015, 374);
            this.dgEncaissementsGlobals.TabIndex = 109;
            // 
            // txtMontantRemboursement
            // 
            this.txtMontantRemboursement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontantRemboursement.Location = new System.Drawing.Point(86, 7);
            this.txtMontantRemboursement.Name = "txtMontantRemboursement";
            this.txtMontantRemboursement.Size = new System.Drawing.Size(80, 20);
            this.txtMontantRemboursement.TabIndex = 105;
            this.txtMontantRemboursement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMontantRemboursement.TextChanged += new System.EventHandler(this.txtMontantRemboursement_TextChanged);
            // 
            // cmbModePaiement
            // 
            this.cmbModePaiement.BackColor = System.Drawing.SystemColors.Window;
            this.cmbModePaiement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModePaiement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbModePaiement.FormattingEnabled = true;
            this.cmbModePaiement.Location = new System.Drawing.Point(168, 7);
            this.cmbModePaiement.Name = "cmbModePaiement";
            this.cmbModePaiement.Size = new System.Drawing.Size(79, 21);
            this.cmbModePaiement.TabIndex = 106;
            // 
            // txtCommentaireRemboursement
            // 
            this.txtCommentaireRemboursement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommentaireRemboursement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaireRemboursement.Location = new System.Drawing.Point(502, 7);
            this.txtCommentaireRemboursement.Name = "txtCommentaireRemboursement";
            this.txtCommentaireRemboursement.Size = new System.Drawing.Size(429, 20);
            this.txtCommentaireRemboursement.TabIndex = 108;
            // 
            // dtpDateRemboursement
            // 
            this.dtpDateRemboursement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateRemboursement.Location = new System.Drawing.Point(7, 7);
            this.dtpDateRemboursement.Name = "dtpDateRemboursement";
            this.dtpDateRemboursement.Size = new System.Drawing.Size(78, 20);
            this.dtpDateRemboursement.TabIndex = 104;
            // 
            // txtReferencePaiement
            // 
            this.txtReferencePaiement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferencePaiement.Location = new System.Drawing.Point(250, 7);
            this.txtReferencePaiement.Name = "txtReferencePaiement";
            this.txtReferencePaiement.Size = new System.Drawing.Size(250, 20);
            this.txtReferencePaiement.TabIndex = 107;
            // 
            // cmdEnregistrerPaiement
            // 
            this.cmdEnregistrerPaiement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnregistrerPaiement.Image = global::prjSenImmoWinform.Properties.Resources._079_16;
            this.cmdEnregistrerPaiement.Location = new System.Drawing.Point(937, 4);
            this.cmdEnregistrerPaiement.Name = "cmdEnregistrerPaiement";
            this.cmdEnregistrerPaiement.Size = new System.Drawing.Size(85, 27);
            this.cmdEnregistrerPaiement.TabIndex = 224;
            this.cmdEnregistrerPaiement.Text = "Enregistrer";
            this.cmdEnregistrerPaiement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEnregistrerPaiement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdEnregistrerPaiement.UseVisualStyleBackColor = true;
            this.cmdEnregistrerPaiement.Click += new System.EventHandler(this.cmdEnregistrerPaiement_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(205)))), ((int)(((byte)(193)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lbTypeClient);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtNumeroFixe);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtNumeroMobile);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtAdresse);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtLieuNaissance);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtDateNaissance);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cmbClients);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.cmdListerContrat);
            this.panel2.Controls.Add(this.cmdDossierClient);
            this.panel2.Location = new System.Drawing.Point(8, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1092, 137);
            this.panel2.TabIndex = 144;
            // 
            // lbTypeClient
            // 
            this.lbTypeClient.AutoSize = true;
            this.lbTypeClient.Location = new System.Drawing.Point(250, 8);
            this.lbTypeClient.Name = "lbTypeClient";
            this.lbTypeClient.Size = new System.Drawing.Size(49, 13);
            this.lbTypeClient.TabIndex = 97;
            this.lbTypeClient.Text = "Prospect";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmail.Location = new System.Drawing.Point(71, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(262, 20);
            this.txtEmail.TabIndex = 105;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(188, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 104;
            this.label7.Text = "Tél fixe";
            // 
            // txtNumeroFixe
            // 
            this.txtNumeroFixe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNumeroFixe.Location = new System.Drawing.Point(232, 81);
            this.txtNumeroFixe.Name = "txtNumeroFixe";
            this.txtNumeroFixe.ReadOnly = true;
            this.txtNumeroFixe.Size = new System.Drawing.Size(101, 20);
            this.txtNumeroFixe.TabIndex = 103;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "Email";
            // 
            // txtNumeroMobile
            // 
            this.txtNumeroMobile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNumeroMobile.Location = new System.Drawing.Point(71, 81);
            this.txtNumeroMobile.Name = "txtNumeroMobile";
            this.txtNumeroMobile.ReadOnly = true;
            this.txtNumeroMobile.Size = new System.Drawing.Size(101, 20);
            this.txtNumeroMobile.TabIndex = 101;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 100;
            this.label2.Text = "Tél mobile";
            // 
            // txtAdresse
            // 
            this.txtAdresse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAdresse.Location = new System.Drawing.Point(71, 56);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.ReadOnly = true;
            this.txtAdresse.Size = new System.Drawing.Size(262, 19);
            this.txtAdresse.TabIndex = 99;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 98;
            this.label11.Text = "Adresse";
            // 
            // txtLieuNaissance
            // 
            this.txtLieuNaissance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLieuNaissance.Location = new System.Drawing.Point(197, 30);
            this.txtLieuNaissance.Name = "txtLieuNaissance";
            this.txtLieuNaissance.ReadOnly = true;
            this.txtLieuNaissance.Size = new System.Drawing.Size(136, 20);
            this.txtLieuNaissance.TabIndex = 96;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(181, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 95;
            this.label13.Text = "à";
            // 
            // txtDateNaissance
            // 
            this.txtDateNaissance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDateNaissance.Location = new System.Drawing.Point(71, 30);
            this.txtDateNaissance.Name = "txtDateNaissance";
            this.txtDateNaissance.ReadOnly = true;
            this.txtDateNaissance.Size = new System.Drawing.Size(101, 20);
            this.txtDateNaissance.TabIndex = 94;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 93;
            this.label10.Text = "Né(e) le";
            // 
            // cmbClients
            // 
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(71, 3);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(175, 21);
            this.cmbClients.TabIndex = 92;
            this.cmbClients.SelectedIndexChanged += new System.EventHandler(this.cmbClients_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(29, 7);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 91;
            this.label20.Text = "Client";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 8);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(0, 13);
            this.label25.TabIndex = 90;
            // 
            // cmdListerContrat
            // 
            this.cmdListerContrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdListerContrat.Location = new System.Drawing.Point(972, 10);
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
            this.cmdDossierClient.Location = new System.Drawing.Point(971, 72);
            this.cmdDossierClient.Name = "cmdDossierClient";
            this.cmdDossierClient.Size = new System.Drawing.Size(114, 35);
            this.cmdDossierClient.TabIndex = 89;
            this.cmdDossierClient.Text = "Dossier client";
            this.cmdDossierClient.UseVisualStyleBackColor = true;
            this.cmdDossierClient.Visible = false;
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(1013, 589);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(83, 31);
            this.cmdFermer.TabIndex = 145;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // FrmRemboursement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 632);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.tcCompteClient);
            this.Controls.Add(this.panel2);
            this.Name = "FrmRemboursement";
            this.Text = "Gestion des remboursements";
            this.tcCompteClient.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCompteRemboursements)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncaissementsGlobals)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCompteClient;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgCompteRemboursements;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgEncaissementsGlobals;
        private System.Windows.Forms.TextBox txtMontantRemboursement;
        private System.Windows.Forms.ComboBox cmbModePaiement;
        private System.Windows.Forms.TextBox txtCommentaireRemboursement;
        private System.Windows.Forms.DateTimePicker dtpDateRemboursement;
        private System.Windows.Forms.TextBox txtReferencePaiement;
        private System.Windows.Forms.Button cmdEnregistrerPaiement;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdListerContrat;
        private System.Windows.Forms.Button cmdDossierClient;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Label lbTypeClient;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumeroFixe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroMobile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLieuNaissance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDateNaissance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lbDebitTotal;
        private System.Windows.Forms.Label lbSoldeTotal;
        private System.Windows.Forms.Label lbCreditTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}