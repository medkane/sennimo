namespace prjSenImmoWinform
{
    partial class FrmDetailsRecouvrementDepot
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
            this.label18 = new System.Windows.Forms.Label();
            this.txtDureeDepot = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label14 = new System.Windows.Forms.Label();
            this.txtTypeContrat = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMontantEcheance = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNbEcheances = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPeriodicite = new System.Windows.Forms.TextBox();
            this.txtMontantDepotMinimumRestant = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMontantTotalDu = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMontantDepotMinimumEncaisse = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMontantEcheancesRestant = new System.Windows.Forms.TextBox();
            this.txtNbEcheancesNonSoldees = new System.Windows.Forms.TextBox();
            this.txtMontantEcheancesEncaisse = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNbEcheancesSoldees = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMontantEcheancesDu = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrixDeVente = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTypeVilla = new System.Windows.Forms.TextBox();
            this.txtMontantTotalRestant = new System.Windows.Forms.TextBox();
            this.txtMontantTotalEncaisse = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDepotMinimum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNbEcheancesEchues = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvNotes = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cmdAjouterNote = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdImprimerFactureRelance = new System.Windows.Forms.Button();
            this.cmsNote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cmsNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(293, 100);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(105, 13);
            this.label18.TabIndex = 278;
            this.label18.Text = "Durée dépôt en mois";
            // 
            // txtDureeDepot
            // 
            this.txtDureeDepot.Location = new System.Drawing.Point(401, 97);
            this.txtDureeDepot.Name = "txtDureeDepot";
            this.txtDureeDepot.ReadOnly = true;
            this.txtDureeDepot.Size = new System.Drawing.Size(95, 20);
            this.txtDureeDepot.TabIndex = 277;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.Location = new System.Drawing.Point(5, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(483, 186);
            this.listView1.TabIndex = 275;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Libellé";
            this.columnHeader1.Width = 105;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Montant";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 88;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Encaissé";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 88;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Restant";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 88;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 274;
            this.label14.Text = "Type contrat";
            // 
            // txtTypeContrat
            // 
            this.txtTypeContrat.Location = new System.Drawing.Point(86, 62);
            this.txtTypeContrat.Name = "txtTypeContrat";
            this.txtTypeContrat.ReadOnly = true;
            this.txtTypeContrat.Size = new System.Drawing.Size(95, 20);
            this.txtTypeContrat.TabIndex = 273;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(15, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 8);
            this.groupBox2.TabIndex = 272;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(301, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 271;
            this.label7.Text = "Montant échéance";
            // 
            // txtMontantEcheance
            // 
            this.txtMontantEcheance.Location = new System.Drawing.Point(401, 123);
            this.txtMontantEcheance.Name = "txtMontantEcheance";
            this.txtMontantEcheance.ReadOnly = true;
            this.txtMontantEcheance.Size = new System.Drawing.Size(95, 20);
            this.txtMontantEcheance.TabIndex = 270;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 269;
            this.label6.Text = "Nombre d\'échéances";
            // 
            // txtNbEcheances
            // 
            this.txtNbEcheances.Location = new System.Drawing.Point(115, 124);
            this.txtNbEcheances.Name = "txtNbEcheances";
            this.txtNbEcheances.ReadOnly = true;
            this.txtNbEcheances.Size = new System.Drawing.Size(88, 20);
            this.txtNbEcheances.TabIndex = 268;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 267;
            this.label4.Text = "Peridicité";
            // 
            // txtPeriodicite
            // 
            this.txtPeriodicite.Location = new System.Drawing.Point(115, 98);
            this.txtPeriodicite.Name = "txtPeriodicite";
            this.txtPeriodicite.ReadOnly = true;
            this.txtPeriodicite.Size = new System.Drawing.Size(88, 20);
            this.txtPeriodicite.TabIndex = 266;
            // 
            // txtMontantDepotMinimumRestant
            // 
            this.txtMontantDepotMinimumRestant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantDepotMinimumRestant.Location = new System.Drawing.Point(381, 216);
            this.txtMontantDepotMinimumRestant.Name = "txtMontantDepotMinimumRestant";
            this.txtMontantDepotMinimumRestant.ReadOnly = true;
            this.txtMontantDepotMinimumRestant.Size = new System.Drawing.Size(88, 20);
            this.txtMontantDepotMinimumRestant.TabIndex = 265;
            this.txtMontantDepotMinimumRestant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(69, 289);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(130, 13);
            this.label16.TabIndex = 264;
            this.label16.Text = "TOTAL";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMontantTotalDu
            // 
            this.txtMontantTotalDu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantTotalDu.Location = new System.Drawing.Point(201, 286);
            this.txtMontantTotalDu.Name = "txtMontantTotalDu";
            this.txtMontantTotalDu.ReadOnly = true;
            this.txtMontantTotalDu.Size = new System.Drawing.Size(88, 20);
            this.txtMontantTotalDu.TabIndex = 263;
            this.txtMontantTotalDu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(135)))), ((int)(((byte)(120)))));
            this.label15.Location = new System.Drawing.Point(201, 195);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 20);
            this.label15.TabIndex = 262;
            this.label15.Text = "Echu";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMontantDepotMinimumEncaisse
            // 
            this.txtMontantDepotMinimumEncaisse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantDepotMinimumEncaisse.Location = new System.Drawing.Point(291, 216);
            this.txtMontantDepotMinimumEncaisse.Name = "txtMontantDepotMinimumEncaisse";
            this.txtMontantDepotMinimumEncaisse.ReadOnly = true;
            this.txtMontantDepotMinimumEncaisse.Size = new System.Drawing.Size(88, 20);
            this.txtMontantDepotMinimumEncaisse.TabIndex = 261;
            this.txtMontantDepotMinimumEncaisse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(135)))), ((int)(((byte)(120)))));
            this.label9.Location = new System.Drawing.Point(381, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 20);
            this.label9.TabIndex = 260;
            this.label9.Text = "Restant";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMontantEcheancesRestant
            // 
            this.txtMontantEcheancesRestant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantEcheancesRestant.Location = new System.Drawing.Point(381, 262);
            this.txtMontantEcheancesRestant.Name = "txtMontantEcheancesRestant";
            this.txtMontantEcheancesRestant.ReadOnly = true;
            this.txtMontantEcheancesRestant.Size = new System.Drawing.Size(88, 20);
            this.txtMontantEcheancesRestant.TabIndex = 259;
            this.txtMontantEcheancesRestant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNbEcheancesNonSoldees
            // 
            this.txtNbEcheancesNonSoldees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbEcheancesNonSoldees.Location = new System.Drawing.Point(381, 239);
            this.txtNbEcheancesNonSoldees.Name = "txtNbEcheancesNonSoldees";
            this.txtNbEcheancesNonSoldees.ReadOnly = true;
            this.txtNbEcheancesNonSoldees.Size = new System.Drawing.Size(88, 20);
            this.txtNbEcheancesNonSoldees.TabIndex = 257;
            this.txtNbEcheancesNonSoldees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMontantEcheancesEncaisse
            // 
            this.txtMontantEcheancesEncaisse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantEcheancesEncaisse.Location = new System.Drawing.Point(291, 262);
            this.txtMontantEcheancesEncaisse.Name = "txtMontantEcheancesEncaisse";
            this.txtMontantEcheancesEncaisse.ReadOnly = true;
            this.txtMontantEcheancesEncaisse.Size = new System.Drawing.Size(88, 20);
            this.txtMontantEcheancesEncaisse.TabIndex = 255;
            this.txtMontantEcheancesEncaisse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(69, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 254;
            this.label5.Text = "Montant des échéances";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNbEcheancesSoldees
            // 
            this.txtNbEcheancesSoldees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbEcheancesSoldees.Location = new System.Drawing.Point(291, 239);
            this.txtNbEcheancesSoldees.Name = "txtNbEcheancesSoldees";
            this.txtNbEcheancesSoldees.ReadOnly = true;
            this.txtNbEcheancesSoldees.Size = new System.Drawing.Size(88, 20);
            this.txtNbEcheancesSoldees.TabIndex = 253;
            this.txtNbEcheancesSoldees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(135)))), ((int)(((byte)(120)))));
            this.label2.Location = new System.Drawing.Point(291, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 252;
            this.label2.Text = "Encaissé";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMontantEcheancesDu
            // 
            this.txtMontantEcheancesDu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantEcheancesDu.Location = new System.Drawing.Point(201, 262);
            this.txtMontantEcheancesDu.Name = "txtMontantEcheancesDu";
            this.txtMontantEcheancesDu.ReadOnly = true;
            this.txtMontantEcheancesDu.Size = new System.Drawing.Size(88, 20);
            this.txtMontantEcheancesDu.TabIndex = 251;
            this.txtMontantEcheancesDu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(329, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 250;
            this.label13.Text = "Prix de vente";
            // 
            // txtPrixDeVente
            // 
            this.txtPrixDeVente.Location = new System.Drawing.Point(408, 61);
            this.txtPrixDeVente.Name = "txtPrixDeVente";
            this.txtPrixDeVente.ReadOnly = true;
            this.txtPrixDeVente.Size = new System.Drawing.Size(88, 20);
            this.txtPrixDeVente.TabIndex = 249;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 247;
            this.label10.Text = "Type villa";
            // 
            // txtTypeVilla
            // 
            this.txtTypeVilla.Location = new System.Drawing.Point(86, 36);
            this.txtTypeVilla.Name = "txtTypeVilla";
            this.txtTypeVilla.ReadOnly = true;
            this.txtTypeVilla.Size = new System.Drawing.Size(88, 20);
            this.txtTypeVilla.TabIndex = 246;
            // 
            // txtMontantTotalRestant
            // 
            this.txtMontantTotalRestant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantTotalRestant.Location = new System.Drawing.Point(381, 286);
            this.txtMontantTotalRestant.Name = "txtMontantTotalRestant";
            this.txtMontantTotalRestant.ReadOnly = true;
            this.txtMontantTotalRestant.Size = new System.Drawing.Size(88, 20);
            this.txtMontantTotalRestant.TabIndex = 19;
            this.txtMontantTotalRestant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMontantTotalEncaisse
            // 
            this.txtMontantTotalEncaisse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMontantTotalEncaisse.Location = new System.Drawing.Point(291, 286);
            this.txtMontantTotalEncaisse.Name = "txtMontantTotalEncaisse";
            this.txtMontantTotalEncaisse.ReadOnly = true;
            this.txtMontantTotalEncaisse.Size = new System.Drawing.Size(88, 20);
            this.txtMontantTotalEncaisse.TabIndex = 17;
            this.txtMontantTotalEncaisse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(67, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Dépôt minimum";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDepotMinimum
            // 
            this.txtDepotMinimum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepotMinimum.Location = new System.Drawing.Point(201, 216);
            this.txtDepotMinimum.Name = "txtDepotMinimum";
            this.txtDepotMinimum.ReadOnly = true;
            this.txtDepotMinimum.Size = new System.Drawing.Size(88, 20);
            this.txtDepotMinimum.TabIndex = 15;
            this.txtDepotMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(69, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nombre d\'échéances";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNbEcheancesEchues
            // 
            this.txtNbEcheancesEchues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbEcheancesEchues.Location = new System.Drawing.Point(201, 239);
            this.txtNbEcheancesEchues.Name = "txtNbEcheancesEchues";
            this.txtNbEcheancesEchues.ReadOnly = true;
            this.txtNbEcheancesEchues.Size = new System.Drawing.Size(88, 20);
            this.txtNbEcheancesEchues.TabIndex = 11;
            this.txtNbEcheancesEchues.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(50, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Client";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(86, 10);
            this.txtClient.Name = "txtClient";
            this.txtClient.ReadOnly = true;
            this.txtClient.Size = new System.Drawing.Size(410, 20);
            this.txtClient.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(7, 7);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(519, 520);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 250;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDureeDepot);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.txtClient);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtTypeVilla);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtPrixDeVente);
            this.panel1.Controls.Add(this.txtTypeContrat);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.txtPeriodicite);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtMontantEcheance);
            this.panel1.Controls.Add(this.txtNbEcheances);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 153);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(408, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(88, 20);
            this.textBox1.TabIndex = 279;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 280;
            this.label3.Text = "Position";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Location = new System.Drawing.Point(5, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(509, 346);
            this.panel3.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(502, 338);
            this.tabControl1.TabIndex = 276;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtMontantTotalEncaisse);
            this.tabPage1.Controls.Add(this.txtMontantTotalRestant);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtMontantTotalDu);
            this.tabPage1.Controls.Add(this.txtMontantEcheancesEncaisse);
            this.tabPage1.Controls.Add(this.txtMontantDepotMinimumRestant);
            this.tabPage1.Controls.Add(this.txtNbEcheancesSoldees);
            this.tabPage1.Controls.Add(this.txtNbEcheancesEchues);
            this.tabPage1.Controls.Add(this.txtNbEcheancesNonSoldees);
            this.tabPage1.Controls.Add(this.txtMontantEcheancesDu);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMontantEcheancesRestant);
            this.tabPage1.Controls.Add(this.txtDepotMinimum);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtMontantDepotMinimumEncaisse);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(494, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Niveau de recouvrement";
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
            this.tabPage2.Size = new System.Drawing.Size(494, 312);
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
            this.lvNotes.Location = new System.Drawing.Point(6, 6);
            this.lvNotes.Name = "lvNotes";
            this.lvNotes.Size = new System.Drawing.Size(482, 247);
            this.lvNotes.TabIndex = 287;
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
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.BackColor = System.Drawing.SystemColors.Info;
            this.txtNote.Location = new System.Drawing.Point(7, 256);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(433, 49);
            this.txtNote.TabIndex = 286;
            // 
            // cmdAjouterNote
            // 
            this.cmdAjouterNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAjouterNote.Image = global::prjSenImmoWinform.Properties.Resources.sticky_notes_24;
            this.cmdAjouterNote.Location = new System.Drawing.Point(442, 255);
            this.cmdAjouterNote.Name = "cmdAjouterNote";
            this.cmdAjouterNote.Size = new System.Drawing.Size(47, 51);
            this.cmdAjouterNote.TabIndex = 285;
            this.cmdAjouterNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAjouterNote.UseVisualStyleBackColor = true;
            this.cmdAjouterNote.Click += new System.EventHandler(this.cmdAjouterNote_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(447, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 28);
            this.button1.TabIndex = 251;
            this.button1.Text = "Fermer";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdImprimerFactureRelance
            // 
            this.cmdImprimerFactureRelance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdImprimerFactureRelance.Image = global::prjSenImmoWinform.Properties.Resources.printer_16;
            this.cmdImprimerFactureRelance.Location = new System.Drawing.Point(7, 534);
            this.cmdImprimerFactureRelance.Name = "cmdImprimerFactureRelance";
            this.cmdImprimerFactureRelance.Size = new System.Drawing.Size(96, 27);
            this.cmdImprimerFactureRelance.TabIndex = 243;
            this.cmdImprimerFactureRelance.Text = "Relance";
            this.cmdImprimerFactureRelance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdImprimerFactureRelance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdImprimerFactureRelance.UseVisualStyleBackColor = true;
            this.cmdImprimerFactureRelance.Click += new System.EventHandler(this.cmdImprimerFactureRelance_Click);
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
            this.modifierToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modifierToolStripMenuItem.Text = "Modifier";
            this.modifierToolStripMenuItem.Click += new System.EventHandler(this.modifierToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // FrmDetailsRecouvrementDepot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(532, 564);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdImprimerFactureRelance);
            this.Name = "FrmDetailsRecouvrementDepot";
            this.Text = "FrmDetailsRecouvrementDepot";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.cmsNote.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDureeDepot;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTypeContrat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMontantEcheance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNbEcheances;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPeriodicite;
        private System.Windows.Forms.TextBox txtMontantDepotMinimumRestant;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMontantTotalDu;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMontantDepotMinimumEncaisse;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMontantEcheancesRestant;
        private System.Windows.Forms.TextBox txtNbEcheancesNonSoldees;
        private System.Windows.Forms.TextBox txtMontantEcheancesEncaisse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNbEcheancesSoldees;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMontantEcheancesDu;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrixDeVente;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTypeVilla;
        private System.Windows.Forms.TextBox txtMontantTotalRestant;
        private System.Windows.Forms.TextBox txtMontantTotalEncaisse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDepotMinimum;
        private System.Windows.Forms.Button cmdImprimerFactureRelance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNbEcheancesEchues;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
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