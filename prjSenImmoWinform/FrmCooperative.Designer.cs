namespace prjSenImmoWinform
{
    partial class FrmCooperative
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pCommandes = new System.Windows.Forms.Panel();
            this.cmdNouveau = new System.Windows.Forms.Button();
            this.cmdEnregistrer = new System.Windows.Forms.Button();
            this.cmdEditer = new System.Windows.Forms.Button();
            this.cmdSupprimer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateSouscription = new System.Windows.Forms.DateTimePicker();
            this.nudTauxCommission = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTelephoneFixe = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTelephoneMobile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtDenomination = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdAjouterMembre = new System.Windows.Forms.Button();
            this.lvMembres = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtNbAdherents = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvCooperatives = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdFermer = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pCommandes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTauxCommission)).BeginInit();
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
            this.splitContainer1.Location = new System.Drawing.Point(4, 7);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdAjouterMembre);
            this.splitContainer1.Panel2.Controls.Add(this.lvMembres);
            this.splitContainer1.Panel2.Controls.Add(this.txtNbAdherents);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(723, 436);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pCommandes);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDateSouscription);
            this.panel1.Controls.Add(this.nudTauxCommission);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtTelephoneFixe);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtTelephoneMobile);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtAdresse);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.txtDenomination);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 143);
            this.panel1.TabIndex = 142;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pCommandes
            // 
            this.pCommandes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pCommandes.Controls.Add(this.cmdNouveau);
            this.pCommandes.Controls.Add(this.cmdEnregistrer);
            this.pCommandes.Controls.Add(this.cmdEditer);
            this.pCommandes.Controls.Add(this.cmdSupprimer);
            this.pCommandes.Location = new System.Drawing.Point(613, 3);
            this.pCommandes.Name = "pCommandes";
            this.pCommandes.Size = new System.Drawing.Size(98, 118);
            this.pCommandes.TabIndex = 233;
            // 
            // cmdNouveau
            // 
            this.cmdNouveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNouveau.Location = new System.Drawing.Point(4, 3);
            this.cmdNouveau.Name = "cmdNouveau";
            this.cmdNouveau.Size = new System.Drawing.Size(91, 27);
            this.cmdNouveau.TabIndex = 135;
            this.cmdNouveau.Text = "Nouveau";
            this.cmdNouveau.UseVisualStyleBackColor = true;
            this.cmdNouveau.Click += new System.EventHandler(this.cmdNouveau_Click);
            // 
            // cmdEnregistrer
            // 
            this.cmdEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnregistrer.Location = new System.Drawing.Point(4, 58);
            this.cmdEnregistrer.Name = "cmdEnregistrer";
            this.cmdEnregistrer.Size = new System.Drawing.Size(91, 27);
            this.cmdEnregistrer.TabIndex = 134;
            this.cmdEnregistrer.Text = "Enregistrer";
            this.cmdEnregistrer.UseVisualStyleBackColor = true;
            this.cmdEnregistrer.Click += new System.EventHandler(this.cmdEnregistrer_Click);
            // 
            // cmdEditer
            // 
            this.cmdEditer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEditer.Location = new System.Drawing.Point(4, 30);
            this.cmdEditer.Name = "cmdEditer";
            this.cmdEditer.Size = new System.Drawing.Size(91, 27);
            this.cmdEditer.TabIndex = 136;
            this.cmdEditer.Text = "Editer";
            this.cmdEditer.UseVisualStyleBackColor = true;
            this.cmdEditer.Click += new System.EventHandler(this.cmdEditer_Click);
            // 
            // cmdSupprimer
            // 
            this.cmdSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSupprimer.Enabled = false;
            this.cmdSupprimer.Location = new System.Drawing.Point(4, 86);
            this.cmdSupprimer.Name = "cmdSupprimer";
            this.cmdSupprimer.Size = new System.Drawing.Size(91, 27);
            this.cmdSupprimer.TabIndex = 137;
            this.cmdSupprimer.Text = "Supprimer";
            this.cmdSupprimer.UseVisualStyleBackColor = true;
            this.cmdSupprimer.Click += new System.EventHandler(this.cmdSupprimer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(470, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 232;
            this.label1.Text = "Souscrit le";
            // 
            // dtpDateSouscription
            // 
            this.dtpDateSouscription.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateSouscription.Location = new System.Drawing.Point(529, 7);
            this.dtpDateSouscription.Name = "dtpDateSouscription";
            this.dtpDateSouscription.Size = new System.Drawing.Size(78, 20);
            this.dtpDateSouscription.TabIndex = 231;
            // 
            // nudTauxCommission
            // 
            this.nudTauxCommission.Location = new System.Drawing.Point(358, 112);
            this.nudTauxCommission.Name = "nudTauxCommission";
            this.nudTauxCommission.Size = new System.Drawing.Size(45, 20);
            this.nudTauxCommission.TabIndex = 222;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 140;
            this.label6.Text = "Taux";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(73, 112);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(232, 20);
            this.txtEmail.TabIndex = 230;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 229;
            this.label12.Text = "Email";
            // 
            // txtTelephoneFixe
            // 
            this.txtTelephoneFixe.Location = new System.Drawing.Point(73, 86);
            this.txtTelephoneFixe.Name = "txtTelephoneFixe";
            this.txtTelephoneFixe.Size = new System.Drawing.Size(123, 20);
            this.txtTelephoneFixe.TabIndex = 221;
            this.txtTelephoneFixe.TextChanged += new System.EventHandler(this.txtTelephoneFixe_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 220;
            this.label7.Text = "Fixe";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtTelephoneMobile
            // 
            this.txtTelephoneMobile.Location = new System.Drawing.Point(73, 60);
            this.txtTelephoneMobile.Name = "txtTelephoneMobile";
            this.txtTelephoneMobile.Size = new System.Drawing.Size(123, 20);
            this.txtTelephoneMobile.TabIndex = 139;
            this.txtTelephoneMobile.TextChanged += new System.EventHandler(this.txtTelephoneMobile_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 138;
            this.label5.Text = "Mobile";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(358, 60);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(249, 47);
            this.txtAdresse.TabIndex = 129;
            this.txtAdresse.TextChanged += new System.EventHandler(this.txtAdresse_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(1, 36);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(72, 13);
            this.label22.TabIndex = 126;
            this.label22.Text = "Dénomination";
            // 
            // txtDenomination
            // 
            this.txtDenomination.Location = new System.Drawing.Point(73, 33);
            this.txtDenomination.Name = "txtDenomination";
            this.txtDenomination.Size = new System.Drawing.Size(534, 20);
            this.txtDenomination.TabIndex = 127;
            this.txtDenomination.TextChanged += new System.EventHandler(this.txtDenomination_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 128;
            this.label2.Text = "Adresse";
            // 
            // cmdAjouterMembre
            // 
            this.cmdAjouterMembre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAjouterMembre.Location = new System.Drawing.Point(128, 249);
            this.cmdAjouterMembre.Name = "cmdAjouterMembre";
            this.cmdAjouterMembre.Size = new System.Drawing.Size(127, 27);
            this.cmdAjouterMembre.TabIndex = 139;
            this.cmdAjouterMembre.Text = "Ajouter un membre";
            this.cmdAjouterMembre.UseVisualStyleBackColor = true;
            this.cmdAjouterMembre.Visible = false;
            this.cmdAjouterMembre.Click += new System.EventHandler(this.cmdAjouterMembre_Click);
            // 
            // lvMembres
            // 
            this.lvMembres.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMembres.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader12,
            this.columnHeader11,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lvMembres.FullRowSelect = true;
            this.lvMembres.HideSelection = false;
            this.lvMembres.Location = new System.Drawing.Point(3, 3);
            this.lvMembres.Name = "lvMembres";
            this.lvMembres.Size = new System.Drawing.Size(714, 244);
            this.lvMembres.TabIndex = 132;
            this.lvMembres.UseCompatibleStateImageBehavior = false;
            this.lvMembres.View = System.Windows.Forms.View.Details;
            this.lvMembres.DoubleClick += new System.EventHandler(this.lvMembres_DoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Souscrit le";
            this.columnHeader8.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Prénom et Nom";
            this.columnHeader9.Width = 162;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Tél Mobile";
            this.columnHeader10.Width = 86;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Statut";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Type contrat";
            this.columnHeader11.Width = 74;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Type Villa";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Lot";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Prix de vente";
            this.columnHeader15.Width = 80;
            // 
            // txtNbAdherents
            // 
            this.txtNbAdherents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbAdherents.Location = new System.Drawing.Point(610, 253);
            this.txtNbAdherents.Name = "txtNbAdherents";
            this.txtNbAdherents.Size = new System.Drawing.Size(108, 20);
            this.txtNbAdherents.TabIndex = 131;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(505, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 130;
            this.label3.Text = "Nombre d\'adhérents";
            // 
            // lvCooperatives
            // 
            this.lvCooperatives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCooperatives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader16});
            this.lvCooperatives.FullRowSelect = true;
            this.lvCooperatives.HideSelection = false;
            this.lvCooperatives.Location = new System.Drawing.Point(3, 32);
            this.lvCooperatives.Name = "lvCooperatives";
            this.lvCooperatives.Size = new System.Drawing.Size(318, 411);
            this.lvCooperatives.TabIndex = 0;
            this.lvCooperatives.UseCompatibleStateImageBehavior = false;
            this.lvCooperatives.View = System.Windows.Forms.View.Details;
            this.lvCooperatives.SelectedIndexChanged += new System.EventHandler(this.lvCooperatives_SelectedIndexChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Souscrit le";
            this.columnHeader7.Width = 98;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Dénomination";
            this.columnHeader1.Width = 206;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tél Mobile";
            this.columnHeader2.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tel Fixe";
            this.columnHeader3.Width = 85;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Email";
            this.columnHeader4.Width = 194;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Adresse";
            this.columnHeader5.Width = 290;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Taux";
            this.columnHeader6.Width = 55;
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(978, 457);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(91, 27);
            this.cmdFermer.TabIndex = 138;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(6, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.lvCooperatives);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1062, 448);
            this.splitContainer2.SplitterDistance = 326;
            this.splitContainer2.TabIndex = 139;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(318, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "Liste des coopératives";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Commercial";
            this.columnHeader16.Width = 160;
            // 
            // FrmCooperative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1073, 492);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.cmdFermer);
            this.Name = "FrmCooperative";
            this.Text = "FrmCooperative";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCooperative_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pCommandes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudTauxCommission)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudTauxCommission;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelephoneFixe;
        private System.Windows.Forms.Button cmdNouveau;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTelephoneMobile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSupprimer;
        private System.Windows.Forms.Button cmdEditer;
        private System.Windows.Forms.Button cmdEnregistrer;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtDenomination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvCooperatives;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateSouscription;
        private System.Windows.Forms.TextBox txtNbAdherents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lvMembres;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdAjouterMembre;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Panel pCommandes;
        private System.Windows.Forms.ColumnHeader columnHeader16;
    }
}