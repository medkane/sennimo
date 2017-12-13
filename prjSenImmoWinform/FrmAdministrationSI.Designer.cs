namespace prjSenImmoWinform
{
    partial class FrmAdministrationSI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdSupprimer = new System.Windows.Forms.Button();
            this.rbParImportation = new System.Windows.Forms.RadioButton();
            this.txtNumeroLotLiberation = new System.Windows.Forms.TextBox();
            this.rbParLot = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdReserverLot = new System.Windows.Forms.Button();
            this.lvListLotsAliberer = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdParcourir = new System.Windows.Forms.Button();
            this.cmdAjouterLot = new System.Windows.Forms.Button();
            this.cmdLibererLot = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdSupprimerContrat = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrixDeVente = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTypeVilla = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalEncaisse = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTypeContrat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroDossier = new System.Windows.Forms.TextBox();
            this.cmdRechercherContrat = new System.Windows.Forms.Button();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.ofdFichierSource = new System.Windows.Forms.OpenFileDialog();
            this.cmdChangementLot = new System.Windows.Forms.Button();
            this.txtNouveauLot = new System.Windows.Forms.TextBox();
            this.lbNouveauLot = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 466);
            this.panel1.TabIndex = 225;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(5, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(594, 449);
            this.tabControl1.TabIndex = 234;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdSupprimer);
            this.tabPage1.Controls.Add(this.rbParImportation);
            this.tabPage1.Controls.Add(this.txtNumeroLotLiberation);
            this.tabPage1.Controls.Add(this.rbParLot);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmdReserverLot);
            this.tabPage1.Controls.Add(this.lvListLotsAliberer);
            this.tabPage1.Controls.Add(this.cmdParcourir);
            this.tabPage1.Controls.Add(this.cmdAjouterLot);
            this.tabPage1.Controls.Add(this.cmdLibererLot);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(586, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gestion des lots";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdSupprimer
            // 
            this.cmdSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSupprimer.Enabled = false;
            this.cmdSupprimer.Location = new System.Drawing.Point(397, 392);
            this.cmdSupprimer.Name = "cmdSupprimer";
            this.cmdSupprimer.Size = new System.Drawing.Size(94, 23);
            this.cmdSupprimer.TabIndex = 223;
            this.cmdSupprimer.Text = "Annuler";
            this.cmdSupprimer.UseVisualStyleBackColor = true;
            this.cmdSupprimer.Click += new System.EventHandler(this.cmdSupprimer_Click);
            // 
            // rbParImportation
            // 
            this.rbParImportation.AutoSize = true;
            this.rbParImportation.Location = new System.Drawing.Point(93, 16);
            this.rbParImportation.Name = "rbParImportation";
            this.rbParImportation.Size = new System.Drawing.Size(95, 17);
            this.rbParImportation.TabIndex = 222;
            this.rbParImportation.Text = "Par importation";
            this.rbParImportation.UseVisualStyleBackColor = true;
            this.rbParImportation.CheckedChanged += new System.EventHandler(this.rbParImportation_CheckedChanged);
            // 
            // txtNumeroLotLiberation
            // 
            this.txtNumeroLotLiberation.Location = new System.Drawing.Point(64, 43);
            this.txtNumeroLotLiberation.Name = "txtNumeroLotLiberation";
            this.txtNumeroLotLiberation.Size = new System.Drawing.Size(244, 20);
            this.txtNumeroLotLiberation.TabIndex = 216;
            this.txtNumeroLotLiberation.TextChanged += new System.EventHandler(this.txtNumeroLotLiberation_TextChanged);
            // 
            // rbParLot
            // 
            this.rbParLot.AutoSize = true;
            this.rbParLot.Checked = true;
            this.rbParLot.Location = new System.Drawing.Point(32, 16);
            this.rbParLot.Name = "rbParLot";
            this.rbParLot.Size = new System.Drawing.Size(55, 17);
            this.rbParLot.TabIndex = 221;
            this.rbParLot.TabStop = true;
            this.rbParLot.Text = "Par lot";
            this.rbParLot.UseVisualStyleBackColor = true;
            this.rbParLot.CheckedChanged += new System.EventHandler(this.rbParLot_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 215;
            this.label1.Text = "Numéro lot";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmdReserverLot
            // 
            this.cmdReserverLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReserverLot.Enabled = false;
            this.cmdReserverLot.Location = new System.Drawing.Point(492, 71);
            this.cmdReserverLot.Name = "cmdReserverLot";
            this.cmdReserverLot.Size = new System.Drawing.Size(90, 24);
            this.cmdReserverLot.TabIndex = 220;
            this.cmdReserverLot.Text = "Réserver";
            this.cmdReserverLot.UseVisualStyleBackColor = true;
            this.cmdReserverLot.Click += new System.EventHandler(this.cmdReserver_Click);
            // 
            // lvListLotsAliberer
            // 
            this.lvListLotsAliberer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvListLotsAliberer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvListLotsAliberer.Location = new System.Drawing.Point(9, 71);
            this.lvListLotsAliberer.Name = "lvListLotsAliberer";
            this.lvListLotsAliberer.Size = new System.Drawing.Size(477, 315);
            this.lvListLotsAliberer.TabIndex = 214;
            this.lvListLotsAliberer.UseCompatibleStateImageBehavior = false;
            this.lvListLotsAliberer.View = System.Windows.Forms.View.Details;
            this.lvListLotsAliberer.SelectedIndexChanged += new System.EventHandler(this.lvListLotsAliberer_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Numéro lot";
            this.columnHeader1.Width = 370;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Statut";
            this.columnHeader2.Width = 109;
            // 
            // cmdParcourir
            // 
            this.cmdParcourir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdParcourir.Enabled = false;
            this.cmdParcourir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdParcourir.Location = new System.Drawing.Point(400, 40);
            this.cmdParcourir.Name = "cmdParcourir";
            this.cmdParcourir.Size = new System.Drawing.Size(86, 25);
            this.cmdParcourir.TabIndex = 219;
            this.cmdParcourir.Text = "Parcourir...";
            this.cmdParcourir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdParcourir.UseVisualStyleBackColor = true;
            this.cmdParcourir.Click += new System.EventHandler(this.cmdParcourir_Click);
            // 
            // cmdAjouterLot
            // 
            this.cmdAjouterLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAjouterLot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAjouterLot.Location = new System.Drawing.Point(311, 40);
            this.cmdAjouterLot.Name = "cmdAjouterLot";
            this.cmdAjouterLot.Size = new System.Drawing.Size(86, 25);
            this.cmdAjouterLot.TabIndex = 217;
            this.cmdAjouterLot.Text = "Ajouter";
            this.cmdAjouterLot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAjouterLot.UseVisualStyleBackColor = true;
            this.cmdAjouterLot.Click += new System.EventHandler(this.cmdAjouter_Click);
            // 
            // cmdLibererLot
            // 
            this.cmdLibererLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLibererLot.Enabled = false;
            this.cmdLibererLot.Location = new System.Drawing.Point(492, 100);
            this.cmdLibererLot.Name = "cmdLibererLot";
            this.cmdLibererLot.Size = new System.Drawing.Size(90, 24);
            this.cmdLibererLot.TabIndex = 218;
            this.cmdLibererLot.Text = "Libérer";
            this.cmdLibererLot.UseVisualStyleBackColor = true;
            this.cmdLibererLot.Click += new System.EventHandler(this.cmdLibererLot_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmdChangementLot);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.cmdSupprimerContrat);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.txtNumeroDossier);
            this.tabPage3.Controls.Add(this.cmdRechercherContrat);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(586, 423);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Gestion des contrats";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(340, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 224;
            this.button1.Text = "Annuler";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmdSupprimerContrat
            // 
            this.cmdSupprimerContrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSupprimerContrat.Enabled = false;
            this.cmdSupprimerContrat.Location = new System.Drawing.Point(438, 33);
            this.cmdSupprimerContrat.Name = "cmdSupprimerContrat";
            this.cmdSupprimerContrat.Size = new System.Drawing.Size(135, 23);
            this.cmdSupprimerContrat.TabIndex = 4;
            this.cmdSupprimerContrat.Text = "Supprimer le contrat";
            this.cmdSupprimerContrat.UseVisualStyleBackColor = true;
            this.cmdSupprimerContrat.Click += new System.EventHandler(this.cmdSupprimerContrat_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtNouveauLot);
            this.panel3.Controls.Add(this.lbNouveauLot);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtPrixDeVente);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.txtLot);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.txtTypeVilla);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtTotalEncaisse);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtClient);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtTypeContrat);
            this.panel3.Location = new System.Drawing.Point(6, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(426, 353);
            this.panel3.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Total encaissé";
            // 
            // txtPrixDeVente
            // 
            this.txtPrixDeVente.Location = new System.Drawing.Point(90, 86);
            this.txtPrixDeVente.Name = "txtPrixDeVente";
            this.txtPrixDeVente.ReadOnly = true;
            this.txtPrixDeVente.Size = new System.Drawing.Size(100, 20);
            this.txtPrixDeVente.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(281, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Lot";
            // 
            // txtLot
            // 
            this.txtLot.Location = new System.Drawing.Point(306, 60);
            this.txtLot.Name = "txtLot";
            this.txtLot.ReadOnly = true;
            this.txtLot.Size = new System.Drawing.Size(100, 20);
            this.txtLot.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Type villa";
            // 
            // txtTypeVilla
            // 
            this.txtTypeVilla.Location = new System.Drawing.Point(90, 60);
            this.txtTypeVilla.Name = "txtTypeVilla";
            this.txtTypeVilla.ReadOnly = true;
            this.txtTypeVilla.Size = new System.Drawing.Size(100, 20);
            this.txtTypeVilla.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Prix de vente";
            // 
            // txtTotalEncaisse
            // 
            this.txtTotalEncaisse.Location = new System.Drawing.Point(306, 86);
            this.txtTotalEncaisse.Name = "txtTotalEncaisse";
            this.txtTotalEncaisse.ReadOnly = true;
            this.txtTotalEncaisse.Size = new System.Drawing.Size(100, 20);
            this.txtTotalEncaisse.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Client";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(90, 33);
            this.txtClient.Name = "txtClient";
            this.txtClient.ReadOnly = true;
            this.txtClient.Size = new System.Drawing.Size(316, 20);
            this.txtClient.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type contrat";
            // 
            // txtTypeContrat
            // 
            this.txtTypeContrat.Location = new System.Drawing.Point(90, 7);
            this.txtTypeContrat.Name = "txtTypeContrat";
            this.txtTypeContrat.ReadOnly = true;
            this.txtTypeContrat.Size = new System.Drawing.Size(100, 20);
            this.txtTypeContrat.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Numéro dossier";
            // 
            // txtNumeroDossier
            // 
            this.txtNumeroDossier.Location = new System.Drawing.Point(85, 6);
            this.txtNumeroDossier.Name = "txtNumeroDossier";
            this.txtNumeroDossier.Size = new System.Drawing.Size(260, 20);
            this.txtNumeroDossier.TabIndex = 1;
            // 
            // cmdRechercherContrat
            // 
            this.cmdRechercherContrat.Location = new System.Drawing.Point(351, 4);
            this.cmdRechercherContrat.Name = "cmdRechercherContrat";
            this.cmdRechercherContrat.Size = new System.Drawing.Size(83, 23);
            this.cmdRechercherContrat.TabIndex = 0;
            this.cmdRechercherContrat.Text = "Rechercher";
            this.cmdRechercherContrat.UseVisualStyleBackColor = true;
            this.cmdRechercherContrat.Click += new System.EventHandler(this.cmdRechercherContrat_Click);
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdFermer.Location = new System.Drawing.Point(526, 477);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(84, 27);
            this.cmdFermer.TabIndex = 228;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // ofdFichierSource
            // 
            this.ofdFichierSource.FileName = "openFileDialog1";
            // 
            // cmdChangementLot
            // 
            this.cmdChangementLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChangementLot.Location = new System.Drawing.Point(438, 179);
            this.cmdChangementLot.Name = "cmdChangementLot";
            this.cmdChangementLot.Size = new System.Drawing.Size(135, 23);
            this.cmdChangementLot.TabIndex = 225;
            this.cmdChangementLot.Text = "Changer le lot attribué";
            this.cmdChangementLot.UseVisualStyleBackColor = true;
            this.cmdChangementLot.Visible = false;
            this.cmdChangementLot.Click += new System.EventHandler(this.cmdChangementLot_Click);
            // 
            // txtNouveauLot
            // 
            this.txtNouveauLot.BackColor = System.Drawing.SystemColors.Window;
            this.txtNouveauLot.Location = new System.Drawing.Point(90, 147);
            this.txtNouveauLot.Name = "txtNouveauLot";
            this.txtNouveauLot.Size = new System.Drawing.Size(100, 20);
            this.txtNouveauLot.TabIndex = 16;
            this.txtNouveauLot.Visible = false;
            // 
            // lbNouveauLot
            // 
            this.lbNouveauLot.AutoSize = true;
            this.lbNouveauLot.Location = new System.Drawing.Point(11, 150);
            this.lbNouveauLot.Name = "lbNouveauLot";
            this.lbNouveauLot.Size = new System.Drawing.Size(69, 13);
            this.lbNouveauLot.TabIndex = 15;
            this.lbNouveauLot.Text = "Nouveau Lot";
            this.lbNouveauLot.Visible = false;
            // 
            // FrmAdministrationSI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 512);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdFermer);
            this.Name = "FrmAdministrationSI";
            this.Text = "FrmAdministrationSI";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdAjouterLot;
        private System.Windows.Forms.ListView lvListLotsAliberer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroLotLiberation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumeroDossier;
        private System.Windows.Forms.Button cmdRechercherContrat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalEncaisse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTypeContrat;
        private System.Windows.Forms.Button cmdSupprimerContrat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrixDeVente;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTypeVilla;
        private System.Windows.Forms.Button cmdLibererLot;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button cmdParcourir;
        private System.Windows.Forms.Button cmdReserverLot;
        private System.Windows.Forms.OpenFileDialog ofdFichierSource;
        private System.Windows.Forms.RadioButton rbParImportation;
        private System.Windows.Forms.RadioButton rbParLot;
        private System.Windows.Forms.Button cmdSupprimer;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdChangementLot;
        private System.Windows.Forms.TextBox txtNouveauLot;
        private System.Windows.Forms.Label lbNouveauLot;
    }
}