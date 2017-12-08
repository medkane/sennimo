namespace prjSenImmoWinform
{
    partial class FrmSelectionRecouvrement
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
            this.rbDepot = new System.Windows.Forms.RadioButton();
            this.rbResa = new System.Windows.Forms.RadioButton();
            this.cmdAnnuler = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.cmbProjets = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbDepot
            // 
            this.rbDepot.AutoSize = true;
            this.rbDepot.Location = new System.Drawing.Point(91, 46);
            this.rbDepot.Name = "rbDepot";
            this.rbDepot.Size = new System.Drawing.Size(54, 17);
            this.rbDepot.TabIndex = 0;
            this.rbDepot.TabStop = true;
            this.rbDepot.Text = "Dépot";
            this.rbDepot.UseVisualStyleBackColor = true;
            // 
            // rbResa
            // 
            this.rbResa.AutoSize = true;
            this.rbResa.Location = new System.Drawing.Point(240, 46);
            this.rbResa.Name = "rbResa";
            this.rbResa.Size = new System.Drawing.Size(82, 17);
            this.rbResa.TabIndex = 1;
            this.rbResa.TabStop = true;
            this.rbResa.Text = "Réservation";
            this.rbResa.UseVisualStyleBackColor = true;
            // 
            // cmdAnnuler
            // 
            this.cmdAnnuler.Location = new System.Drawing.Point(5, 79);
            this.cmdAnnuler.Name = "cmdAnnuler";
            this.cmdAnnuler.Size = new System.Drawing.Size(103, 29);
            this.cmdAnnuler.TabIndex = 2;
            this.cmdAnnuler.Text = "Annuler";
            this.cmdAnnuler.UseVisualStyleBackColor = true;
            this.cmdAnnuler.Click += new System.EventHandler(this.cmdAnnuler_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(219, 79);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(103, 29);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type de contrat ";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(205)))), ((int)(((byte)(193)))));
            this.panel1.Controls.Add(this.label37);
            this.panel1.Controls.Add(this.cmbProjets);
            this.panel1.Controls.Add(this.rbDepot);
            this.panel1.Controls.Add(this.cmdOK);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmdAnnuler);
            this.panel1.Controls.Add(this.rbResa);
            this.panel1.Location = new System.Drawing.Point(4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 112);
            this.panel1.TabIndex = 5;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(7, 19);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(75, 17);
            this.label37.TabIndex = 230;
            this.label37.Text = "Projet";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProjets
            // 
            this.cmbProjets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProjets.FormattingEnabled = true;
            this.cmbProjets.Location = new System.Drawing.Point(91, 15);
            this.cmbProjets.Name = "cmbProjets";
            this.cmbProjets.Size = new System.Drawing.Size(224, 21);
            this.cmbProjets.TabIndex = 229;
            // 
            // FrmSelectionRecouvrement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(219)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(338, 121);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectionRecouvrement";
            this.Text = "Gestion du recouvrement";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbDepot;
        private System.Windows.Forms.RadioButton rbResa;
        private System.Windows.Forms.Button cmdAnnuler;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmbProjets;
    }
}