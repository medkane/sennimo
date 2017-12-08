namespace prjSenImmoWinform
{
    partial class FrmVenteDepot
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.dgEcheances = new System.Windows.Forms.DataGridView();
            this.txtVersmentApresDepot = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudDureeDepot = new System.Windows.Forms.NumericUpDown();
            this.label63 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.dtpDateDerniereEcheance = new System.Windows.Forms.DateTimePicker();
            this.dtpDatePremiereEcheance = new System.Windows.Forms.DateTimePicker();
            this.cmbTypeEcheanciers = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.cmdSimuler = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMontantEncaisse = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutreDepotMinimum = new System.Windows.Forms.CheckBox();
            this.txtDepotGarantieDepot = new System.Windows.Forms.TextBox();
            this.txtNombresEcheancesAutreDepotMinimum = new System.Windows.Forms.TextBox();
            this.dtpDateLivraisonDepotPrevue = new System.Windows.Forms.DateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.txtEcheanceAutreDepotMinimum = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.rbModeReglement = new System.Windows.Forms.RadioButton();
            this.pModeReglement = new System.Windows.Forms.Panel();
            this.txtMontantDerniereEcheance = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.txtNombreEcheances = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtMontantEcheance = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txtDerniereEcheancesAutreDepotMinimum = new System.Windows.Forms.TextBox();
            this.chkAutreEcheance = new System.Windows.Forms.CheckBox();
            this.txtAutreDepotMinimum = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMontantDerniereEcheanceSouhaitee = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.txtNombreEcheancesSouhaitees = new System.Windows.Forms.TextBox();
            this.txtMontantEcheanceSouhaite = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEcheances)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDureeDepot)).BeginInit();
            this.panel1.SuspendLayout();
            this.pModeReglement.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cmdEnregistrer);
            this.panel3.Controls.Add(this.dgEcheances);
            this.panel3.Controls.Add(this.txtVersmentApresDepot);
            this.panel3.Controls.Add(this.label59);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label34);
            this.panel3.Controls.Add(this.cmdSimuler);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.txtNombresEcheancesAutreDepotMinimum);
            this.panel3.Controls.Add(this.dtpDateLivraisonDepotPrevue);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.txtEcheanceAutreDepotMinimum);
            this.panel3.Controls.Add(this.label43);
            this.panel3.Location = new System.Drawing.Point(463, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(593, 566);
            this.panel3.TabIndex = 65;
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdEnregistrer.Location = new System.Drawing.Point(467, 529);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(117, 27);
            this.cmdEnregistrer.TabIndex = 239;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // dgEcheances
            // 
            this.dgEcheances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEcheances.Location = new System.Drawing.Point(10, 186);
            this.dgEcheances.Name = "dgEcheances";
            this.dgEcheances.Size = new System.Drawing.Size(575, 337);
            this.dgEcheances.TabIndex = 238;
            // 
            // txtVersmentApresDepot
            // 
            this.txtVersmentApresDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersmentApresDepot.Location = new System.Drawing.Point(130, 154);
            this.txtVersmentApresDepot.Name = "txtVersmentApresDepot";
            this.txtVersmentApresDepot.ReadOnly = true;
            this.txtVersmentApresDepot.Size = new System.Drawing.Size(71, 20);
            this.txtVersmentApresDepot.TabIndex = 108;
            this.txtVersmentApresDepot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(14, 157);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(113, 13);
            this.label59.TabIndex = 107;
            this.label59.Text = "Montant à ventiller";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.nudDureeDepot);
            this.panel2.Controls.Add(this.label63);
            this.panel2.Controls.Add(this.label60);
            this.panel2.Controls.Add(this.dtpDateDerniereEcheance);
            this.panel2.Controls.Add(this.dtpDatePremiereEcheance);
            this.panel2.Controls.Add(this.cmbTypeEcheanciers);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label49);
            this.panel2.Location = new System.Drawing.Point(5, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(580, 69);
            this.panel2.TabIndex = 236;
            // 
            // nudDureeDepot
            // 
            this.nudDureeDepot.Location = new System.Drawing.Point(389, 12);
            this.nudDureeDepot.Name = "nudDureeDepot";
            this.nudDureeDepot.Size = new System.Drawing.Size(61, 20);
            this.nudDureeDepot.TabIndex = 66;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(456, 14);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(28, 13);
            this.label63.TabIndex = 244;
            this.label63.Text = "mois";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(319, 15);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(66, 13);
            this.label60.TabIndex = 242;
            this.label60.Text = "Durée dépôt";
            // 
            // dtpDateDerniereEcheance
            // 
            this.dtpDateDerniereEcheance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDerniereEcheance.Location = new System.Drawing.Point(389, 39);
            this.dtpDateDerniereEcheance.Name = "dtpDateDerniereEcheance";
            this.dtpDateDerniereEcheance.Size = new System.Drawing.Size(95, 20);
            this.dtpDateDerniereEcheance.TabIndex = 241;
            // 
            // dtpDatePremiereEcheance
            // 
            this.dtpDatePremiereEcheance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatePremiereEcheance.Location = new System.Drawing.Point(126, 39);
            this.dtpDatePremiereEcheance.Name = "dtpDatePremiereEcheance";
            this.dtpDatePremiereEcheance.Size = new System.Drawing.Size(95, 20);
            this.dtpDatePremiereEcheance.TabIndex = 65;
            // 
            // cmbTypeEcheanciers
            // 
            this.cmbTypeEcheanciers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbTypeEcheanciers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeEcheanciers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTypeEcheanciers.FormattingEnabled = true;
            this.cmbTypeEcheanciers.Location = new System.Drawing.Point(126, 12);
            this.cmbTypeEcheanciers.Name = "cmbTypeEcheanciers";
            this.cmbTypeEcheanciers.Size = new System.Drawing.Size(165, 21);
            this.cmbTypeEcheanciers.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Périodicité";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Date première échéance";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(264, 43);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(122, 13);
            this.label49.TabIndex = 97;
            this.label49.Text = "Date dernière échéance";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(293, 131);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(100, 13);
            this.label34.TabIndex = 83;
            this.label34.Text = "Nombre échéances";
            // 
            // cmdSimuler
            // 
            this.cmdSimuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSimuler.Location = new System.Drawing.Point(499, 154);
            this.cmdSimuler.Name = "cmdSimuler";
            this.cmdSimuler.Size = new System.Drawing.Size(86, 27);
            this.cmdSimuler.TabIndex = 67;
            this.cmdSimuler.Text = "Calculer";
            this.cmdSimuler.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtMontantEncaisse);
            this.panel1.Controls.Add(this.label57);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkAutreDepotMinimum);
            this.panel1.Controls.Add(this.txtDepotGarantieDepot);
            this.panel1.Location = new System.Drawing.Point(5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(579, 44);
            this.panel1.TabIndex = 237;
            // 
            // txtMontantEncaisse
            // 
            this.txtMontantEncaisse.Location = new System.Drawing.Point(100, 10);
            this.txtMontantEncaisse.Name = "txtMontantEncaisse";
            this.txtMontantEncaisse.ReadOnly = true;
            this.txtMontantEncaisse.Size = new System.Drawing.Size(71, 20);
            this.txtMontantEncaisse.TabIndex = 110;
            this.txtMontantEncaisse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(179, 14);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(79, 13);
            this.label57.TabIndex = 103;
            this.label57.Text = "Dépôt minimum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "Montant Encaissé";
            // 
            // chkAutreDepotMinimum
            // 
            this.chkAutreDepotMinimum.AutoSize = true;
            this.chkAutreDepotMinimum.Location = new System.Drawing.Point(334, 12);
            this.chkAutreDepotMinimum.Name = "chkAutreDepotMinimum";
            this.chkAutreDepotMinimum.Size = new System.Drawing.Size(264, 17);
            this.chkAutreDepotMinimum.TabIndex = 87;
            this.chkAutreDepotMinimum.Text = "Considerer tout le solde comme dépôt minimum de ";
            this.chkAutreDepotMinimum.UseVisualStyleBackColor = true;
            // 
            // txtDepotGarantieDepot
            // 
            this.txtDepotGarantieDepot.Location = new System.Drawing.Point(259, 10);
            this.txtDepotGarantieDepot.Name = "txtDepotGarantieDepot";
            this.txtDepotGarantieDepot.ReadOnly = true;
            this.txtDepotGarantieDepot.Size = new System.Drawing.Size(71, 20);
            this.txtDepotGarantieDepot.TabIndex = 104;
            this.txtDepotGarantieDepot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNombresEcheancesAutreDepotMinimum
            // 
            this.txtNombresEcheancesAutreDepotMinimum.Location = new System.Drawing.Point(396, 128);
            this.txtNombresEcheancesAutreDepotMinimum.Name = "txtNombresEcheancesAutreDepotMinimum";
            this.txtNombresEcheancesAutreDepotMinimum.ReadOnly = true;
            this.txtNombresEcheancesAutreDepotMinimum.Size = new System.Drawing.Size(71, 20);
            this.txtNombresEcheancesAutreDepotMinimum.TabIndex = 84;
            this.txtNombresEcheancesAutreDepotMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpDateLivraisonDepotPrevue
            // 
            this.dtpDateLivraisonDepotPrevue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDateLivraisonDepotPrevue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateLivraisonDepotPrevue.Location = new System.Drawing.Point(140, 532);
            this.dtpDateLivraisonDepotPrevue.Name = "dtpDateLivraisonDepotPrevue";
            this.dtpDateLivraisonDepotPrevue.Size = new System.Drawing.Size(108, 20);
            this.dtpDateLivraisonDepotPrevue.TabIndex = 98;
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(7, 535);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(128, 13);
            this.label38.TabIndex = 94;
            this.label38.Text = "Date livraison prévue";
            // 
            // txtEcheanceAutreDepotMinimum
            // 
            this.txtEcheanceAutreDepotMinimum.Location = new System.Drawing.Point(130, 128);
            this.txtEcheanceAutreDepotMinimum.Name = "txtEcheanceAutreDepotMinimum";
            this.txtEcheanceAutreDepotMinimum.Size = new System.Drawing.Size(71, 20);
            this.txtEcheanceAutreDepotMinimum.TabIndex = 80;
            this.txtEcheanceAutreDepotMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(30, 131);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(97, 13);
            this.label43.TabIndex = 79;
            this.label43.Text = "Montant échéance";
            // 
            // rbModeReglement
            // 
            this.rbModeReglement.AutoSize = true;
            this.rbModeReglement.Location = new System.Drawing.Point(41, 193);
            this.rbModeReglement.Name = "rbModeReglement";
            this.rbModeReglement.Size = new System.Drawing.Size(116, 17);
            this.rbModeReglement.TabIndex = 242;
            this.rbModeReglement.TabStop = true;
            this.rbModeReglement.Text = "Mode de règlement";
            this.rbModeReglement.UseVisualStyleBackColor = true;
            // 
            // pModeReglement
            // 
            this.pModeReglement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pModeReglement.Controls.Add(this.txtMontantDerniereEcheance);
            this.pModeReglement.Controls.Add(this.label17);
            this.pModeReglement.Controls.Add(this.label42);
            this.pModeReglement.Controls.Add(this.txtNombreEcheances);
            this.pModeReglement.Controls.Add(this.label24);
            this.pModeReglement.Controls.Add(this.txtMontantEcheance);
            this.pModeReglement.Location = new System.Drawing.Point(22, 135);
            this.pModeReglement.Name = "pModeReglement";
            this.pModeReglement.Size = new System.Drawing.Size(535, 41);
            this.pModeReglement.TabIndex = 240;
            // 
            // txtMontantDerniereEcheance
            // 
            this.txtMontantDerniereEcheance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMontantDerniereEcheance.Location = new System.Drawing.Point(450, 10);
            this.txtMontantDerniereEcheance.Name = "txtMontantDerniereEcheance";
            this.txtMontantDerniereEcheance.ReadOnly = true;
            this.txtMontantDerniereEcheance.Size = new System.Drawing.Size(69, 20);
            this.txtMontantDerniereEcheance.TabIndex = 72;
            this.txtMontantDerniereEcheance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(349, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 13);
            this.label17.TabIndex = 73;
            this.label17.Text = "Dernière echéance";
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(23, 14);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(97, 13);
            this.label42.TabIndex = 70;
            this.label42.Text = "Montant echéance";
            // 
            // txtNombreEcheances
            // 
            this.txtNombreEcheances.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNombreEcheances.Location = new System.Drawing.Point(307, 10);
            this.txtNombreEcheances.Name = "txtNombreEcheances";
            this.txtNombreEcheances.ReadOnly = true;
            this.txtNombreEcheances.Size = new System.Drawing.Size(23, 20);
            this.txtNombreEcheances.TabIndex = 69;
            this.txtNombreEcheances.Text = "56";
            this.txtNombreEcheances.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(204, 14);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(100, 13);
            this.label24.TabIndex = 68;
            this.label24.Text = "Nombre échéances";
            // 
            // txtMontantEcheance
            // 
            this.txtMontantEcheance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMontantEcheance.Location = new System.Drawing.Point(122, 10);
            this.txtMontantEcheance.Name = "txtMontantEcheance";
            this.txtMontantEcheance.ReadOnly = true;
            this.txtMontantEcheance.Size = new System.Drawing.Size(69, 20);
            this.txtMontantEcheance.TabIndex = 71;
            this.txtMontantEcheance.Text = "1 200 000";
            this.txtMontantEcheance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(124, 276);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(98, 13);
            this.label47.TabIndex = 89;
            this.label47.Text = "Dernière echéance";
            // 
            // txtDerniereEcheancesAutreDepotMinimum
            // 
            this.txtDerniereEcheancesAutreDepotMinimum.Location = new System.Drawing.Point(463, 320);
            this.txtDerniereEcheancesAutreDepotMinimum.Name = "txtDerniereEcheancesAutreDepotMinimum";
            this.txtDerniereEcheancesAutreDepotMinimum.ReadOnly = true;
            this.txtDerniereEcheancesAutreDepotMinimum.Size = new System.Drawing.Size(69, 20);
            this.txtDerniereEcheancesAutreDepotMinimum.TabIndex = 90;
            this.txtDerniereEcheancesAutreDepotMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkAutreEcheance
            // 
            this.chkAutreEcheance.AutoSize = true;
            this.chkAutreEcheance.Location = new System.Drawing.Point(12, 349);
            this.chkAutreEcheance.Name = "chkAutreEcheance";
            this.chkAutreEcheance.Size = new System.Drawing.Size(210, 17);
            this.chkAutreEcheance.TabIndex = 88;
            this.chkAutreEcheance.Text = "Considérer un autre montant échéance";
            this.chkAutreEcheance.UseVisualStyleBackColor = true;
            // 
            // txtAutreDepotMinimum
            // 
            this.txtAutreDepotMinimum.Location = new System.Drawing.Point(277, 295);
            this.txtAutreDepotMinimum.Name = "txtAutreDepotMinimum";
            this.txtAutreDepotMinimum.ReadOnly = true;
            this.txtAutreDepotMinimum.Size = new System.Drawing.Size(69, 20);
            this.txtAutreDepotMinimum.TabIndex = 86;
            this.txtAutreDepotMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(364, 369);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 13);
            this.label18.TabIndex = 74;
            this.label18.Text = "Dernière echéance";
            // 
            // txtMontantDerniereEcheanceSouhaitee
            // 
            this.txtMontantDerniereEcheanceSouhaitee.Location = new System.Drawing.Point(463, 366);
            this.txtMontantDerniereEcheanceSouhaitee.Name = "txtMontantDerniereEcheanceSouhaitee";
            this.txtMontantDerniereEcheanceSouhaitee.ReadOnly = true;
            this.txtMontantDerniereEcheanceSouhaitee.Size = new System.Drawing.Size(69, 20);
            this.txtMontantDerniereEcheanceSouhaitee.TabIndex = 76;
            this.txtMontantDerniereEcheanceSouhaitee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(233, 370);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(100, 13);
            this.label64.TabIndex = 74;
            this.label64.Text = "Nombre échéances";
            // 
            // txtNombreEcheancesSouhaitees
            // 
            this.txtNombreEcheancesSouhaitees.Location = new System.Drawing.Point(336, 366);
            this.txtNombreEcheancesSouhaitees.Name = "txtNombreEcheancesSouhaitees";
            this.txtNombreEcheancesSouhaitees.ReadOnly = true;
            this.txtNombreEcheancesSouhaitees.Size = new System.Drawing.Size(23, 20);
            this.txtNombreEcheancesSouhaitees.TabIndex = 75;
            this.txtNombreEcheancesSouhaitees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMontantEcheanceSouhaite
            // 
            this.txtMontantEcheanceSouhaite.Location = new System.Drawing.Point(158, 370);
            this.txtMontantEcheanceSouhaite.Name = "txtMontantEcheanceSouhaite";
            this.txtMontantEcheanceSouhaite.Size = new System.Drawing.Size(69, 20);
            this.txtMontantEcheanceSouhaite.TabIndex = 73;
            this.txtMontantEcheanceSouhaite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(58, 373);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(99, 13);
            this.label58.TabIndex = 72;
            this.label58.Text = "Echéance souhaité";
            // 
            // FrmVenteDepot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 585);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.rbModeReglement);
            this.Controls.Add(this.pModeReglement);
            this.Controls.Add(this.txtDerniereEcheancesAutreDepotMinimum);
            this.Controls.Add(this.chkAutreEcheance);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.txtMontantEcheanceSouhaite);
            this.Controls.Add(this.txtAutreDepotMinimum);
            this.Controls.Add(this.txtNombreEcheancesSouhaitees);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.txtMontantDerniereEcheanceSouhaitee);
            this.Controls.Add(this.label18);
            this.Name = "FrmVenteDepot";
            this.Text = "FrmVenteDepot";
            this.Load += new System.EventHandler(this.FrmVenteDepot_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEcheances)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDureeDepot)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pModeReglement.ResumeLayout(false);
            this.pModeReglement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbModeReglement;
        private System.Windows.Forms.Panel pModeReglement;
        private System.Windows.Forms.TextBox txtMontantDerniereEcheance;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtNombreEcheances;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtMontantEcheance;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown nudDureeDepot;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.DateTimePicker dtpDateDerniereEcheance;
        private System.Windows.Forms.DateTimePicker dtpDatePremiereEcheance;
        private System.Windows.Forms.ComboBox cmbTypeEcheanciers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtDerniereEcheancesAutreDepotMinimum;
        private System.Windows.Forms.CheckBox chkAutreEcheance;
        private System.Windows.Forms.CheckBox chkAutreDepotMinimum;
        private System.Windows.Forms.TextBox txtAutreDepotMinimum;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtNombresEcheancesAutreDepotMinimum;
        private System.Windows.Forms.TextBox txtEcheanceAutreDepotMinimum;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMontantDerniereEcheanceSouhaitee;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txtNombreEcheancesSouhaitees;
        private System.Windows.Forms.TextBox txtMontantEcheanceSouhaite;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Button cmdSimuler;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtVersmentApresDepot;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtDepotGarantieDepot;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.DateTimePicker dtpDateLivraisonDepotPrevue;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtMontantEncaisse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgEcheances;
        private System.Windows.Forms.Button cmdEnregistrer;
    }
}