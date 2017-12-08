namespace prjSenImmoWinform
{
    partial class FrmReportingTechnique
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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbNombreDeVillas = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDateFinTechnique = new System.Windows.Forms.DateTimePicker();
            this.dtpDateDebutTechnique = new System.Windows.Forms.DateTimePicker();
            this.cmbIlotsTechnique = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdReportingtechnique = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.dgReportingTechnique = new System.Windows.Forms.DataGridView();
            this.cmdFermer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportingTechnique)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Location = new System.Drawing.Point(12, 12);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(169)))), ((int)(((byte)(152)))));
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            this.splitContainer3.Panel1.Controls.Add(this.lbNombreDeVillas);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.label23);
            this.splitContainer3.Panel2.Controls.Add(this.dgReportingTechnique);
            this.splitContainer3.Size = new System.Drawing.Size(1064, 621);
            this.splitContainer3.SplitterDistance = 51;
            this.splitContainer3.TabIndex = 24;
            // 
            // lbNombreDeVillas
            // 
            this.lbNombreDeVillas.AutoSize = true;
            this.lbNombreDeVillas.Location = new System.Drawing.Point(88, 67);
            this.lbNombreDeVillas.Name = "lbNombreDeVillas";
            this.lbNombreDeVillas.Size = new System.Drawing.Size(0, 13);
            this.lbNombreDeVillas.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(168, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "au";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Période du";
            // 
            // dtpDateFinTechnique
            // 
            this.dtpDateFinTechnique.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFinTechnique.Location = new System.Drawing.Point(192, 8);
            this.dtpDateFinTechnique.Name = "dtpDateFinTechnique";
            this.dtpDateFinTechnique.Size = new System.Drawing.Size(95, 20);
            this.dtpDateFinTechnique.TabIndex = 26;
            // 
            // dtpDateDebutTechnique
            // 
            this.dtpDateDebutTechnique.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateDebutTechnique.Location = new System.Drawing.Point(67, 8);
            this.dtpDateDebutTechnique.Name = "dtpDateDebutTechnique";
            this.dtpDateDebutTechnique.Size = new System.Drawing.Size(95, 20);
            this.dtpDateDebutTechnique.TabIndex = 25;
            // 
            // cmbIlotsTechnique
            // 
            this.cmbIlotsTechnique.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cmbIlotsTechnique.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIlotsTechnique.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIlotsTechnique.FormattingEnabled = true;
            this.cmbIlotsTechnique.Location = new System.Drawing.Point(325, 8);
            this.cmbIlotsTechnique.Name = "cmbIlotsTechnique";
            this.cmbIlotsTechnique.Size = new System.Drawing.Size(220, 21);
            this.cmbIlotsTechnique.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Ilôt";
            // 
            // cmdReportingtechnique
            // 
            this.cmdReportingtechnique.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReportingtechnique.Location = new System.Drawing.Point(903, 5);
            this.cmdReportingtechnique.Name = "cmdReportingtechnique";
            this.cmdReportingtechnique.Size = new System.Drawing.Size(144, 27);
            this.cmdReportingtechnique.TabIndex = 22;
            this.cmdReportingtechnique.Text = "Reporting technique";
            this.cmdReportingtechnique.UseVisualStyleBackColor = true;
            this.cmdReportingtechnique.Click += new System.EventHandler(this.cmdReportingtechnique_Click_1);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(197)))), ((int)(((byte)(190)))));
            this.label23.Location = new System.Drawing.Point(-3, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1071, 25);
            this.label23.TabIndex = 48;
            this.label23.Text = "Reporting Technique";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgReportingTechnique
            // 
            this.dgReportingTechnique.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgReportingTechnique.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgReportingTechnique.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgReportingTechnique.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReportingTechnique.Location = new System.Drawing.Point(4, 28);
            this.dgReportingTechnique.Name = "dgReportingTechnique";
            this.dgReportingTechnique.Size = new System.Drawing.Size(1054, 530);
            this.dgReportingTechnique.TabIndex = 2;
            // 
            // cmdFermer
            // 
            this.cmdFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdFermer.Location = new System.Drawing.Point(981, 645);
            this.cmdFermer.Name = "cmdFermer";
            this.cmdFermer.Size = new System.Drawing.Size(85, 30);
            this.cmdFermer.TabIndex = 64;
            this.cmdFermer.Text = "Fermer";
            this.cmdFermer.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.Controls.Add(this.cmbIlotsTechnique);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmdReportingtechnique);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpDateDebutTechnique);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtpDateFinTechnique);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 39);
            this.panel1.TabIndex = 30;
            // 
            // FrmReportingTechnique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 687);
            this.Controls.Add(this.cmdFermer);
            this.Controls.Add(this.splitContainer3);
            this.Name = "FrmReportingTechnique";
            this.Text = "FrmReportingTechnique";
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgReportingTechnique)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lbNombreDeVillas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDateFinTechnique;
        private System.Windows.Forms.DateTimePicker dtpDateDebutTechnique;
        private System.Windows.Forms.ComboBox cmbIlotsTechnique;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdReportingtechnique;
        private System.Windows.Forms.DataGridView dgReportingTechnique;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button cmdFermer;
        private System.Windows.Forms.Panel panel1;
    }
}