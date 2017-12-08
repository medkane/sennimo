namespace prjSenImmoWinform
{
    partial class FrmMesActivitesCommerciales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMesActivitesCommerciales));
            this.lvActivitesCommerciales = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmActivite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.annulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmdFermer = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpDateFinCalendar = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateDebutCalendar = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.cmdRechercher = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEchueNonExecutee = new System.Windows.Forms.CheckBox();
            this.chkAnnulee = new System.Windows.Forms.CheckBox();
            this.chkExecutee = new System.Windows.Forms.CheckBox();
            this.chkNonEchue = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbCommercial = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkCommercial = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmActivite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvActivitesCommerciales
            // 
            this.lvActivitesCommerciales.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvActivitesCommerciales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvActivitesCommerciales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvActivitesCommerciales.ContextMenuStrip = this.cmActivite;
            this.lvActivitesCommerciales.FullRowSelect = true;
            this.lvActivitesCommerciales.HoverSelection = true;
            this.lvActivitesCommerciales.Location = new System.Drawing.Point(4, 3);
            this.lvActivitesCommerciales.Name = "lvActivitesCommerciales";
            this.lvActivitesCommerciales.Size = new System.Drawing.Size(931, 334);
            this.lvActivitesCommerciales.SmallImageList = this.imageList1;
            this.lvActivitesCommerciales.TabIndex = 213;
            this.lvActivitesCommerciales.UseCompatibleStateImageBehavior = false;
            this.lvActivitesCommerciales.View = System.Windows.Forms.View.Details;
            this.lvActivitesCommerciales.SelectedIndexChanged += new System.EventHandler(this.lvActivitesCommerciales_SelectedIndexChanged);
            this.lvActivitesCommerciales.DoubleClick += new System.EventHandler(this.lvActivitesCommerciales_DoubleClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date";
            this.columnHeader5.Width = 85;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Heure";
            this.columnHeader1.Width = 76;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Client";
            this.columnHeader2.Width = 184;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Activité";
            this.columnHeader3.Width = 136;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Détails";
            this.columnHeader4.Width = 500;
            // 
            // cmActivite
            // 
            this.cmActivite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.annulerToolStripMenuItem,
            this.cloturerToolStripMenuItem});
            this.cmActivite.Name = "cmActivite";
            this.cmActivite.Size = new System.Drawing.Size(118, 48);
            this.cmActivite.Opening += new System.ComponentModel.CancelEventHandler(this.cmActivite_Opening);
            // 
            // annulerToolStripMenuItem
            // 
            this.annulerToolStripMenuItem.Name = "annulerToolStripMenuItem";
            this.annulerToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.annulerToolStripMenuItem.Text = "Annuler";
            this.annulerToolStripMenuItem.Click += new System.EventHandler(this.annulerToolStripMenuItem_Click);
            // 
            // cloturerToolStripMenuItem
            // 
            this.cloturerToolStripMenuItem.Name = "cloturerToolStripMenuItem";
            this.cloturerToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.cloturerToolStripMenuItem.Text = "Clôturer";
            this.cloturerToolStripMenuItem.Click += new System.EventHandler(this.cloturerToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "triang_yellow.ico");
            this.imageList1.Images.SetKeyName(1, "triang_green.ico");
            this.imageList1.Images.SetKeyName(2, "triang_red.ico");
            this.imageList1.Images.SetKeyName(3, "point_green.ico");
            this.imageList1.Images.SetKeyName(4, "point_red.ico");
            this.imageList1.Images.SetKeyName(5, "point_yellow.ico");
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(854, 443);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(92, 31);
            this.cmdFermer.TabIndex = 214;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(705, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 220;
            this.label10.Text = "au";
            // 
            // dtpDateFinCalendar
            // 
            this.dtpDateFinCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDateFinCalendar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFinCalendar.Location = new System.Drawing.Point(727, 10);
            this.dtpDateFinCalendar.Name = "dtpDateFinCalendar";
            this.dtpDateFinCalendar.Size = new System.Drawing.Size(98, 20);
            this.dtpDateFinCalendar.TabIndex = 217;
            this.dtpDateFinCalendar.ValueChanged += new System.EventHandler(this.dtpDateFinCalendar_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(524, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 219;
            this.label4.Text = "Période du";
            // 
            // dtpDateDebutCalendar
            // 
            this.dtpDateDebutCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDateDebutCalendar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDebutCalendar.Location = new System.Drawing.Point(595, 10);
            this.dtpDateDebutCalendar.Name = "dtpDateDebutCalendar";
            this.dtpDateDebutCalendar.Size = new System.Drawing.Size(107, 20);
            this.dtpDateDebutCalendar.TabIndex = 218;
            this.dtpDateDebutCalendar.ValueChanged += new System.EventHandler(this.dtpDateDebutCalendar_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(197)))), ((int)(((byte)(190)))));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(5, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cmbProjets);
            this.splitContainer1.Panel1.Controls.Add(this.cmdRechercher);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.dtpDateDebutCalendar);
            this.splitContainer1.Panel1.Controls.Add(this.dtpDateFinCalendar);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chkEchueNonExecutee);
            this.splitContainer1.Panel2.Controls.Add(this.chkAnnulee);
            this.splitContainer1.Panel2.Controls.Add(this.chkExecutee);
            this.splitContainer1.Panel2.Controls.Add(this.chkNonEchue);
            this.splitContainer1.Panel2.Controls.Add(this.lvActivitesCommerciales);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.cmbCommercial);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.chkCommercial);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Size = new System.Drawing.Size(939, 427);
            this.splitContainer1.SplitterDistance = 38;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 221;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 227;
            this.label3.Text = "Projet";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(48, 9);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(138, 21);
            this.cmbProjets.TabIndex = 226;
            this.cmbProjets.SelectedIndexChanged += new System.EventHandler(this.cmbProjets_SelectedIndexChanged);
            this.cmbProjets.Validated += new System.EventHandler(this.cmbProjets_Validated);
            // 
            // cmdRechercher
            // 
            this.cmdRechercher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRechercher.Location = new System.Drawing.Point(831, 8);
            this.cmdRechercher.Name = "cmdRechercher";
            this.cmdRechercher.Size = new System.Drawing.Size(93, 23);
            this.cmdRechercher.TabIndex = 221;
            this.cmdRechercher.Text = "Rechercher";
            this.cmdRechercher.UseVisualStyleBackColor = true;
            this.cmdRechercher.Click += new System.EventHandler(this.cmdRechercher_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(930, 30);
            this.label1.TabIndex = 212;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkEchueNonExecutee
            // 
            this.chkEchueNonExecutee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEchueNonExecutee.AutoSize = true;
            this.chkEchueNonExecutee.Location = new System.Drawing.Point(801, 357);
            this.chkEchueNonExecutee.Name = "chkEchueNonExecutee";
            this.chkEchueNonExecutee.Size = new System.Drawing.Size(125, 17);
            this.chkEchueNonExecutee.TabIndex = 248;
            this.chkEchueNonExecutee.Text = "Echue non exécutée";
            this.chkEchueNonExecutee.UseVisualStyleBackColor = true;
            this.chkEchueNonExecutee.CheckedChanged += new System.EventHandler(this.chkEchueNonExecutee_CheckedChanged);
            // 
            // chkAnnulee
            // 
            this.chkAnnulee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAnnulee.AutoSize = true;
            this.chkAnnulee.Location = new System.Drawing.Point(685, 357);
            this.chkAnnulee.Name = "chkAnnulee";
            this.chkAnnulee.Size = new System.Drawing.Size(65, 17);
            this.chkAnnulee.TabIndex = 247;
            this.chkAnnulee.Text = "Annulée";
            this.chkAnnulee.UseVisualStyleBackColor = true;
            this.chkAnnulee.CheckedChanged += new System.EventHandler(this.chkAnnulee_CheckedChanged);
            // 
            // chkExecutee
            // 
            this.chkExecutee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkExecutee.AutoSize = true;
            this.chkExecutee.Location = new System.Drawing.Point(563, 357);
            this.chkExecutee.Name = "chkExecutee";
            this.chkExecutee.Size = new System.Drawing.Size(71, 17);
            this.chkExecutee.TabIndex = 246;
            this.chkExecutee.Text = "Exécutée";
            this.chkExecutee.UseVisualStyleBackColor = true;
            this.chkExecutee.CheckedChanged += new System.EventHandler(this.chkExecutee_CheckedChanged);
            // 
            // chkNonEchue
            // 
            this.chkNonEchue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkNonEchue.AutoSize = true;
            this.chkNonEchue.Location = new System.Drawing.Point(433, 357);
            this.chkNonEchue.Name = "chkNonEchue";
            this.chkNonEchue.Size = new System.Drawing.Size(79, 17);
            this.chkNonEchue.TabIndex = 245;
            this.chkNonEchue.Text = "Non échue";
            this.chkNonEchue.UseVisualStyleBackColor = true;
            this.chkNonEchue.CheckedChanged += new System.EventHandler(this.chkNonEchue_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Location = new System.Drawing.Point(382, 347);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(4, 32);
            this.groupBox4.TabIndex = 244;
            this.groupBox4.TabStop = false;
            // 
            // cmbCommercial
            // 
            this.cmbCommercial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCommercial.FormattingEnabled = true;
            this.cmbCommercial.Location = new System.Drawing.Point(89, 354);
            this.cmbCommercial.Name = "cmbCommercial";
            this.cmbCommercial.Size = new System.Drawing.Size(160, 21);
            this.cmbCommercial.TabIndex = 242;
            this.cmbCommercial.Visible = false;
            this.cmbCommercial.SelectedIndexChanged += new System.EventHandler(this.cmbCommercial_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(388, 358);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(43, 13);
            this.panel1.TabIndex = 223;
            // 
            // chkCommercial
            // 
            this.chkCommercial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCommercial.AutoSize = true;
            this.chkCommercial.Location = new System.Drawing.Point(6, 356);
            this.chkCommercial.Name = "chkCommercial";
            this.chkCommercial.Size = new System.Drawing.Size(80, 17);
            this.chkCommercial.TabIndex = 243;
            this.chkCommercial.Text = "Commercial";
            this.chkCommercial.UseVisualStyleBackColor = true;
            this.chkCommercial.CheckedChanged += new System.EventHandler(this.chkCommercial_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(181)))), ((int)(((byte)(0)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(518, 358);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(43, 13);
            this.panel2.TabIndex = 224;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(130, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 222;
            this.label2.Text = "Légende";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(179, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(752, 9);
            this.groupBox3.TabIndex = 241;
            this.groupBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::prjSenImmoWinform.Properties.Resources.triang_green;
            this.pictureBox1.Location = new System.Drawing.Point(249, 355);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 231;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(640, 358);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(43, 13);
            this.panel3.TabIndex = 225;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::prjSenImmoWinform.Properties.Resources.triang_yellow;
            this.pictureBox2.Location = new System.Drawing.Point(188, 355);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 232;
            this.pictureBox2.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Salmon;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(756, 358);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(43, 13);
            this.panel4.TabIndex = 226;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Image = global::prjSenImmoWinform.Properties.Resources.triang_red;
            this.pictureBox3.Location = new System.Drawing.Point(323, 355);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(18, 18);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 233;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(179, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 9);
            this.groupBox2.TabIndex = 238;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(443, -2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 239;
            this.label12.Text = "Statut";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Location = new System.Drawing.Point(253, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(4, 35);
            this.groupBox1.TabIndex = 237;
            this.groupBox1.TabStop = false;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(77, -2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 240;
            this.label13.Text = "Priorité";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(270, 358);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 234;
            this.label8.Text = "Normale";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(344, 358);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 236;
            this.label11.Text = "Haute";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(209, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 235;
            this.label9.Text = "Faible";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // FrmMesActivitesCommerciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 479);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdFermer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMesActivitesCommerciales";
            this.Text = "Mes Activites Commerciales";
            this.Load += new System.EventHandler(this.FrmMesActivitesCommerciales_Load);
            this.cmActivite.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvActivitesCommerciales;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpDateFinCalendar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateDebutCalendar;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip cmActivite;
        private System.Windows.Forms.ToolStripMenuItem annulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloturerToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbCommercial;
        private System.Windows.Forms.CheckBox chkCommercial;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkEchueNonExecutee;
        private System.Windows.Forms.CheckBox chkAnnulee;
        private System.Windows.Forms.CheckBox chkExecutee;
        private System.Windows.Forms.CheckBox chkNonEchue;
        private System.Windows.Forms.Button cmdRechercher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbProjets;
        private System.Windows.Forms.Label label1;
    }
}