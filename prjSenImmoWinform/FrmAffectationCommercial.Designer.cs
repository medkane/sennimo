namespace prjSenImmoWinform
{
    partial class FrmAffectationCommercial
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtProspect = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdAffecter = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgCommerciaux = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateAffectation = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCommerciaux)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 396);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(450, 74);
            this.textBox1.TabIndex = 0;
            // 
            // txtProspect
            // 
            this.txtProspect.Location = new System.Drawing.Point(76, 46);
            this.txtProspect.Name = "txtProspect";
            this.txtProspect.ReadOnly = true;
            this.txtProspect.Size = new System.Drawing.Size(380, 20);
            this.txtProspect.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Prospect";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 380);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Commentaires";
            // 
            // cmdAffecter
            // 
            this.cmdAffecter.Location = new System.Drawing.Point(7, 500);
            this.cmdAffecter.Name = "cmdAffecter";
            this.cmdAffecter.Size = new System.Drawing.Size(101, 36);
            this.cmdAffecter.TabIndex = 8;
            this.cmdAffecter.Text = "Affecter";
            this.cmdAffecter.UseVisualStyleBackColor = true;
            this.cmdAffecter.Click += new System.EventHandler(this.cmdAffecter_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(375, 500);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "Fermer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgCommerciaux
            // 
            this.dgCommerciaux.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgCommerciaux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCommerciaux.Location = new System.Drawing.Point(6, 130);
            this.dgCommerciaux.Name = "dgCommerciaux";
            this.dgCommerciaux.RowHeadersVisible = false;
            this.dgCommerciaux.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCommerciaux.Size = new System.Drawing.Size(450, 239);
            this.dgCommerciaux.TabIndex = 10;
            this.dgCommerciaux.DoubleClick += new System.EventHandler(this.dgCommerciaux_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Date affectation";
            // 
            // dtpDateAffectation
            // 
            this.dtpDateAffectation.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateAffectation.Location = new System.Drawing.Point(360, 11);
            this.dtpDateAffectation.Name = "dtpDateAffectation";
            this.dtpDateAffectation.Size = new System.Drawing.Size(96, 20);
            this.dtpDateAffectation.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(219)))), ((int)(((byte)(197)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbProjets);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtProspect);
            this.panel1.Controls.Add(this.dgCommerciaux);
            this.panel1.Controls.Add(this.dtpDateAffectation);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 487);
            this.panel1.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 17);
            this.label8.TabIndex = 227;
            this.label8.Text = "Projet";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Items.AddRange(new object[] {
            "AKYS",
            "KERRIA"});
            this.cmbProjets.Location = new System.Drawing.Point(76, 72);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(380, 21);
            this.cmbProjets.TabIndex = 226;
            this.cmbProjets.SelectedIndexChanged += new System.EventHandler(this.cmbProjets_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(135)))), ((int)(((byte)(120)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(6, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 27);
            this.label2.TabIndex = 206;
            this.label2.Text = "Liste des commerciaux";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmAffectationCommercial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(481, 542);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmdAffecter);
            this.Name = "FrmAffectationCommercial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Affectation du commercial";
            ((System.ComponentModel.ISupportInitialize)(this.dgCommerciaux)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtProspect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdAffecter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgCommerciaux;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateAffectation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbProjets;
    }
}