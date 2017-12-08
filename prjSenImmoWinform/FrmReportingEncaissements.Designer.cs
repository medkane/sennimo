namespace prjSenImmoWinform
{
    partial class FrmReportingEncaissements
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.cmdRecouvrement = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpDateFin = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvEncaissementNewVentesRecouvrement = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chartEncaissements = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgResultDepot = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgResultResa = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTotalResa = new System.Windows.Forms.TextBox();
            this.txtTotalDepot = new System.Windows.Forms.TextBox();
            this.dtpDateDebut = new System.Windows.Forms.DateTimePicker();
            this.txtTotalVentes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEncaissements)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultDepot)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultResa)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer5
            // 
            this.splitContainer5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer5.Location = new System.Drawing.Point(8, 6);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer5.Panel1.Controls.Add(this.panel4);
            this.splitContainer5.Panel1.Controls.Add(this.button2);
            this.splitContainer5.Panel1.Controls.Add(this.label15);
            this.splitContainer5.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer5_Panel1_Paint);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.label8);
            this.splitContainer5.Panel2.Controls.Add(this.textBox4);
            this.splitContainer5.Panel2.Controls.Add(this.textBox5);
            this.splitContainer5.Panel2.Controls.Add(this.textBox6);
            this.splitContainer5.Panel2.Controls.Add(this.panel1);
            this.splitContainer5.Panel2.Controls.Add(this.panel2);
            this.splitContainer5.Panel2.Controls.Add(this.panel3);
            this.splitContainer5.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer5_Panel2_Paint);
            this.splitContainer5.Size = new System.Drawing.Size(1144, 581);
            this.splitContainer5.SplitterDistance = 43;
            this.splitContainer5.TabIndex = 29;
            // 
            // cmdRecouvrement
            // 
            this.cmdRecouvrement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRecouvrement.Location = new System.Drawing.Point(982, 2);
            this.cmdRecouvrement.Name = "cmdRecouvrement";
            this.cmdRecouvrement.Size = new System.Drawing.Size(141, 27);
            this.cmdRecouvrement.TabIndex = 37;
            this.cmdRecouvrement.Text = "Etat Global Recouvrement";
            this.cmdRecouvrement.UseVisualStyleBackColor = true;
            this.cmdRecouvrement.Click += new System.EventHandler(this.cmdRecouvrement_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(929, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 27);
            this.button2.TabIndex = 36;
            this.button2.Text = "Etat Global Recouvrement Dépôt";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(158, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "Date de référence";
            // 
            // dtpDateFin
            // 
            this.dtpDateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFin.Location = new System.Drawing.Point(115, 6);
            this.dtpDateFin.Name = "dtpDateFin";
            this.dtpDateFin.Size = new System.Drawing.Size(95, 20);
            this.dtpDateFin.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(31, 505);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 63;
            this.label8.Text = "Total";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox4.Location = new System.Drawing.Point(71, 505);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(59, 20);
            this.textBox4.TabIndex = 64;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox5.Location = new System.Drawing.Point(133, 505);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(57, 20);
            this.textBox5.TabIndex = 65;
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox6.Location = new System.Drawing.Point(193, 505);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(59, 20);
            this.textBox6.TabIndex = 66;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lvEncaissementNewVentesRecouvrement);
            this.panel1.Controls.Add(this.chartEncaissements);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(605, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 523);
            this.panel1.TabIndex = 62;
            // 
            // lvEncaissementNewVentesRecouvrement
            // 
            this.lvEncaissementNewVentesRecouvrement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEncaissementNewVentesRecouvrement.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvEncaissementNewVentesRecouvrement.Location = new System.Drawing.Point(3, 309);
            this.lvEncaissementNewVentesRecouvrement.Name = "lvEncaissementNewVentesRecouvrement";
            this.lvEncaissementNewVentesRecouvrement.Size = new System.Drawing.Size(524, 181);
            this.lvEncaissementNewVentesRecouvrement.TabIndex = 49;
            this.lvEncaissementNewVentesRecouvrement.UseCompatibleStateImageBehavior = false;
            this.lvEncaissementNewVentesRecouvrement.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Période";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nouvelles ventes";
            this.columnHeader2.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Recouvrement";
            this.columnHeader3.Width = 110;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Total";
            this.columnHeader4.Width = 110;
            // 
            // chartEncaissements
            // 
            this.chartEncaissements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartEncaissements.BorderlineColor = System.Drawing.SystemColors.MenuText;
            this.chartEncaissements.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Name = "ChartArea1";
            this.chartEncaissements.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartEncaissements.Legends.Add(legend1);
            this.chartEncaissements.Location = new System.Drawing.Point(3, 28);
            this.chartEncaissements.Name = "chartEncaissements";
            this.chartEncaissements.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Résa";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Dépôt";
            this.chartEncaissements.Series.Add(series1);
            this.chartEncaissements.Series.Add(series2);
            this.chartEncaissements.Size = new System.Drawing.Size(524, 275);
            this.chartEncaissements.TabIndex = 48;
            this.chartEncaissements.Text = "chart1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 495);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "Total";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(850, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Période du";
            this.label4.Visible = false;
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox7.Location = new System.Drawing.Point(53, 495);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(59, 20);
            this.textBox7.TabIndex = 31;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox8.Location = new System.Drawing.Point(115, 495);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(57, 20);
            this.textBox8.TabIndex = 32;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(911, 68);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(95, 20);
            this.dateTimePicker1.TabIndex = 32;
            this.dateTimePicker1.Visible = false;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox9.Location = new System.Drawing.Point(175, 495);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(59, 20);
            this.textBox9.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(197)))), ((int)(((byte)(190)))));
            this.label5.Location = new System.Drawing.Point(1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(544, 25);
            this.label5.TabIndex = 47;
            this.label5.Text = "Encaissements nouvelles ventes/recouvrement";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dgResultDepot);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(2, 344);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 153);
            this.panel2.TabIndex = 61;
            // 
            // dgResultDepot
            // 
            this.dgResultDepot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgResultDepot.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgResultDepot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResultDepot.Location = new System.Drawing.Point(3, 27);
            this.dgResultDepot.Name = "dgResultDepot";
            this.dgResultDepot.Size = new System.Drawing.Size(590, 119);
            this.dgResultDepot.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(197)))), ((int)(((byte)(190)))));
            this.label7.Location = new System.Drawing.Point(-4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(613, 25);
            this.label7.TabIndex = 47;
            this.label7.Text = "Recouvrement clients dépôt";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.dgResultResa);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.txtTotalResa);
            this.panel3.Controls.Add(this.txtTotalDepot);
            this.panel3.Controls.Add(this.dtpDateDebut);
            this.panel3.Controls.Add(this.txtTotalVentes);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(599, 336);
            this.panel3.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "Total";
            // 
            // dgResultResa
            // 
            this.dgResultResa.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgResultResa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResultResa.Location = new System.Drawing.Point(2, 28);
            this.dgResultResa.Name = "dgResultResa";
            this.dgResultResa.Size = new System.Drawing.Size(590, 275);
            this.dgResultResa.TabIndex = 27;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(850, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Période du";
            this.label16.Visible = false;
            // 
            // txtTotalResa
            // 
            this.txtTotalResa.Location = new System.Drawing.Point(66, 309);
            this.txtTotalResa.Name = "txtTotalResa";
            this.txtTotalResa.Size = new System.Drawing.Size(59, 20);
            this.txtTotalResa.TabIndex = 31;
            // 
            // txtTotalDepot
            // 
            this.txtTotalDepot.Location = new System.Drawing.Point(128, 309);
            this.txtTotalDepot.Name = "txtTotalDepot";
            this.txtTotalDepot.Size = new System.Drawing.Size(57, 20);
            this.txtTotalDepot.TabIndex = 32;
            // 
            // dtpDateDebut
            // 
            this.dtpDateDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDebut.Location = new System.Drawing.Point(911, 68);
            this.dtpDateDebut.Name = "dtpDateDebut";
            this.dtpDateDebut.Size = new System.Drawing.Size(95, 20);
            this.dtpDateDebut.TabIndex = 32;
            this.dtpDateDebut.Visible = false;
            // 
            // txtTotalVentes
            // 
            this.txtTotalVentes.Location = new System.Drawing.Point(188, 309);
            this.txtTotalVentes.Name = "txtTotalVentes";
            this.txtTotalVentes.Size = new System.Drawing.Size(59, 20);
            this.txtTotalVentes.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(197)))), ((int)(((byte)(190)))));
            this.label1.Location = new System.Drawing.Point(-4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 25);
            this.label1.TabIndex = 47;
            this.label1.Text = "Recouvrement clients réservation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFermer.Location = new System.Drawing.Point(1088, 594);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(64, 30);
            this.cmdFermer.TabIndex = 64;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            this.cmdFermer.Click += new System.EventHandler(this.cmdFermer_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Beige;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.dtpDateFin);
            this.panel4.Controls.Add(this.cmdRecouvrement);
            this.panel4.Location = new System.Drawing.Point(4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1133, 33);
            this.panel4.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Date de référence";
            // 
            // FrmReportingEncaissements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1158, 636);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.splitContainer5);
            this.Name = "FrmReportingEncaissements";
            this.Text = "FrmReportingEncaissements";
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEncaissements)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResultDepot)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultResa)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.DataGridView dgResultDepot;
        private System.Windows.Forms.DataGridView dgResultResa;
        private System.Windows.Forms.Button cmdRecouvrement;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpDateFin;
        private System.Windows.Forms.DateTimePicker dtpDateDebut;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalResa;
        private System.Windows.Forms.TextBox txtTotalDepot;
        private System.Windows.Forms.TextBox txtTotalVentes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEncaissements;
        private System.Windows.Forms.ListView lvEncaissementNewVentesRecouvrement;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
    }
}